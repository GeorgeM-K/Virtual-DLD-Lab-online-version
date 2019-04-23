// written by: Deeptanshu Murdeshwar
// tested by: Deeptanshu Murdeshwar
// debugged by: Deeptanshu Murdeshwar
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Prelab3 : MonoBehaviour {

	private Button button;
	private Text text;
	public GameObject inputfield000;
	public GameObject inputfield001;
	public GameObject inputfield010;
	public GameObject inputfield011;
	public GameObject inputfield100;
	public GameObject inputfield101;
	public GameObject inputfield110;
	public GameObject inputfield111;
	public GameObject inputfield8;
	public GameObject inputfield1;
	public GameObject inputfield2;
	public GameObject inputfield3;
	public GameObject inputfield4;
	public GameObject inputfield5;
	public GameObject inputfield6;
	public GameObject inputfield7;
	int prelab3Grade = 5;
	// Use this for initialization
	void Start () {

		inputfield000 = GameObject.Find("InputField (000)");
		inputfield001 = GameObject.Find("InputField (001)");
		inputfield010 = GameObject.Find("InputField (010)");
		inputfield011 = GameObject.Find("InputField (011)");
		inputfield100 = GameObject.Find("InputField (100)");
		inputfield101 = GameObject.Find("InputField (101)");
		inputfield110 = GameObject.Find("InputField (110)");
		inputfield111 = GameObject.Find("InputField (111)");

		inputfield1 = GameObject.Find("InputField (1)");
		inputfield2 = GameObject.Find("InputField (2)");
		inputfield3 = GameObject.Find("InputField (3)");
		inputfield4 = GameObject.Find("InputField (4)");
		inputfield5 = GameObject.Find("InputField (5)");
		inputfield6 = GameObject.Find("InputField (6)");
		inputfield7 = GameObject.Find("InputField (7)");
		inputfield8 = GameObject.Find("InputField (8)");
		GameObject button1 = GameObject.Find("Button");
		Button btn = button1.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	// Update is called once per frame
	void Update () {

	}

	void TaskOnClick()
	{
		Debug.Log ("Clicked");
		GameObject promptMessage = GameObject.Find("PromptMessage");
		Text message = promptMessage.GetComponent<Text>();

		InputField field000 = inputfield000.GetComponent<InputField>();
		InputField field001 = inputfield001.GetComponent<InputField>();
		InputField field010 = inputfield010.GetComponent<InputField>();
		InputField field011 = inputfield011.GetComponent<InputField>();
		InputField field100 = inputfield100.GetComponent<InputField>();
		InputField field101 = inputfield101.GetComponent<InputField>();
		InputField field110 = inputfield110.GetComponent<InputField>();
		InputField field111 = inputfield111.GetComponent<InputField>();
		InputField field8 = inputfield8.GetComponent<InputField> ();
		InputField field1 = inputfield1.GetComponent<InputField> ();
		InputField field2 = inputfield2.GetComponent<InputField> ();
		InputField field3 = inputfield3.GetComponent<InputField> ();
		InputField field4 = inputfield4.GetComponent<InputField> ();
		InputField field5 = inputfield5.GetComponent<InputField> ();
		InputField field6 = inputfield6.GetComponent<InputField> ();
		InputField field7 = inputfield7.GetComponent<InputField> ();

		if(field000.text == "000" &&
			field001.text == "001" &&
			field010.text == "011" &&
			field011.text == "010" &&
			field100.text == "110" &&
			field101.text == "111" &&
			field110.text == "101" &&
			field111.text == "100"&&
			field8.text == "4" && 
			field1.text == "0"&&
			field2.text == "1" &&
			field3.text == "3"&&
			field4.text == "2"&&
			field5.text == "6"&&
			field6.text == "7"&&
			field7.text == "5"){
			message.text = "That's right!";
			DataInsert.inputPrelab3Grade = prelab3Grade;
			StartCoroutine (pushtoDataBase ());
			StartCoroutine (TransitionTopreLab3p2 ());
		}
		else
		{
			message.text = "That's wrong. " +
				"Try again."+"\nhint** check lab manual";
				if(prelab3Grade > 0)
			{
				prelab3Grade--;
			}
		}
	}
	private IEnumerator TransitionTopreLab3p2(){
		yield return new WaitForSecondsRealtime (5);
		SceneManager.LoadScene ("Scenes/prelab3Kmap");
		yield break;
	}
	IEnumerator pushtoDataBase(){

		string email = dbManager.email;
		int grade = DataInsert.inputPrelab3Grade;
		StudentSubsystem.prelab3grade = grade; //this updates the grade shown in studentsubsystem

		string lab = "preLabThree";
		//preLabOne
		//postLabOne

		WWWForm form = new WWWForm ();
		form.AddField ("email", email);
		form.AddField ("grade", grade);
		form.AddField ("Lab", lab);

		WWW www = new WWW ("https://dldvirtuallab.000webhostapp.com/labGrade.php", form);
		yield return www;

		if (www.text == "0") {
			Debug.Log ("Grade entered successfully");
		} else {
			Debug.Log ("Error" + www.text);
		}
	}
}