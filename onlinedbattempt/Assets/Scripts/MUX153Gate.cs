// written by: Khalid Akash
// tested by: Khalid Akash
// debugged by: Khalid Akash

// edited 2019 Daniel Chan
// manipulated MUXGate script to create a script for the 74LS153
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The chips are all movable devices that contain 14 Logic Nodes.
/// These Logic Nodes are stored in Hash Tables, but the implementation
/// can easily be changed to Lists as well. To function correctly,
/// they must be ‘snapped’ to 14 other nodes, typically this means 
/// a collision between all of the chip’s Logic Nodes and the Protoboard’s
/// Logic Nodes are detected simultaneously. Once the chip detect that all
/// 14 nodes are collided with, and the user lifts the mouse, the OnMouseUp()
/// callback is recorded, and the position of the chip is snapped to the
/// top left Logic Node’s collided Logic Node’s position (arbitrarily chosen),
/// a green indicator is shown to show potential snappings.  Once the device
/// is snapped, before any logic calculation is done, the chip must detect
/// a collided node on both the 7th pin, and the 14th pin, with a logic low
/// and a logic high going to the respective nodes. After that, based
/// on the datasheet, the collided input Logic Node’s states are taken, and the 
/// output is set.
/// [Edit DC19] This chip has 16 pins (WIP)
/// </summary>
public class MUX153Gate : MonoBehaviour, LogicInterface
{


    private Dictionary<string, GameObject> logic_dictionary = new Dictionary<string, GameObject>(); //Contains all the gameobject nodes for the 74LS400 chip.+
    private GameObject DeviceGameObject;
    private GameObject snapIndicatorGameObj;
    public const string LOGIC_DEVICE_ID = "74LS153_MUX_NODE_";
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool SNAPPED = false; //Set to true if all Logic Nodes of this device is in collision with an external node


    public void SetSnapped(bool snap)
    {
        this.SNAPPED = snap;
    }

    public Dictionary<string, GameObject> GetLogicDictionary()
    {
        return logic_dictionary;
    }

    /// <summary>
    /// Sets all 14 nodes in the specified position in the Logic Chips
    /// </summary>
    void Start()
    {
        DeviceGameObject = this.gameObject;
        //Loop that places Logic Nodes on the 74LS400 chip
        float horizontal_pos = -.205f; //set up for left side of the chip
        float vertical_pos = .58f; //top of the chip
        float vertical_direct = -.208f;
        for (int i = 0; i < 16; i++)
        {
            GameObject logicNode = new GameObject(LOGIC_DEVICE_ID + i); //logic node with the name leftlogicnode_{i}_0
            logicNode.transform.parent = DeviceGameObject.transform; //sets the Protoboard game object as logicNode_0's parent
            logicNode.transform.localPosition = new Vector3(horizontal_pos, vertical_pos + i * (vertical_direct), 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
            logicNode.transform.localScale = new Vector3(.10F, .10F, 0);
            logicNode.AddComponent<LogicNode>();
            logic_dictionary.Add(LOGIC_DEVICE_ID + i, logicNode);
            if (i == 5) //when the left side is complete
            {
                vertical_pos = vertical_pos + (13 * vertical_direct);
                vertical_direct = .208f;
                horizontal_pos = horizontal_pos + .532f; //change the horizontal position to the right side

            }
        }

        //add SNAP indicator object to the chip
        snapIndicatorGameObj = new GameObject(LOGIC_DEVICE_ID + "_SNAP_INDICATOR_");
        snapIndicatorGameObj.transform.parent = DeviceGameObject.transform; //sets the Protoboard game object as logicNode_0's parent
        snapIndicatorGameObj.transform.localPosition = new Vector3(-.0775f, .575f, 0); //'localPosition' sets the position of this node RELATIVE to the protoboard
        snapIndicatorGameObj.transform.localScale = new Vector3(.10F, .10F, 0);
        SpriteRenderer sprite_renderer = snapIndicatorGameObj.AddComponent<SpriteRenderer>(); //adds a test "circle" graphic
        sprite_renderer.sprite = Resources.Load<Sprite>("Sprites/logicCircle");
        sprite_renderer.sortingLayerName = "FrontLayer";
        sprite_renderer.material.color = new Color(1, 1, 1);
    }

    /// <summary>
    /// Mouse down on GameObject activates the movement of the object
    /// to follow the mouse position
    /// </summary>
    void OnMouseDown()
    {
        Debug.Log("74LS153 Mouse Down");
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    }
    /// <summary>
    /// Callback that notifies the object that the mouse is being dragged
    /// on it. This is used to help 'move' the GameObject by calculating the offset
    /// from the previous mouse position. It also checks if the chip is in the
    /// 'Snapped' position if the Mouse click is let go.
    /// </summary>
    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
        transform.position = curPosition;

        //Check for snapping when chip is removed from set place.
        //Check if all nodes with the chip is colliding with another logic node;
        SpriteRenderer spr_ren = snapIndicatorGameObj.GetComponent<SpriteRenderer>();
        foreach (KeyValuePair<string, GameObject> entry in logic_dictionary)
        {
            GameObject logic_node = entry.Value;
            LogicNode logic_behavior = logic_node.GetComponent<LogicNode>();
            if (logic_behavior.GetCollidingNode() == null)
            {
                //indicator
                spr_ren.material.color = new Color(1, 1, 1); //neutral
                SNAPPED = false;
                Debug.Log("Snap not set.");
                return;
            }
        }
        //if execution reached here, it means all colliding nodes are valid nodes
        //indicate device can be active
        spr_ren.material.color = new Color(0, 1, 0); //green

    }

