using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Postlab4 : MonoBehaviour {

    public Button button;
    public Text text;
    public GameObject SD1;
    public GameObject SD2;
    public GameObject Co1;
    public GameObject Co2;
    public GameObject Co3;
    public GameObject Co4;
    public GameObject F1;
    public GameObject F2;
    public GameObject F3;
    public GameObject F4;

    DataInsert Grader;
    string Owner;
    int prelabGrade = 10;
    // Use this for initialization
    void Start()
    {
        GameObject grader = new GameObject("Grader");
        Grader = grader.AddComponent<DataInsert>();
        SD1 = GameObject.Find("SD 1");
        SD2 = GameObject.Find("SD 2");
        Co1 = GameObject.Find("Co 1");
        Co2 = GameObject.Find("Co 2");
        Co3 = GameObject.Find("Co 3");
        Co4 = GameObject.Find("Co 4");
        F1 = GameObject.Find("F 1");
        F2 = GameObject.Find("F 2");
        F3 = GameObject.Find("F 3");
        F4 = GameObject.Find("F 4");


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

        InputField sd1 = SD1.GetComponent<InputField>();
        InputField sd2 = SD2.GetComponent<InputField>();
        InputField co1 = Co1.GetComponent<InputField>();
        InputField co2 = Co2.GetComponent<InputField>();
        InputField co3 = Co3.GetComponent<InputField>();
        InputField co4 = Co4.GetComponent<InputField>();
        InputField f1 = F1.GetComponent<InputField>();
        InputField f2 = F2.GetComponent<InputField>();
        InputField f3 = F3.GetComponent<InputField>();
        InputField f4 = F4.GetComponent<InputField>();

        if (sd1.text == "0111" &&
            sd2.text == "0011" &&
            co1.text == "0" &&
            co2.text == "1" &&
            co3.text == "0" &&
            co4.text == "0" &&
            f1.text == "0111" &&
            f2.text == "0011" &&
            f3.text == "0011" &&
            f4.text == "0001")
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
        SceneManager.LoadScene("Scenes/StudentSubsystem");
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
