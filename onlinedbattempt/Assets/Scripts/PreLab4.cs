// written by: Dhruvik Patel
// tested by: Dhruvik Patel
// debugged by: Dhruvik Patel, Khalid Akash
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreLab4 : MonoBehaviour
{

    public Button button;
    public Text text;
    public GameObject Sum1;
    public GameObject Sum2;
    public GameObject Sum3;
    public GameObject Sum4;
    public GameObject Carry1;
    public GameObject Carry2;
    public GameObject Carry3;
    public GameObject Carry4;

    DataInsert Grader;
    string Owner;
    int prelabGrade = 10;
    // Use this for initialization
    void Start()
    {
        GameObject grader = new GameObject("Grader");
        Grader = grader.AddComponent<DataInsert>();
        Sum1 = GameObject.Find("Sum1");
        Sum2 = GameObject.Find("Sum2");
        Sum3 = GameObject.Find("Sum3");
        Sum4 = GameObject.Find("Sum4");
        Carry1 = GameObject.Find("Carry1");
        Carry2 = GameObject.Find("Carry2");
        Carry3 = GameObject.Find("Carry3");
        Carry4 = GameObject.Find("Carry4");


        GameObject button1 = GameObject.Find("Button");
        Button btn = button1.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TaskOnClick()
    {
        Debug.Log("Clicked");
        GameObject promptMessage = GameObject.Find("PromptMessage");
        Text message = promptMessage.GetComponent<Text>();

        InputField s1 = Sum1.GetComponent<InputField>();
        InputField s2 = Sum2.GetComponent<InputField>();
        InputField s3 = Sum3.GetComponent<InputField>();
        InputField s4 = Sum4.GetComponent<InputField>();
        InputField c1 = Carry1.GetComponent<InputField>();
        InputField c2 = Carry2.GetComponent<InputField>();
        InputField c3 = Carry3.GetComponent<InputField>();
        InputField c4 = Carry4.GetComponent<InputField>();

        if (s1.text == "0" &&
            s2.text == "1" &&
            s3.text == "1" &&
            s4.text == "0" &&
            c1.text == "0" &&
            c2.text == "0" &&
            c3.text == "0" &&
            c4.text == "1")
        {
            message.text = "That's right!";
            DataInsert.inputPrelab4Grade = prelabGrade;
            StartCoroutine(pushtoDataBase());
            StartCoroutine(TransitionToLab1());
        }
        else
        {
            message.text = "That's wrong. " +
                "Try again.";
            if (prelabGrade > 0)
            {
                prelabGrade--;
            }
        }

    }
    private IEnumerator TransitionToLab1()
    {
        yield return new WaitForSecondsRealtime(5);
        SceneManager.LoadScene("Scenes/Lab 4");
        yield break;
    }

    IEnumerator pushtoDataBase()
    {

        string email = dbManager.email;
        int grade = DataInsert.inputPrelab4Grade;
        string lab = "preLabFour";
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
