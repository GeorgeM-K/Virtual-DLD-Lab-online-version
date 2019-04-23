using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class prelab2Kmap : MonoBehaviour {
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
	int Prelab2Gradep1 = 10;
	DataInsert Grader;
	// Use this for initialization
	void Start () {
		GameObject grader = new GameObject("Grader");
		Grader = grader.AddComponent<DataInsert>();
		inputfield0000 = GameObject.Find ("InputField (0000)");
		inputfield0001 = GameObject.Find ("InputField (0001)");
		inputfield0010 = GameObject.Find ("InputField (0010)");
		inputfield0011 = GameObject.Find ("InputField (0011)");
		inputfield0100 = GameObject.Find ("InputField (0100)");
		inputfield0101 = GameObject.Find ("InputField (0101)");
		inputfield0110 = GameObject.Find ("InputField (0110)");
		inputfield0111 = GameObject.Find ("InputField (0111)");
		inputfield1000 = GameObject.Find ("InputField (1000)");
		inputfield1001 = GameObject.Find ("InputField (1001)");
		inputfield1010 = GameObject.Find ("InputField (1010)");
		inputfield1011 = GameObject.Find ("InputField (1011)");
		inputfield1100 = GameObject.Find ("InputField (1100)");
		inputfield1101 = GameObject.Find ("InputField (1101)");
		inputfield1110 = GameObject.Find ("InputField (1110)");
		inputfield1111 = GameObject.Find ("InputField (1111)");

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
		GameObject er6 = GameObject.Find ("err6");
		GameObject er7 = GameObject.Find ("err7");
		GameObject er14 = GameObject.Find ("err14");
		Text error1 = er1.GetComponent<Text> ();
		Text error3 = er3.GetComponent<Text> ();
		Text error5 = er5.GetComponent<Text> ();
		Text error6 = er6.GetComponent<Text> ();
		Text error7 = er7.GetComponent<Text> ();
		Text error14 = er14.GetComponent<Text> ();
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
		    field0110.text == "1" &&
		    field0111.text == "1" &&
		    field1000.text == "0" &&
		    field1001.text == "0" &&
		    field1010.text == "0" &&
		    field1011.text == "0" &&
		    field1100.text == "0" &&
		    field1101.text == "0" &&
		    field1110.text == "1" &&
		    field1111.text == "0") {
			DataInsert.inputPrelab2Grade = Prelab2Gradep1;
			StartCoroutine (pushtoDataBase ());
			message.text = "";
			function.text = "That's Right!\t" +
			"The reduced function can be now be found, which in this case is: F = A'D + BCD'\n your score is = " + Prelab2Gradep1 +"/6";
			StartCoroutine (pushtoDataBase ());
			StartCoroutine (TransitionToPrelab2 ());
		} else if (field0001.text != "1") {
			message.text = "There is one or more mistake(s), try again.";
			error1.text = "X";	
			Prelab2Gradep1--;
		} else if (field0011.text != "1") {
			message.text = "There is one or more mistake(s), try again.";
			error3.text = "X";	
			Prelab2Gradep1--;
		} else if (field0101.text != "1") {
			message.text = "There is one or more mistake(s), try again.";
			error5.text = "X";	
			Prelab2Gradep1--;
		} else if (field0110.text != "1") {
			message.text = "There is one or more mistake(s), try again.";
			error6.text = "X";	
			Prelab2Gradep1--;
		} else if (field0111.text != "1") {
			message.text = "There is one or more mistake(s), try again.";
			error7.text = "X";	
			Prelab2Gradep1--;
		} else if (field1110.text != "1") {
			message.text = "There is one or more mistake(s), try again.";
			error14.text = "X";	
			Prelab2Gradep1--;
		}

	}

	private IEnumerator TransitionToPrelab2()
	{
		yield return new WaitForSecondsRealtime(5);
		SceneManager.LoadScene("Scenes/StudentSubSystem");
		yield break;
	}
	IEnumerator pushtoDataBase(){

		string email = dbManager.email;
		int grade = DataInsert.inputPostlab2Grade;
		string lab = "postLabTwo";
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
