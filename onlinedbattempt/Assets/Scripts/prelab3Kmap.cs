using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class prelab3Kmap : MonoBehaviour {
	public Button button;
	public Text text;
	public GameObject b2_0;
	public GameObject b2_1;
	public GameObject b2_2;
	public GameObject b2_3;
	public GameObject b2_4;
	public GameObject b2_5;
	public GameObject b2_6;
	public GameObject b2_7;

	public GameObject b1_0;
	public GameObject b1_1;
	public GameObject b1_2;
	public GameObject b1_3;
	public GameObject b1_4;
	public GameObject b1_5;
	public GameObject b1_6;
	public GameObject b1_7;

	public GameObject b0_0;
	public GameObject b0_1;
	public GameObject b0_2;
	public GameObject b0_3;
	public GameObject b0_4;
	public GameObject b0_5;
	public GameObject b0_6;
	public GameObject b0_7;
	int PrelabGrade3p2 = 5;
	// Use this for initialization
	void Start () {
		b2_0 = GameObject.Find ("B2_0");
		b2_1 = GameObject.Find ("B2_1");
		b2_2 = GameObject.Find ("B2_2");
		b2_3 = GameObject.Find ("B2_3");
		b2_4 = GameObject.Find ("B2_4");
		b2_5 = GameObject.Find ("B2_5");
		b2_6 = GameObject.Find ("B2_6");
		b2_7 = GameObject.Find ("B2_7");

		b1_0 = GameObject.Find ("B2_0 (1)");
		b1_1 = GameObject.Find ("B2_1 (1)");
		b1_2 = GameObject.Find ("B2_2 (1)");
		b1_3 = GameObject.Find ("B2_3 (1)");
		b1_4 = GameObject.Find ("B2_4 (1)");
		b1_5 = GameObject.Find ("B2_5 (1)");
		b1_6 = GameObject.Find ("B2_6 (1)");
		b1_7 = GameObject.Find ("B2_7 (1)");

		b0_0 = GameObject.Find ("B2_0 (2)");
		b0_1 = GameObject.Find ("B2_1 (2)");
		b0_2 = GameObject.Find ("B2_2 (2)");
		b0_3 = GameObject.Find ("B2_3 (2)");
		b0_4 = GameObject.Find ("B2_4 (2)");
		b0_5 = GameObject.Find ("B2_5 (2)");
		b0_6 = GameObject.Find ("B2_6 (2)");
		b0_7 = GameObject.Find ("B2_7 (2)");

		GameObject button = GameObject.Find ("Button");
		Button btn = button.GetComponent<Button> ();
		btn.onClick.AddListener (TaskOnClick);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void TaskOnClick(){
		Debug.Log ("clicked");
		GameObject promptMessage = GameObject.Find("PromptMessage");
		Text message = promptMessage.GetComponent<Text>();
		GameObject promptMessage1 = GameObject.Find("PromptMessage (1)");
		Text message1 = promptMessage1.GetComponent<Text>();
		GameObject promptMessage2 = GameObject.Find("PromptMessage (2)");
		Text message2 = promptMessage2.GetComponent<Text>();
		GameObject promptMessage3 = GameObject.Find("PromptMessage (3)");
		Text message3 = promptMessage3.GetComponent<Text>();

		InputField b20 = b2_0.GetComponent<InputField> ();
		InputField b21 = b2_1.GetComponent<InputField> ();
		InputField b22 = b2_2.GetComponent<InputField> ();
		InputField b23 = b2_3.GetComponent<InputField> ();
		InputField b24 = b2_4.GetComponent<InputField> ();
		InputField b25 = b2_5.GetComponent<InputField> ();
		InputField b26 = b2_6.GetComponent<InputField> ();
		InputField b27 = b2_7.GetComponent<InputField> ();

		InputField b10 = b1_0.GetComponent<InputField> ();
		InputField b11 = b1_1.GetComponent<InputField> ();
		InputField b12 = b1_2.GetComponent<InputField> ();
		InputField b13 = b1_3.GetComponent<InputField> ();
		InputField b14 = b1_4.GetComponent<InputField> ();
		InputField b15 = b1_5.GetComponent<InputField> ();
		InputField b16 = b1_6.GetComponent<InputField> ();
		InputField b17 = b1_7.GetComponent<InputField> ();

		InputField b00 = b0_0.GetComponent<InputField> ();
		InputField b01 = b0_1.GetComponent<InputField> ();
		InputField b02 = b0_2.GetComponent<InputField> ();
		InputField b03 = b0_3.GetComponent<InputField> ();
		InputField b04 = b0_4.GetComponent<InputField> ();
		InputField b05 = b0_5.GetComponent<InputField> ();
		InputField b06 = b0_6.GetComponent<InputField> ();
		InputField b07 = b0_7.GetComponent<InputField> ();

		if((b20.text == "0" &&
			b21.text == "0" &&
			b22.text == "0" &&
			b23.text == "0" &&
			b24.text == "1" &&
			b25.text == "1" &&
			b26.text == "1" &&
			b27.text == "1") &&(
			b10.text == "0" &&
				b11.text == "0" &&
				b12.text == "1" &&
				b13.text == "1" &&
				b14.text == "1" &&
				b15.text == "1" &&
				b16.text == "0" &&
				b17.text == "0") &&
			(b00.text == "0" &&
				b01.text == "1" &&
				b02.text == "1" &&
				b03.text == "0" &&
				b04.text == "1" &&
				b05.text == "0" &&
				b06.text == "0" &&
				b07.text == "1")){
				message.text = "Correct!!";
			message1.text = "B2 = G2";
			message2.text = "B1 = G2'G1 + G2G1'";
			message3.text = "B0 = G2'(G1'G0+G1G0')+G2(G1G0+G1'G0)";
				DataInsert.inputPrelab3Grade += PrelabGrade3p2;
				StartCoroutine (pushtoDataBase ());
				StartCoroutine(TransitionToLab3());
			}
			else{
				message.text = "There is one mistake or more. \n please check work";
				if(PrelabGrade3p2 > 0){
					PrelabGrade3p2--;}
			}
			}private IEnumerator TransitionToLab3(){
				yield return new WaitForSecondsRealtime (5);
				SceneManager.LoadScene ("Scenes/Lab3");
				yield break;}
			IEnumerator pushtoDataBase(){

				string email = dbManager.email;
				int grade = DataInsert.inputPrelab3Grade;
				StudentSubsystem.prelab3grade +=grade; //this updates the grade shown in studentsubsystem

				string lab = "preLabThree";
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
