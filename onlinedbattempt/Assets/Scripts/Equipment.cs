// written by: Khalid Akash
// tested by: Khalid Akash
// debugged by: Khalid Akash
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Equipment object that holds information about all Equipment available to
/// use and lists them on a Dropdown menu. Allows the user to create new
/// equipment from clicking the Dropdown.
/// </summary>
public class Equipment : MonoBehaviour {

    public Dropdown dropDown;
    public List<string> equipmentNames;
    GameObject mainCamera;
    int resetValue;
	/// <summary>
    /// Lists the available equipment to the Dropdown menu
    /// </summary>
	public void Start () {
        mainCamera = GameObject.Find("Main Camera");
        equipmentNames = new List<string>();
        equipmentNames.Add("None Selected");
        equipmentNames.Add("74LS00");
        equipmentNames.Add("74LS04");
        equipmentNames.Add("74LS08");
        equipmentNames.Add("74LS32");
        equipmentNames.Add("74LS86A");
        equipmentNames.Add("74LS151A");
        equipmentNames.Add("74LS153");
        equipmentNames.Add("LED");
        equipmentNames.Add("SPDT");
        equipmentNames.Add("Wire");
        dropDown.ClearOptions();
        List<Dropdown.OptionData> equipmentListDD = new List<Dropdown.OptionData>();
        foreach(string equipName in equipmentNames)
        {
            var equipNameOptionData = new Dropdown.OptionData(equipName);
            equipmentListDD.Add(equipNameOptionData);
        }
        dropDown.AddOptions(equipmentListDD);
        GameObject label = dropDown.transform.Find("Label").gameObject;
        label.GetComponent<Text>().text = "Equipment";
        dropDown.onValueChanged.AddListener(CallBackWithParameter);
	}
	
    /// <summary>
    /// Call back from clicking the Equipment Dropdown menu
    /// </summary>
    /// <param name="index">Index in which the equipment is contained.</param>
    public void CallBackWithParameter(int index)
    {
        string equipmentName = dropDown.GetComponent<Dropdown>().options[index].text;
        Debug.Log("Dropdown Selected option: " + equipmentName);
        //change label back to equipment
        GameObject label = dropDown.transform.Find("Label").gameObject;
        label.GetComponent<Text>().text = "Equipment";
        //load prefab into game
        LoadPrefab(equipmentName);
    }


    /// <summary>
    /// Load the Prefab (premade GameObject) based on what is clicked on the
    /// Dropdown menu.
    /// </summary>
    /// <param name="equipmentName">Name of the equipment</param>
    private void LoadPrefab(string equipmentName)
    {
        GameObject newPrefab = null;
        GameObject equipment = null;
        Debug.Log("Creating " + equipmentName + " prefab.");
        switch (equipmentName)
        {
            case "74LS00":
                newPrefab = Resources.Load<GameObject>("Prefabs/Lab/NANDChip");
                LoadPrefab(null);
                break;
            case "74LS04":
                newPrefab = Resources.Load<GameObject>("Prefabs/Lab/INVChip");
                LoadPrefab(null);
                break;
            case "74LS08":
                newPrefab = Resources.Load<GameObject>("Prefabs/Lab/ANDChip");
                LoadPrefab(null);
                break;
            case "74LS32":
                newPrefab = Resources.Load<GameObject>("Prefabs/Lab/ORChip");
                LoadPrefab(null);
                break;
            case "74LS86A":
                newPrefab = Resources.Load<GameObject>("Prefabs/Lab/XORChip");
                LoadPrefab(null);
                break;
            case "LED":
                newPrefab = Resources.Load<GameObject>("Prefabs/Lab/LEDChip");
                LoadPrefab(null);
                break;
            case "SPDT":
                newPrefab = Resources.Load<GameObject>("Prefabs/Lab/Switch");
                LoadPrefab(null);
                break;
            case "74LS153":
                newPrefab = Resources.Load<GameObject>("Prefabs/Lab/MUX153Chip");
                LoadPrefab(null);
                break;
            case "74LS151A":
                newPrefab = Resources.Load<GameObject>("Prefabs/Lab/MUXChip");
                LoadPrefab(null);
                break;
            case "Wire": //TODO
            LoadPrefab(null);
            /*   while (equipmentName == wire){
                GameObject wire = new GameObject("[Wire");
                wire.AddComponent<Wire>();
                wire.transform.parent = mainCamera.transform;
            }
remove loadprefab(null)s if it doesn't work to return to normal*/
                break; 

        }
        if (newPrefab != null)
        {
            equipment = Instantiate<GameObject>(newPrefab);
        }
        if (equipment == null)
        {
            return;
        }
        equipment.transform.parent = mainCamera.transform;
        equipment.transform.localPosition = new Vector3(0, 0, 10);
        dropDown.value = 0;
    }
    
	// Update is called once per frame
	void Update () {
		
	}
}
