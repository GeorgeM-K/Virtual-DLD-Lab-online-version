// written by: Dhruvik Patel
// tested by: Dhruvik Patel
// debugged by: Dhruvik Patel, Khalid Akash
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PostLab1 : MonoBehaviour {

    public Button button;
    public Text text;
    public GameObject inputfield0000;
    public GameObject inputfield0001;
    public GameObject inputfield0010;
    public GameObject inputfield0011;
    public GameObject inputfield0100;
    public GameObject inputfield0101;
    public GameObject inputfield0110;
    public GameObject inputfield0111;
    public GameObject inputfield1000;
    public GameObject inputfield1001;
    public GameObject inputfield1010;
    public GameObject inputfield1011;
    public GameObject inputfield1100;
    public GameObject inputfield1101;
    public GameObject inputfield1110;
    public GameObject inputfield1111;
    int postLabGrade = 10;
    DataInsert dataInsert;
    // Use this for initialization
    void Start () {
        GameObject dbConn = new GameObject("dbConn");
        dataInsert = dbConn.AddComponent<DataInsert>();
        GameObject button00 = GameObject.Find("Button");
        Button btn = button00.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void TaskOnClick()
    {
        Debug.Log("Clicked");
        GameObject promptMessage = GameObject.Find("PromptMessage");
        Text message = promptMessage.GetComponent<Text>();

        GameObject reducedFucntionMessage = GameObject.Find("ReducedFunctionMessage");
        Text function = reducedFucntionMessage.GetComponent<Text>();
		GameObject er1 = GameObject.Find ("err1");
		GameObject er3 = GameObject.Find ("err3");
		GameObject er5 = GameObject.Find ("err5");
		GameObject er7 = GameObject.Find ("err7");
		GameObject er9 = GameObject.Find ("err9");
		GameObject er14 = GameObject.Find ("err14");
		GameObject er11 = GameObject.Find ("err11");
		GameObject er13 = GameObject.Find ("err13");
		GameObject er15 = GameObject.Find ("err15");
		Text error1 = er1.GetComponent<Text> ();
		Text error3 = er3.GetComponent<Text> ();
		Text error5 = er5.GetComponent<Text> ();
		Text error7 = er7.GetComponent<Text> ();
		Text error9 = er9.GetComponent<Text> ();
		Text error14 = er14.GetComponent<Text> ();
		Text error11= er11.GetComponent<Text> ();
		Text error13 = er13.GetComponent<Text> ();
		Text error15 = er15.GetComponent<Text> ();
        InputField field0000 = inputfield0000.GetComponent<InputField>();
        InputField field0001 = inputfield0001.GetComponent<InputField>();
        InputField field0010 = inputfield0010.GetComponent<InputField>();
        InputField field0011 = inputfield0011.GetComponent<InputField>();
        InputField field0100 = inputfield0100.GetComponent<InputField>();
        InputField field0101 = inputfield0101.GetComponent<InputField>();
        InputField field0110 = inputfield0110.GetComponent<InputField>();
        InputField field0111 = inputfield0111.GetComponent<InputField>();
        InputField field1000 = inputfield1000.GetComponent<InputField>();
        InputField field1001 = inputfield1001.GetComponent<InputField>();
        InputField field1010 = inputfield1010.GetComponent<InputField>();
        InputField field1011 = inputfield1011.GetComponent<InputField>();
        InputField field1100 = inputfield1100.GetComponent<InputField>();
        InputField field1101 = inputfield1101.GetComponent<InputField>();
        InputField field1110 = inputfield1110.GetComponent<InputField>();
        InputField field1111 = inputfield1111.GetComponent<InputField>();

		if (field0000.text == "0" &&
		    field0001.text == "1" &&
		    field0010.text == "0" &&
		    field0011.text == "1" &&
		    field0100.text == "0" &&
		    field0101.text == "1" &&
		    field0110.text == "0" &&
		    field0111.text == "1" &&
		    field1000.text == "0" &&
		    field1001.text == "1" &&
		    field1010.text == "0" &&
		    field1011.text == "1" &&
		    field1100.text == "0" &&
		    field1101.text == "1" &&
		    field1110.text == "0" &&
		    field1111.text == "1") {
			DataInsert.inputPostlab1Grade = postLabGrade;
			dataInsert.InsertStudentGrade (DataInsert.inputStudent, DataInsert.inputPassword, DataInsert.inputLab1Grade, DataInsert.inputLab2Grade);
			message.text = "";
			function.text = "That's Right!\t" +
			"The reduced function can be now be found, which in this case is: F = D";
			StartCoroutine (pushtoDataBase ());
			StartCoroutine (TransitionToStudentSubsystem ());
		} else if (field0001.text != "1") {
			message.text = "There is one or more mistake(s), try again.";
			error1.text = "X";
			postLabGrade--;
		} else if (field0011.text != "1") {
			message.text = "There is one or more mistake(s), try again.";
			error3.text = "X";
			postLabGrade--;
		} else if (field0101.text != "1") {
			message.text = "There is one or more mistake(s), try again.";
			error5.text = "X";
			postLabGrade--;
		} else if (field0111.text !="1") {
			message.text = "There is one or more mistake(s), try again.";
			error7.text = "X";
			postLabGrade--;
		} else if (field1001.text != "1") {
			message.text = "There is one or more mistake(s), try again.";
			error9.text = "X";
			postLabGrade--;
		} else if (field1011.text != "1") {
			message.text = "There is one or more mistake(s), try again.";
			error11.text = "X";
			postLabGrade--;
		} else if (field1101.text != "1") {
			message.text = "There is one or more mistake(s), try again.";
			error13.text = "X";
			postLabGrade--;
		}
		else if(field1111.text != "1"){
			message.text = "There is one or more mistake(s), try again.";
			error15.text = "X";
			postLabGrade--;

    }
	}

    private IEnumerator TransitionToStudentSubsystem()
    {
        yield return new WaitForSecondsRealtime(5);
        SceneManager.LoadScene("Scenes/StudentSubsystem");
        yield break;
    }
     IEnumerator pushtoDataBase(){

        string email = dbManager.email;
        int grade = DataInsert.inputPostlab1Grade;
        string lab = "postLabOne";
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
}
