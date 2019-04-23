// written by: George Melman-Kenny
// tested by: George Melman-Kenny
// debugged by: George Melman-Kenny
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public static class GradingCONSTANTS
{
    public static string INPUT = "INPUTSWITCH";
    public static string OUTPUT = "OUTPUTLED";
}
public class LabOneGrader : MonoBehaviour
{
    public Button Finish;
    public GameObject InputA, InputB, InputC, Gate1, Gate2, OutputF;
    List<GameObject> MarksList; //List that stores checkmark/cross game objects
    LogicManager logicManager;
    Sprite checkMarkSprite, crossMarkSprite;
    int LabOneGrade = 80;
	int preLab1Grade = 10;
    int [] attempt = new int [3] {0, 0,0};
    int numTries=0;
    // Use this for initialization

    //void OnGUI(){
        //GUI.Label(new Rect(10, 10, 100, 20), "All tags are not SNAPPED");
   // }

    void Start()
    {
        logicManager = GameObject.Find("LogicManager").GetComponent<LogicManager>();
        MarksList = new List<GameObject>();
        checkMarkSprite = Resources.Load<Sprite>("Sprites/002-tick");
        crossMarkSprite = Resources.Load<Sprite>("Sprites/001-close");
        /* InputA.GetComponent<CheckerTagScript>().Type = GradingCONSTANTS.INPUT;
        InputB.GetComponent<CheckerTagScript>().Type = GradingCONSTANTS.INPUT;
        InputC.GetComponent<CheckerTagScript>().Type = GradingCONSTANTS.INPUT;
        Gate1.GetComponent<CheckerTagScript>().Type = GradingCONSTANTS.INPUT;
        Gate2.GetComponent<CheckerTagScript>().Type = GradingCONSTANTS.INPUT;
        OutputF.GetComponent<CheckerTagScript>().Type = GradingCONSTANTS.OUTPUT; */
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
                case "InputC":
                    InputC = this.gameObject.transform.GetChild(i).gameObject;
                    InputC.GetComponent<CheckerTagScript>().Type = GradingCONSTANTS.INPUT;
                    break;
                case "Gate1":
                    Gate1 = this.gameObject.transform.GetChild(i).gameObject;
                    Gate1.GetComponent<CheckerTagScript>().Type = GradingCONSTANTS.INPUT;
                    break;
                case "Gate2":
                    Gate2 = this.gameObject.transform.GetChild(i).gameObject;
                    Gate2.GetComponent<CheckerTagScript>().Type = GradingCONSTANTS.INPUT;
                    break;
                case "OutputF":
                    OutputF = this.gameObject.transform.GetChild(i).gameObject;
                    OutputF.GetComponent<CheckerTagScript>().Type = GradingCONSTANTS.OUTPUT;
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
        CheckerTagScript InputCTag = InputC.GetComponent<CheckerTagScript>();
        CheckerTagScript Gate1Tag = Gate1.GetComponent<CheckerTagScript>();
        CheckerTagScript Gate2Tag = Gate2.GetComponent<CheckerTagScript>();
        CheckerTagScript OutputFTag = OutputF.GetComponent<CheckerTagScript>();
        if (InputATag.GetCollidingObject() == null)
        {
            Debug.Log("Switch A tags are not SNAPPED!");
       //     OnGUI();
                numTries = 0;
            
            yield break;
        }

        if( InputBTag.GetCollidingObject() == null){
            Debug.Log("Switch B tags are not SNAPPED!");
       //     OnGUI();
       numTries = 0;
            
            yield break;
        }
        if(InputCTag.GetCollidingObject() == null){
            Debug.Log("Switch C tags are not SNAPPED!");
       //     OnGUI();
       numTries = 0;
            
            yield break;
        }
        if(Gate1Tag.GetCollidingObject() == null){
            Debug.Log("AND Gate tags are not SNAPPED!");
       //     OnGUI();
       numTries = 0;
            
            yield break;
        }
        if(Gate2Tag.GetCollidingObject() == null ){
            Debug.Log("OR Gate tags are not SNAPPED!");
       //     OnGUI();
       numTries = 0;
            
            yield break;
        }
        if(OutputFTag.GetCollidingObject() == null ){
            Debug.Log("LED tags are not SNAPPED!");
       //     OnGUI();
       numTries = 0;
            
            yield break;
        }
        

        Switch InputASwitch = InputATag.GetCollidingObject().GetComponent<Switch>();
        Switch InputBSwitch = InputBTag.GetCollidingObject().GetComponent<Switch>();
        Switch InputCSwitch = InputCTag.GetCollidingObject().GetComponent<Switch>();
        ANDGate Gate1And = Gate1Tag.GetCollidingObject().GetComponent<ANDGate>();
        ORGate Gate2Or = Gate2Tag.GetCollidingObject().GetComponent<ORGate>();
        LEDScript OutputFLED = OutputFTag.GetCollidingObject().GetComponent<LEDScript>();




        //InputASwitch.ToggleSwitch(false); InputBSwitch.ToggleSwitch(false); InputCSwitch.ToggleSwitch(false); Gate1And.ClearChip(); Gate2Or.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);

    LogicNode outputB = InputBSwitch.middleNode.GetComponent<LogicNode>();
    LogicNode collidingoutputB = outputB.GetCollidingNode().GetComponent<LogicNode>();
    LogicNode outputC = InputCSwitch.middleNode.GetComponent<LogicNode>();
    LogicNode collidingoutputC = outputC.GetCollidingNode().GetComponent<LogicNode>();
    LogicNode outputA = InputASwitch.middleNode.GetComponent<LogicNode>();
    LogicNode collidingoutputA = outputA.GetCollidingNode().GetComponent<LogicNode>();
    
    

        if(collidingoutputB.logic_state == (int)LOGIC.LOW || !Gate1And.IsDeviceOn() || !Gate2Or.IsDeviceOn() || !OutputFLED.isLEDON() || collidingoutputC.logic_state == (int)LOGIC.LOW || collidingoutputA.logic_state == (int)LOGIC.LOW ){
            if(collidingoutputB.logic_state == (int)LOGIC.LOW){
            Debug.Log("This output is incorrect");
            //attempt[numTries] -= 5;
            LabOneGrade-=15;
                    
            //yield break;
            }
         if(!Gate1And.IsDeviceOn()){
            Debug.Log("This output is incorrect");
            //attempt[numTries] -= 5;
            LabOneGrade-=15;
                   
            //yield break;
            }
            if(!Gate2Or.IsDeviceOn()){
            Debug.Log("This output is incorrect");
            //attempt[numTries] -= 5;
            LabOneGrade-=15;
                    
            //yield break;
            }
            if(!OutputFLED.isLEDON()){
            Debug.Log("This output is incorrect");
            // attempt[numTries] -= 5;
            LabOneGrade-=10;
                    
            //yield break;
            }
            if(collidingoutputB.logic_state == (int)LOGIC.HIGH){
                if(collidingoutputC.logic_state == (int)LOGIC.LOW && collidingoutputA.logic_state ==(int)LOGIC.LOW){
                    Debug.Log("This output is incorrect");
                    //attempt[numTries] -= 5;
                    LabOneGrade-=5;
                    
              //      yield break;
                     }
             }
            if(collidingoutputB.logic_state == (int)LOGIC.HIGH){
                if(collidingoutputC.logic_state == (int)LOGIC.HIGH){
                    if(!OutputFLED.isLEDON()){
                    Debug.Log("This output is incorrect");
                    //attempt[numTries] -= 5;
                    LabOneGrade-=5;
                    
                //    yield break;
                    }  
                }
            }
            if(collidingoutputB.logic_state == (int)LOGIC.HIGH){
                if(collidingoutputC.logic_state == (int)LOGIC.LOW && collidingoutputA.logic_state == (int)LOGIC.HIGH){
                    if(!OutputFLED.isLEDON()){
                    Debug.Log("This output is incorrect");
                    //attempt[numTries] -= 5;
                    LabOneGrade-=5;
                    
                  //  yield break;
                    }  
                }
            }
            
           DataInsert.inputLab1Grade += LabOneGrade;
        Debug.Log("Incorrect Output, assigning partial credit");
        StartCoroutine(pushtoDataBase());
        SceneManager.LoadScene("Scenes/Postlab1");

        }
    
    

    else{
       int actualGrade1 = LabOneGrade;
        DataInsert.inputLab1Grade += actualGrade1;
        Debug.Log("Correct Output");
        StartCoroutine(pushtoDataBase());
        SceneManager.LoadScene("Scenes/Postlab1");
    }
    
    }
    
        /* else{
        int actualGrade2 = attempt.Max();
        DataInsert.inputLab1Grade += actualGrade2+80;
        Debug.Log("Incorrect Output, assigning credit based on best attempt");
        StartCoroutine(pushtoDataBase());
        SceneManager.LoadScene("Scenes/Postlab1");
        }*/
    
    

     IEnumerator pushtoDataBase(){

        string email = dbManager.email;
        int grade = DataInsert.inputLab1Grade;
        string lab = "labOne";
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



