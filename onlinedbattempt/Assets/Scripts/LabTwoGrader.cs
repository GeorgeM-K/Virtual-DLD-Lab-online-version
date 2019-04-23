// written by: Christopher Basilio
// tested by: Christopher Basilio
// debugged by: Christopher Basilio
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;


public class LabTwoGrader : MonoBehaviour
{
    public Button Finish;
    public GameObject InputA, InputB, Gate1, Gate2, OutputCout, OutputSum;
    List<GameObject> MarksList; //List that stores checkmark/cross game objects
    LogicManager logicManager;
    Sprite checkMarkSprite, crossMarkSprite;
    int LabTwoGrade = 80;
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
                case "OutputCout":
                    OutputCout=this.gameObject.transform.GetChild(i).gameObject;
                    OutputCout.GetComponent<CheckerTagScript>().Type = GradingCONSTANTS.OUTPUT;
                    break;
                case "OutputSum":
                    OutputSum = this.gameObject.transform.GetChild(i).gameObject;
                    OutputSum.GetComponent<CheckerTagScript>().Type = GradingCONSTANTS.OUTPUT;
                    break;
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
        CheckerTagScript OutputCoutTag = OutputCout.GetComponent<CheckerTagScript>();
        CheckerTagScript OutputSumTag = OutputSum.GetComponent<CheckerTagScript>();

        if (InputATag.GetCollidingObject() == null)
        {
            Debug.Log("Switch A tags are not SNAPPED!");
                numTries = 0;
            
            yield break;
        }

        if( InputBTag.GetCollidingObject() == null){
            Debug.Log("Switch B tags are not SNAPPED!");
       numTries = 0;
            
            yield break;
        }
        if(Gate1Tag.GetCollidingObject() == null){
            Debug.Log("AND Gate tags are not SNAPPED!");
       numTries = 0;
            
            yield break;
        }
        if(Gate2Tag.GetCollidingObject() == null ){
            Debug.Log("OR Gate tags are not SNAPPED!");
       numTries = 0;
            
            yield break;
        }
        if(OutputSumTag.GetCollidingObject() == null ){
            Debug.Log("LED tags are not SNAPPED!");
       numTries = 0;
            
            yield break;
        }
        if(OutputCoutTag.GetCollidingObject()==null)
            Debug.Log("COut is not SNAPPED!");
        numTries=0;

        yield break;


        Switch InputASwitch = InputATag.GetCollidingObject().GetComponent<Switch>();
        Switch InputBSwitch = InputBTag.GetCollidingObject().GetComponent<Switch>();
        ANDGate Gate1And = Gate1Tag.GetCollidingObject().GetComponent<ANDGate>();
        ORGate Gate2Or = Gate2Tag.GetCollidingObject().GetComponent<ORGate>();
        LEDScript OutputSumLED = OutputSumTag.GetCollidingObject().GetComponent<LEDScript>();
        LEDScript OutputCoutLED = OutputSumTag.GetCollidingObject().GetComponent<LEDScript>();


        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);

    LogicNode outputB = InputBSwitch.middleNode.GetComponent<LogicNode>();
    LogicNode collidingoutputB = outputB.GetCollidingNode().GetComponent<LogicNode>();
    LogicNode outputA = InputASwitch.middleNode.GetComponent<LogicNode>();
    LogicNode collidingoutputA = outputA.GetCollidingNode().GetComponent<LogicNode>();
    
    if(numTries<3){

    if(!Gate1And.IsDeviceOn()){
        Debug.Log("This output is incorrect");
        attempt[numTries] -= 5;
        yield break;
    }
    if(!Gate2Or.IsDeviceOn()){
        Debug.Log("This output is incorrect");
        attempt[numTries] -= 5;
        yield break;
    }
    if(collidingoutputA.logic_state==(int)LOGIC.LOW && collidingoutputB.logic_state==(int)LOGIC.LOW){
        if(OutputSumLED.isLEDON() || OutputCoutLED.isLEDON()){
            Debug.Log("This output is incorrect");
            attempt[numTries]-=5;
            yield break;
        }
    }
    if(collidingoutputA.logic_state==(int)LOGIC.LOW && collidingoutputB.logic_state==(int)LOGIC.HIGH){
        if(!OutputSumLED.isLEDON() || OutputCoutLED.isLEDON()){
            Debug.Log("This output is incorrect");
            attempt[numTries]-=5;
            yield break;
        }
    }
    if(collidingoutputA.logic_state==(int)LOGIC.HIGH && collidingoutputB.logic_state==(int)LOGIC.LOW){
        if(!OutputSumLED.isLEDON() || OutputCoutLED.isLEDON()){
            Debug.Log("This output is incorrect");
            attempt[numTries]-=5;
            yield break;
        }
    }
    if(collidingoutputA.logic_state==(int)LOGIC.HIGH && collidingoutputB.logic_state==(int)LOGIC.HIGH){
        if(OutputSumLED.isLEDON() || !OutputCoutLED.isLEDON()){
            Debug.Log("This output is incorrect");
            attempt[numTries]-=5;
            yield break;
        }
    }

    else{
       int actualGrade2 = LabTwoGrade;
        DataInsert.inputLab2Grade += actualGrade2;
        Debug.Log("Correct Output");
				SceneManager.LoadScene("Scenes/prelab2Kmap");
    }
    
    }
    
        else{
        int actualGrade2 = attempt.Max();
        DataInsert.inputLab2Grade += actualGrade2+80;
        Debug.Log("Incorrect Output, assigning credit based on best attempt");
        SceneManager.LoadScene("Scenes/prelab2Kmap");//named it wrong
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator pushtoDataBase()
    {

        string email = dbManager.email;
        int grade = DataInsert.inputPrelab4Grade;
        string lab = "labTwo";
        //preLabOne
        //postLabOne

        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("grade", grade);
        form.AddField("Lab", lab);

        WWW www = new WWW("https://dldvirtuallab.000webhostapp.com/labGrade.php", form);
        yield return www;

        if (www.text == "0")
        {
            Debug.Log("Grade entered successfully");
        }
        else
        {
            Debug.Log("Error" + www.text);
        }



    }

}

