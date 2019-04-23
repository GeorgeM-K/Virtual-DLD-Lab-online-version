using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;


public class LabFourGrader : MonoBehaviour
{
    public Button Finish;
    public GameObject InputA, InputB, Gate1, Gate2; //OutputCout, OutputSum;
    List<GameObject> MarksList; //List that stores checkmark/cross game objects
    LogicManager logicManager;
    Sprite checkMarkSprite, crossMarkSprite;
    int LabFourGrade = 80;
    int [] attempt = new int [3] {0, 0,0};
    int numTries=0;
    // Use this for initialization

    void Start()
    {
        logicManager = GameObject.Find("LogicManager").GetComponent<LogicManager>();
        MarksList = new List<GameObject>();
        checkMarkSprite = Resources.Load<Sprite>("Sprites/002-tick");
        crossMarkSprite = Resources.Load<Sprite>("Sprites/001-close");

        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            switch (this.gameObject.transform.GetChild(i).name)
            {
                case "InputA":
                    InputA = this.gameObject.transform.GetChild(i).gameObject;
                    InputA.GetComponent<CheckerTagScript>().Type = GradingCONSTANTS.INPUT;
                    break;
                case "InputB":
                    InputB = this.gameObject.transform.GetChild(i).gameObject;
                    InputB.GetComponent<CheckerTagScript>().Type = GradingCONSTANTS.INPUT;
                    break;
                case "Gate1":
                    Gate1 = this.gameObject.transform.GetChild(i).gameObject;
                    Gate1.GetComponent<CheckerTagScript>().Type = GradingCONSTANTS.INPUT;
                    break;
                case "Gate2":
                    Gate2 = this.gameObject.transform.GetChild(i).gameObject;
                    Gate2.GetComponent<CheckerTagScript>().Type = GradingCONSTANTS.INPUT;
                    break;
                /*case "OutputCout":
                    OutputCout=this.gameObject.transform.GetChild(i).gameObject;
                    OutputCout.GetComponent<CheckerTagScript>().Type = GradingCONSTANTS.OUTPUT;
                    break;
                case "OutputSum":
                    OutputSum = this.gameObject.transform.GetChild(i).gameObject;
                    OutputSum.GetComponent<CheckerTagScript>().Type = GradingCONSTANTS.OUTPUT;
                    break;*/
            }
        }
        
        
        Finish.onClick.AddListener(GradeCheckInitializer);

    }

    private void GradeCheckInitializer()
    {
        numTries++;
        Debug.Log("Finish button clicked! Checking input and output.");
        StartCoroutine(FinishChecker());
    }

    private void AddCheckMarkOrCross(bool isCheckMark)
    {
        int count = MarksList.Count;
        if (isCheckMark)
        {
            GameObject check = new GameObject("Check");
            check.transform.parent = this.gameObject.transform;
            check.transform.position = new Vector3(-3.3f + count * .9f, 3.10f, 0);
            SpriteRenderer spriteRenderer = check.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = checkMarkSprite;
            MarksList.Add(check);

        }
        else if (!isCheckMark)
        {
            GameObject cross = new GameObject("Cross");
            cross.transform.parent = this.gameObject.transform;
            cross.transform.position = new Vector3(-3.3f + count * .9f, 3.10f, 0);
            SpriteRenderer spriteRenderer = cross.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = crossMarkSprite;
            MarksList.Add(cross);
        }
    }

    IEnumerator FinishChecker()
    {
        for (int i = 0; i < MarksList.Count; i++)
        {
            Destroy(MarksList[i]);
        }
        MarksList.Clear();
        CheckerTagScript InputATag = InputA.GetComponent<CheckerTagScript>();
        CheckerTagScript InputBTag = InputB.GetComponent<CheckerTagScript>();
        CheckerTagScript Gate1Tag = Gate1.GetComponent<CheckerTagScript>();
        CheckerTagScript Gate2Tag = Gate2.GetComponent<CheckerTagScript>();
        //CheckerTagScript OutputCoutTag = OutputCout.GetComponent<CheckerTagScript>();
        //CheckerTagScript OutputSumTag = OutputSum.GetComponent<CheckerTagScript>();

        if (InputATag.GetCollidingObject() == null)
        {
            Debug.Log("Switch A tags are not SNAPPED!");
            numTries = 0;

            yield break;
        }

        if (InputBTag.GetCollidingObject() == null)
        {
            Debug.Log("Switch B tags are not SNAPPED!");
            numTries = 0;
            yield break;
        }
        if (Gate1Tag.GetCollidingObject() == null)
        {
            Debug.Log("AND Gate tags are not SNAPPED!");
            numTries = 0;
            yield break;
        }
        if (Gate2Tag.GetCollidingObject() == null)
        {
            Debug.Log("XOR Gate tags are not SNAPPED!");
            numTries = 0;
            yield break;
        }
        Switch InputASwitch = InputATag.GetCollidingObject().GetComponent<Switch>();
        Switch InputBSwitch = InputBTag.GetCollidingObject().GetComponent<Switch>();
        ANDGate Gate1And = Gate1Tag.GetCollidingObject().GetComponent<ANDGate>();
        XORGate Gate2Xor = Gate2Tag.GetCollidingObject().GetComponent<XORGate>();
        //LEDScript OutputSumLED = OutputSumTag.GetCollidingObject().GetComponent<LEDScript>();
        //LEDScript OutputCoutLED = OutputSumTag.GetCollidingObject().GetComponent<LEDScript>();

        logicManager.ResetAllLogic();
        //yield return new WaitForSecondsRealtime(1);

        LogicNode outputB = InputBSwitch.middleNode.GetComponent<LogicNode>();
        LogicNode collidingoutputB = outputB.GetCollidingNode().GetComponent<LogicNode>();
        LogicNode outputA = InputASwitch.middleNode.GetComponent<LogicNode>();
        LogicNode collidingoutputA = outputA.GetCollidingNode().GetComponent<LogicNode>();

        if (collidingoutputB.logic_state == (int)LOGIC.LOW || !Gate1And.IsDeviceOn() || !Gate2Xor.IsDeviceOn() || collidingoutputA.logic_state == (int)LOGIC.LOW)
        {

            if (!Gate1And.IsDeviceOn())
            {
                Debug.Log("This output is incorrect");
                LabFourGrade -= 5;
            }

            if (!Gate2Xor.IsDeviceOn())
            {
                Debug.Log("This output is incorrect");
                LabFourGrade -= 5;
            }
                DataInsert.inputLab4Grade += LabFourGrade;
                Debug.Log("Correct Output");
                StartCoroutine(pushtoDataBase());
                SceneManager.LoadScene("Scenes/Postlab 4");

        }
        else
        {

            DataInsert.inputLab4Grade += LabFourGrade;
            Debug.Log("Correct Output");
            StartCoroutine(pushtoDataBase());
            SceneManager.LoadScene("Scenes/Postlab 4");
        }
    }



        IEnumerator pushtoDataBase(){

        string email = dbManager.email;
        int grade = DataInsert.inputLab4Grade;
        string lab = "labFour";
        //preLabOne
        //postLabOne

         WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("grade", grade);
        form.AddField("Lab", lab );
        
        WWW www = new WWW("https://dldvirtuallab.000webhostapp.com/labGrade.php", form);
        yield return www;

        if(www.text == "0"){
            Debug.Log("Grade entered successfully");
        }
        else{
            Debug.Log("Error" + www.text);
        }



    }

    // Update is called once per frame
    void Update()
    {

    }
}