    /// <summary>
    /// Checks if pin 7 and pin 14 is connected to ground and logic high respectively.
    /// This is checked whenever a new state change is requested to be reacted to.
    /// </summary>
    /// <returns>True or False boolean value on whether the Device is considered on</returns>
    public bool IsDeviceOn()
    {
        if (!SNAPPED)
        {
            return false;
        }
        GameObject logic_gnd;
        GameObject logic_vcc;
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 6, out logic_gnd)
            && logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 13, out logic_vcc))
        {
            LogicNode logic_behavior_gnd = logic_gnd.GetComponent<LogicNode>();
            LogicNode logic_behavior_vcc = logic_vcc.GetComponent<LogicNode>();
            LogicNode gndCollision = logic_behavior_gnd.GetCollidingNode().GetComponent<LogicNode>();
            LogicNode vccCollision = logic_behavior_vcc.GetCollidingNode().GetComponent<LogicNode>();
            Debug.Log("GND Set to: " + gndCollision.GetLogicState() + " for Device " + this.gameObject.name);
            Debug.Log("VCC Set to: " + vccCollision.GetLogicState() + " for Device " + this.gameObject.name);
            if (gndCollision.GetLogicState() == (int)LOGIC.LOW
                && vccCollision.GetLogicState() == (int)LOGIC.HIGH)
            {
                Debug.Log(this.DeviceGameObject.name + " is ON.");
                return true;
            }
        }
        Debug.Log(this.DeviceGameObject.name + " is OFF.");
        return false;
    }


    /// <summary>
    /// Checks if the Device is On, and then continues to check all the inputs colliding
    /// node and sets the output nodes to the correct state.
    /// </summary>
    private void ChipIO()
    {
        GameObject input2C1_11, input2C0_10, output2Y_9, output1Y_7, input1C1_5, input1C0_6, input1C2_4, input1C3_3, selectB_2,
            strobe1G_1, strobe2G_15, selectA_14, input2C3_13, input2C2_12, gnd_8, vcc_16;

        //GND, Node 8
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 7, out gnd_8))
        {
            LogicNode logic_behavior = gnd_8.GetComponent<LogicNode>();
            GameObject collided_node = logic_behavior.GetCollidingNode();
            LogicNode collided_behavior = collided_node.GetComponent<LogicNode>();
            int collided_state = collided_behavior.GetLogicState();
            //GND pin collision node is not GND
            if (collided_state != (int)LOGIC.LOW)
            {
                logic_behavior.SetLogicState((int)LOGIC.INVALID);
                Debug.Log("MUX 74LS153 Ground Input not set to LOW");
            }

        }
        //VCC, Node 16
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 15, out vcc_16))
        {
            LogicNode logic_behavior = vcc_16.GetComponent<LogicNode>();
            GameObject collided_node = logic_behavior.GetCollidingNode();
            LogicNode collided_behavior = collided_node.GetComponent<LogicNode>();
            int collided_state = collided_behavior.GetLogicState();
            if (collided_state != (int)LOGIC.HIGH)
            {
                logic_behavior.SetLogicState((int)LOGIC.INVALID);
                Debug.Log("MUX 74LS153 VCC Input not set to HIGH");
            }

        }
        /**
         * INPUTs find the collided nodes of the input pins and sets the input's
         * pin state to the collided node's state.
         * 
         */

        //MUX Select B, Node 2
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 1, out selectB_2))
        {
            LogicNode logic_behavior = selectB_2.GetComponent<LogicNode>();
            GameObject collided_node = logic_behavior.GetCollidingNode();
            LogicNode collided_behavior = collided_node.GetComponent<LogicNode>();
            int collided_state = collided_behavior.GetLogicState();
            if (collided_state == (int)LOGIC.INVALID || !IsDeviceOn())
            {
                logic_behavior.SetLogicState((int)LOGIC.INVALID);
                Debug.Log("MUX 74LS153 input 0 is invalid.");
            }

        }

        //MUX Strobe 1G, Node 1
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 0, out strobe1G_1))
        {
            LogicNode logic_behavior = strobe1G_1.GetComponent<LogicNode>();
            GameObject collided_node = logic_behavior.GetCollidingNode();
            LogicNode collided_behavior = collided_node.GetComponent<LogicNode>();
            int collided_state = collided_behavior.GetLogicState();
            if (collided_state == (int)LOGIC.INVALID || !IsDeviceOn())
            {
                logic_behavior.SetLogicState((int)LOGIC.INVALID);
                Debug.Log("MUX 74LS153 input 0 is invalid.");
            }

        }

        //MUX Strobe 2G, Node 15
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 14, out strobe2G_15))
        {
            LogicNode logic_behavior = strobe2G_15.GetComponent<LogicNode>();
            GameObject collided_node = logic_behavior.GetCollidingNode();
            LogicNode collided_behavior = collided_node.GetComponent<LogicNode>();
            int collided_state = collided_behavior.GetLogicState();
            if (collided_state == (int)LOGIC.INVALID || !IsDeviceOn())
            {
                logic_behavior.SetLogicState((int)LOGIC.INVALID);
                Debug.Log("MUX 74LS153 input 0 is invalid.");
            }

        }

        //MUX Select A, Node 14
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 13, out selectA_14))
        {
            LogicNode logic_behavior = selectA_14.GetComponent<LogicNode>();
            GameObject collided_node = logic_behavior.GetCollidingNode();
            LogicNode collided_behavior = collided_node.GetComponent<LogicNode>();
            int collided_state = collided_behavior.GetLogicState();
            if (collided_state == (int)LOGIC.INVALID || !IsDeviceOn())
            {
                logic_behavior.SetLogicState((int)LOGIC.INVALID);
                Debug.Log("MUX 74LS153 input 0 is invalid.");
            }

        }

        //MUX Input 2C3, Node 13
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 12, out input2C3_13))
        {
            LogicNode logic_behavior = input2C3_13.GetComponent<LogicNode>();
            GameObject collided_node = logic_behavior.GetCollidingNode();
            LogicNode collided_behavior = collided_node.GetComponent<LogicNode>();
            int collided_state = collided_behavior.GetLogicState();
            if (collided_state == (int)LOGIC.INVALID || !IsDeviceOn())
            {
                logic_behavior.SetLogicState((int)LOGIC.INVALID);
                Debug.Log("MUX 74LS153 input 0 is invalid.");
            }

        }

        //MUX Input 2C2, Node 12
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 11, out input2C2_12))
        {
            LogicNode logic_behavior = input2C2_12.GetComponent<LogicNode>();
            GameObject collided_node = logic_behavior.GetCollidingNode();
            LogicNode collided_behavior = collided_node.GetComponent<LogicNode>();
            int collided_state = collided_behavior.GetLogicState();
            if (collided_state == (int)LOGIC.INVALID || !IsDeviceOn())
            {
                logic_behavior.SetLogicState((int)LOGIC.INVALID);
                Debug.Log("MUX 74LS153 input 0 is invalid.");
            }

        }

        //MUX Input 2C1, Node 11
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 10, out input2C1_11))
        {
            LogicNode logic_behavior = input2C1_11.GetComponent<LogicNode>();
            GameObject collided_node = logic_behavior.GetCollidingNode();
            LogicNode collided_behavior = collided_node.GetComponent<LogicNode>();
            int collided_state = collided_behavior.GetLogicState();
            if (collided_state == (int)LOGIC.INVALID || !IsDeviceOn())
            {
                logic_behavior.SetLogicState((int)LOGIC.INVALID);
                Debug.Log("MUX 74LS153 input 0 is invalid.");
            }

        }

        //MUX Input 2C0, Node 10
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 9, out input2C0_10))
        {
            LogicNode logic_behavior = input2C0_10.GetComponent<LogicNode>();
            GameObject collided_node = logic_behavior.GetCollidingNode();
            LogicNode collided_behavior = collided_node.GetComponent<LogicNode>();
            int collided_state = collided_behavior.GetLogicState();
            if (collided_state == (int)LOGIC.INVALID || !IsDeviceOn())
            {
                logic_behavior.SetLogicState((int)LOGIC.INVALID);
                Debug.Log("MUX 74LS153 input 0 is invalid.");
            }

        }

        //MUX Output 2Y, Node 9
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 8, out output2Y_9))
        {
            LogicNode logic_behavior = output2Y_9.GetComponent<LogicNode>();
            GameObject collided_node = logic_behavior.GetCollidingNode();
            LogicNode collided_behavior = collided_node.GetComponent<LogicNode>();
            int collided_state = collided_behavior.GetLogicState();
            LogicNode iB0, iB1, iB2, iB3, E, A, B;
            iB0 = input2C0_10.GetComponent<LogicNode>();
            iB1 = input2C1_11.GetComponent<LogicNode>();
            iB2 = input2C2_12.GetComponent<LogicNode>();
            iB3 = input2C3_13.GetComponent<LogicNode>();
            E = strobe2G_15.GetComponent<LogicNode>();
            A = selectA_14.GetComponent<LogicNode>();
            B = selectB_2.GetComponent<LogicNode>();
            int low = (int)LOGIC.LOW;
            int high = (int)LOGIC.HIGH;
            int invalid = (int)LOGIC.INVALID;

            bool AND0, AND1, AND2, AND3;

            //using the schematic for the logic of a 74LS153, we simplify the code using the individual gates
            if (iB0.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == high &&
                A.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == low &&
                B.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == low &&
                E.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == low)
            {
                AND0 = true;
            }
            else
            {
                AND0 = false;
            }

            if (iB1.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == high &&
                A.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == high &&
                B.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == low &&
                E.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == low)
            {
                AND1 = true;
            }
            else
            {
                AND1 = false;
            }

            if (iB2.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == high &&
                A.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == low &&
                B.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == high &&
                E.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == low)
            {
                AND2 = true;
            }
            else
            {
                AND2 = false;
            }

            if (iB3.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == high &&
                A.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == high &&
                B.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == high &&
                E.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == low)
            {
                AND3 = true;
            }
            else
            {
                AND3 = false;
            }

            if (IsDeviceOn())
            {
                if (AND0 || AND1 || AND2 || AND3)
                {
                    logic_behavior.SetLogicState((int)LOGIC.HIGH);
                }
                else
                {
                    logic_behavior.SetLogicState((int)LOGIC.LOW);
                }
            }
            else
            {
                logic_behavior.SetLogicState((int)LOGIC.INVALID);
            }


        }

        //MUX Input 1C3, Node 3
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 2, out input1C3_3))
        {
            LogicNode logic_behavior = input1C3_3.GetComponent<LogicNode>();
            GameObject collided_node = logic_behavior.GetCollidingNode();
            LogicNode collided_behavior = collided_node.GetComponent<LogicNode>();
            int collided_state = collided_behavior.GetLogicState();
            if (collided_state == (int)LOGIC.INVALID || !IsDeviceOn())
            {
                logic_behavior.SetLogicState((int)LOGIC.INVALID);
                Debug.Log("MUX 74LS153 input 0 is invalid.");
            }

        }

        //MUX Input 1C2, Node 4
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 3, out input1C2_4))
        {
            LogicNode logic_behavior = input1C2_4.GetComponent<LogicNode>();
            GameObject collided_node = logic_behavior.GetCollidingNode();
            LogicNode collided_behavior = collided_node.GetComponent<LogicNode>();
            int collided_state = collided_behavior.GetLogicState();
            if (collided_state == (int)LOGIC.INVALID || !IsDeviceOn())
            {
                logic_behavior.SetLogicState((int)LOGIC.INVALID);
                Debug.Log("MUX 74LS153 input 0 is invalid.");
            }

        }

        //MUX Input 1C1, Node 5
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 4, out input1C1_5))
        {
            LogicNode logic_behavior = input1C1_5.GetComponent<LogicNode>();
            GameObject collided_node = logic_behavior.GetCollidingNode();
            LogicNode collided_behavior = collided_node.GetComponent<LogicNode>();
            int collided_state = collided_behavior.GetLogicState();
            if (collided_state == (int)LOGIC.INVALID || !IsDeviceOn())
            {
                logic_behavior.SetLogicState((int)LOGIC.INVALID);
                Debug.Log("MUX 74LS153 input 0 is invalid.");
            }
        }

        //MUX Input 1C0, Node 6
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 5, out input1C0_6))
        {
            LogicNode logic_behavior = input1C0_6.GetComponent<LogicNode>();
            GameObject collided_node = logic_behavior.GetCollidingNode();
            LogicNode collided_behavior = collided_node.GetComponent<LogicNode>();
            int collided_state = collided_behavior.GetLogicState();
            if (collided_state == (int)LOGIC.INVALID || !IsDeviceOn())
            {
                logic_behavior.SetLogicState((int)LOGIC.INVALID);
                Debug.Log("MUX 74LS153 input 0 is invalid.");
            }
        }

        //MUX Output 1Y, Node 7
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 6, out output1Y_7))
        {
            LogicNode logic_behavior = output1Y_7.GetComponent<LogicNode>();
            GameObject collided_node = logic_behavior.GetCollidingNode();
            LogicNode collided_behavior = collided_node.GetComponent<LogicNode>();
            int collided_state = collided_behavior.GetLogicState();
            LogicNode iA0, iA1, iA2, iA3, E, A, B;
            iA0 = input1C0_6.GetComponent<LogicNode>();
            iA1 = input1C1_5.GetComponent<LogicNode>();
            iA2 = input1C2_4.GetComponent<LogicNode>();
            iA3 = input1C3_3.GetComponent<LogicNode>();
            E = strobe1G_1.GetComponent<LogicNode>();
            A = selectA_14.GetComponent<LogicNode>();
            B = selectB_2.GetComponent<LogicNode>();
            int low = (int)LOGIC.LOW;
            int high = (int)LOGIC.HIGH;
            int invalid = (int)LOGIC.INVALID;

            bool AND0, AND1, AND2, AND3;

            //using the schematic for the logic of a 74LS153, we simplify the code using the individual gates
            if( iA0.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == high &&
                A.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == low &&
                B.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == low &&
                E.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == low)
            {
                AND0 = true;
            }
            else
            {
                AND0 = false;
            }

            if (iA1.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == high &&
                A.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == high &&
                B.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == low &&
                E.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == low)
            {
                AND1 = true;
            }
            else
            {
                AND1 = false;
            }

            if (iA2.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == high &&
                A.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == low &&
                B.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == high &&
                E.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == low)
            {
                AND2 = true;
            }
            else
            {
                AND2 = false;
            }

            if (iA3.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == high &&
                A.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == high &&
                B.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == high &&
                E.GetCollidingNode().GetComponent<LogicNode>().GetLogicState() == low)
            {
                AND3 = true;
            }
            else
            {
                AND3 = false;
            }

            if (IsDeviceOn())
            {
                if( AND0 || AND1 || AND2 || AND3 )
                {
                    logic_behavior.SetLogicState((int)LOGIC.HIGH);
                }
                else
                {
                    logic_behavior.SetLogicState((int)LOGIC.LOW);
                }
            }
            else
            {
                logic_behavior.SetLogicState((int)LOGIC.INVALID);
            }

        }



    }
    /// <summary>
    /// Check if the device has all it's nodes is colliding with another set of Logic Nodes
    /// </summary>
    private void CheckIfSnapped()
    {
        Debug.Log("XOR 74LS86A Mouse Up");

        //Check if all nodes with the chip is colliding with another logic node;
        foreach (KeyValuePair<string, GameObject> entry in logic_dictionary)
        {
            GameObject logic_node = entry.Value;
            LogicNode logic_behavior = logic_node.GetComponent<LogicNode>();
            if (logic_behavior.GetCollidingNode() == null)
            {
                //indicator
                SpriteRenderer spr_ren = snapIndicatorGameObj.GetComponent<SpriteRenderer>();
                spr_ren.material.color = new Color(1, 1, 1); //neutral
                SNAPPED = false;
                Debug.Log("Snap not set.");
                return;
            }
        }
        //On release of mouse, SNAP the chip to the position
        GameObject node_left;
        //get both top left and top right logic nodes on the chip to check if they collided with any other logic nodes
        if (logic_dictionary.TryGetValue(LOGIC_DEVICE_ID + 0, out node_left))
        {
            LogicNode logicNodeScript_l = node_left.GetComponent<LogicNode>();
            GameObject collidingNodeLeft = logicNodeScript_l.GetCollidingNode();
            Debug.Log("XOR 74LS86A SNAPPED!");
            Vector3 collidingNodePos = collidingNodeLeft.transform.position;
            Vector3 offsetPosition = new Vector3(collidingNodePos.x + .245f, collidingNodePos.y - .58f, collidingNodePos.z);
            DeviceGameObject.transform.position = offsetPosition;
            //indicator
            SpriteRenderer spr_ren = snapIndicatorGameObj.GetComponent<SpriteRenderer>();
            spr_ren.material.color = new Color(0, 1, 0); //green
            SNAPPED = true;
        }
    }
    /// <summary>
    /// Checks if the chip is snapped when the Mouse click is released to snap it
    /// into position.
    /// </summary>
    public void OnMouseUp()
    {
        CheckIfSnapped();
    }


    /// <summary>
    /// Sets all the nodes of the chip to a logic of invalid/neutral.
    /// </summary>
    private void ClearChip()
    {
        foreach (KeyValuePair<string, GameObject> entry in logic_dictionary)
        {
            GameObject logicNodeGameObj = entry.Value;
            LogicNode logic_node = logicNodeGameObj.GetComponent<LogicNode>();
            logic_node.SetLogicState((int)LOGIC.INVALID);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// If the chip is snapped, react to the input logics and set the outputs
    /// to the correct states. Otherwise, clear the chips.
    /// </summary>
    /// <param name="logicNode"></param>
    /// <param name="requestedState"></param>
    public void ReactToLogic(GameObject logicNode, int requestedState)
    {
        if (requestedState == (int)LOGIC.INVALID && !SNAPPED)
        {
            ClearChip();
        }
        //Check if chip is snapped to protoboard, and then updates logic
        else if (SNAPPED)
        {
            ChipIO();
        }

    }


    public bool isSnapped()
    {
        return SNAPPED;
    }


    public void ReactToLogic(GameObject LogicNode)
    {

    }


}