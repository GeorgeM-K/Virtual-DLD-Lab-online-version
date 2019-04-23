// written by: Khalid Akash, Joe Cella
// tested by: Khalid Akash
// debugged by: Khalid Akash
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StudentSubsystem : MonoBehaviour {

    public Button SandboxMode, Lab1Button, QuizzesButton, Lab3Button, Lab2Button, Lab4Button, CameraButton, LeaderboardButton, LogoutButton, AboutButton;
    public Text Lab1Avg, Lab2Avg, currScore1, currScore2, username;

    DataInsert dataInsert;
	public static int prelab1grade, prelab2grade, prelab3grade, prelab4grade;
	// Use this for initialization
    void Start () {
        dataInsert = new DataInsert();
        Debug.Log("Username: " + DataInsert.inputStudent + " Password: " 
            + DataInsert.inputPassword + " Lab1: " + DataInsert.inputLab1Grade 
            + " Lab2: " + DataInsert.inputLab2Grade);
        SandboxMode.onClick.AddListener(EnterSandboxMode);
        Lab1Button.onClick.AddListener(EnterLab1);
        Lab2Button.onClick.AddListener(EnterLab2);
		Lab3Button.onClick.AddListener(EnterLab3);
        Lab4Button.onClick.AddListener(EnterLab4);
		QuizzesButton.onClick.AddListener(EnterQuizzes);
        CameraButton.onClick.AddListener(EnterCamera);
        LogoutButton.onClick.AddListener(Logout);
        AboutButton.onClick.AddListener(EnterAbout);
        LeaderboardButton.onClick.AddListener(LeaderboardButtonClick);

		GameObject currentScore1 = GameObject.Find ("currScore1");
		Text Score1 = currentScore1.GetComponent<Text> ();
		Score1.text = prelab1grade.ToString ();

		GameObject currentScore2 = GameObject.Find ("currScore2");
		Text Score2 = currentScore2.GetComponent<Text> ();
		Score2.text = prelab2grade.ToString ();

		GameObject currentScore3 = GameObject.Find ("currScore3");
		Text Score3 = currentScore3.GetComponent<Text> ();
		Score3.text = prelab3grade.ToString ();

        Lab1Avg = GameObject.Find("Lab1Avg").GetComponent<Text>();
        Lab2Avg = GameObject.Find("Lab2Avg").GetComponent<Text>();
        
		//currScore1.text = prelab1grade.ToString ();
		//currScore2 = GameObject.Find("currScore2").GetComponent<Text>();
        username = GameObject.Find("Username").GetComponent<Text>();
        currScore1.text = DataInsert.inputLab1Grade + "";
        currScore2.text = DataInsert.inputLab2Grade + "";
        Lab1Avg.text = DataInsert.lab1avg + "";
        Lab2Avg.text = DataInsert.lab2avg + "";
        username.text = DataInsert.inputStudent;

    }
	
    private void EnterSandboxMode()
    {
        Debug.Log("Sandbox Button Clicked");
        SceneManager.LoadScene("Scenes/SandboxLab");
    }

    private void EnterLab1()
    {
        Debug.Log("Lab 1 Button Clicked");
		if (StudentSubsystem.prelab1grade > 0) {
			SceneManager.LoadScene ("Scenes/Lab1");
		} else {
			SceneManager.LoadScene ("Scenes/Prelab1");
		}
    }

    private void EnterLab2()
	{
		Debug.Log ("Lab 2 Button Clicked");
		if (StudentSubsystem.prelab2grade > 0) {
			SceneManager.LoadScene ("Scenes/Lab2");
		} else {
			SceneManager.LoadScene ("Scenes/Prelab2");
		}
	}

	private void EnterLab3()
	{
		Debug.Log("Lab 3 Button Clicked");
		if (StudentSubsystem.prelab3grade > 0) {
			SceneManager.LoadScene ("Scenes/Lab3");
		} else {
			SceneManager.LoadScene ("Scenes/Prelab3");
		}
	}

    private void EnterLab4()
    {
        Debug.Log("Lab 4 Button Clicked");
        SceneManager.LoadScene("Scenes/Prelab4");
    }

	private void EnterQuizzes()
	{
		Debug.Log ("Quizzes Button Clicked");
		SceneManager.LoadScene("Scenes/QuizMenu");
	}

    private void EnterCamera()
    {
        Debug.Log("Camera Button Clicked");
        SceneManager.LoadScene("Scenes/Camera");
    }

    private void EnterChat()
    {
        Debug.Log("Chat button clicked");
        
    }

    private void Logout()
    {
        Debug.Log("Logout Button Clicked");
        SceneManager.LoadScene("Scenes/Login");
    }

    private void LeaderboardButtonClick()
    {
        Debug.Log("Leaderboard button clicked");
        SceneManager.LoadScene("Scenes/Leaderboards");
    }

    private void EnterAbout()
    {
        Debug.Log("About button clicked");
        SceneManager.LoadScene("Scenes/About");
    }
    // Update is called once per frame
    void Update () {
		
	}
}
