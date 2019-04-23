// written by: Dhruvik Patel
// tested by: Dhruvik Patel
// debugged by: Dhruvik Patel, Khalid Akash
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreLab1 : MonoBehaviour {

    public Button button;
    public Text text;
    public GameObject inputfield000;
    public GameObject inputfield001;
    public GameObject inputfield010;
    public GameObject inputfield011;
    public GameObject inputfield100;
    public GameObject inputfield101;
    public GameObject inputfield110;
    public GameObject inputfield111;
    DataInsert Grader;
    string Owner;
    int prelabGrade = 10;
    // Use this for initialization
    void Start () {
        GameObject grader = new GameObject("Grader");
        Grader = grader.AddComponent<DataInsert>();
        inputfield000 = GameObject.Find("InputField (000)");
        inputfield001 = GameObject.Find("InputField (001)");
        inputfield010 = GameObject.Find("InputField (010)");
        inputfield011 = GameObject.Find("InputField (011)");
        inputfield100 = GameObject.Find("InputField (100)");
        inputfield101 = GameObject.Find("InputField (101)");
        inputfield110 = GameObject.Find("InputField (110)");
        inputfield111 = GameObject.Find("InputField (111)");

        GameObject button1 = GameObject.Find("Button");
        Button btn = button1.GetComponent<Button>();
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


		GameObject er0 = GameObject.Find ("err0");
		GameObject er1 = GameObject.Find ("err1");
		GameObject er2 = GameObject.Find ("err2");
		GameObject er3 = GameObject.Find ("err3");
		GameObject er4 = GameObject.Find ("err4");
		GameObject er5 = GameObject.Find ("err5");
		GameObject er6 = GameObject.Find ("err6");
		GameObject er7 = GameObject.Find ("err7");
		Text error0 = er0.GetComponent<Text> ();
		Text error1 = er1.GetComponent<Text> ();
		Text error2 = er2.GetComponent<Text> ();
		Text error3 = er3.GetComponent<Text> ();
		Text error4 = er4.GetComponent<Text> ();
		Text error5 = er5.GetComponent<Text> ();
		Text error6 = er6.GetComponent<Text> ();
		Text error7 = er7.GetComponent<Text> ();
        InputField field000 = inputfield000.GetComponent<InputField>();
        InputField field001 = inputfield001.GetComponent<InputField>();
        InputField field010 = inputfield010.GetComponent<InputField>();
        InputField field011 = inputfield011.GetComponent<InputField>();
        InputField field100 = inputfield100.GetComponent<InputField>();
        InputField field101 = inputfield101.GetComponent<InputField>();
        InputField field110 = inputfield110.GetComponent<InputField>();
        InputField field111 = inputfield111.GetComponent<InputField>();

		if (field000.text == "0" &&
		    field001.text == "0" &&
		    field010.text == "0" &&
		    field011.text == "1" &&
		    field100.text == "0" &&
		    field101.text == "0" &&
		    field110.text == "1" &&
		    field111.text == "1") {
			message.text = "That's right! Total score is = " + prelabGrade;
			DataInsert.inputPrelab1Grade = prelabGrade;
			StartCoroutine (pushtoDataBase());
			StartCoroutine (TransitionToLab1());
		} else if (field000.text != "0") {
			message.text = "There is one or more mistake(s), try again.";
			error0.text = "X";
			prelabGrade--;
		} else if (
		           field001.text != "0" 
		           ) {
			message.text = "There is one or more mistake(s), try again.";
			error1.text = "X";
			prelabGrade--;
		} else if (
		           field010.text != "0" 
		           ) {
			message.text = "There is one or more mistake(s), try again.";
			error2.text = "X";
			prelabGrade--;
		} else if (
		           field011.text != "1" 
		           ) {
			message.text = "There is one or more mistake(s), try again.";
			error3.text = "X";
			prelabGrade--;
		} else if (
		           field100.text != "0" 
		          ) {
			message.text = "There is one or more mistake(s), try again.";
			error4.text = "X";
			prelabGrade--;
		} else if (
		           field101.text != "0" 
		           ) {
			message.text = "There is one or more mistake(s), try again.";
			error5.text = "X";
			prelabGrade--;
		} else if (
		        field110.text != "1" 
		        ) {
			message.text = "There is one or more mistake(s), try again.";
			error6.text = "X";
			prelabGrade--;
		} else if (
		        field111.text != "1") {
			message.text = "There is one or more mistake(s), try again.";
			error7.text = "X";
			prelabGrade--;
		}
	}

    private IEnumerator TransitionToLab1()
    {
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene("Scenes/Lab1");
        yield break;
    }

     IEnumerator pushtoDataBase(){

        string email = dbManager.email;
        int grade = DataInsert.inputPrelab1Grade;
		StudentSubsystem.prelab1grade =grade; //this updates the grade shown in studentsubsystem

        string lab = "preLabOne";
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
