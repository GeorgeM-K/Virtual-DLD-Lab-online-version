using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Postlab4Test : MonoBehaviour {

    public GameObject sum1;
    public GameObject sum2;
    public GameObject carry1;
    public GameObject carry2;
    public GameObject carry3;
    public GameObject carry4;
    public GameObject F1;
    public GameObject F2;
    public GameObject F3;
    public GameObject F4;

    [UnityTest]
    public IEnumerator Input1()
    {

        SetupScene();

        yield return new WaitForSecondsRealtime(1);

        GameObject promptMessage = GameObject.Find("PromptMessage");
        Text message = promptMessage.GetComponent<Text>();

        GameObject button1 = GameObject.Find("Button");
        Button btn = button1.GetComponent<Button>();

        sum1 = GameObject.Find("SD 1");
        sum2 = GameObject.Find("SD 2");
        carry1 = GameObject.Find("Co 1");
        carry2 = GameObject.Find("Co 2");
        carry3 = GameObject.Find("Co 3");
        carry4 = GameObject.Find("Co 4");
        F1= GameObject.Find("F 1");
        F2= GameObject.Find("F 2");
        F3= GameObject.Find("F 3");
        F4= GameObject.Find("F 4");

        InputField[] input = new InputField[10];

        input[0] = sum1.GetComponent<InputField>();
        input[1] = sum2.GetComponent<InputField>();
        input[2] = carry1.GetComponent<InputField>();
        input[3] = carry2.GetComponent<InputField>();
        input[4] = carry3.GetComponent<InputField>();
        input[5] = carry4.GetComponent<InputField>();
        input[6] = F1.GetComponent<InputField>();
        input[7] = F2.GetComponent<InputField>();
        input[8] = F3.GetComponent<InputField>();
        input[9] = F4.GetComponent<InputField>();



        input[0].text = "0000";
        input[1].text = "0000";
        input[2].text = "0";
        input[3].text = "0";
        input[4].text = "0";
        input[5].text = "0";
        input[6].text = "0000";
        input[7].text = "0000";
        input[8].text = "0000";
        input[9].text = "0000";

        btn.onClick.Invoke();

        if (input[0].text == "0111" &&
           input[1].text == "0011" &&
           input[2].text == "0" &&
           input[3].text == "1" &&
           input[4].text == "0" &&
           input[5].text == "0" &&
           input[6].text == "0111" &&
           input[7].text == "0011" &&
           input[8].text == "0011" &&
           input[9].text == "0001")
        { 
            yield break;
        }
        else if (message.text == "That's wrong. " + "Try again.")
        {
            yield return new WaitForSeconds(1);
            yield break;
        }

        Assert.Fail();

    }

    [UnityTest]
    public IEnumerator Input2()
    {

        SetupScene();

        yield return new WaitForSecondsRealtime(1);

        GameObject promptMessage = GameObject.Find("PromptMessage");
        Text message = promptMessage.GetComponent<Text>();

        GameObject button1 = GameObject.Find("Button");
        Button btn = button1.GetComponent<Button>();

        sum1 = GameObject.Find("SD 1");
        sum2 = GameObject.Find("SD 2");
        carry1 = GameObject.Find("Co 1");
        carry2 = GameObject.Find("Co 2");
        carry3 = GameObject.Find("Co 3");
        carry4 = GameObject.Find("Co 4");
        F1 = GameObject.Find("F 1");
        F2 = GameObject.Find("F 2");
        F3 = GameObject.Find("F 3");
        F4 = GameObject.Find("F 4");

        InputField[] input = new InputField[10];

        input[0] = sum1.GetComponent<InputField>();
        input[1] = sum2.GetComponent<InputField>();
        input[2] = carry1.GetComponent<InputField>();
        input[3] = carry2.GetComponent<InputField>();
        input[4] = carry3.GetComponent<InputField>();
        input[5] = carry4.GetComponent<InputField>();
        input[6] = F1.GetComponent<InputField>();
        input[7] = F2.GetComponent<InputField>();
        input[8] = F3.GetComponent<InputField>();
        input[9] = F4.GetComponent<InputField>();



        input[0].text = "1111";
        input[1].text = "1111";
        input[2].text = "1";
        input[3].text = "1";
        input[4].text = "1";
        input[5].text = "1";
        input[6].text = "1111";
        input[7].text = "1111";
        input[8].text = "1111";
        input[9].text = "1111";

        btn.onClick.Invoke();

        if (input[0].text == "0111" &&
           input[1].text == "0011" &&
           input[2].text == "0" &&
           input[3].text == "1" &&
           input[4].text == "0" &&
           input[5].text == "0" &&
           input[6].text == "0111" &&
           input[7].text == "0011" &&
           input[8].text == "0011" &&
           input[9].text == "0001")
        {
            yield break;
        }
        else if (message.text == "That's wrong. " + "Try again.")
        {
            yield return new WaitForSeconds(1);
            yield break;
        }

        Assert.Fail();

    }


    [UnityTest]
    public IEnumerator Input3()
    {

        SetupScene();

        yield return new WaitForSecondsRealtime(1);

        GameObject promptMessage = GameObject.Find("PromptMessage");
        Text message = promptMessage.GetComponent<Text>();

        GameObject button1 = GameObject.Find("Button");
        Button btn = button1.GetComponent<Button>();

        sum1 = GameObject.Find("SD 1");
        sum2 = GameObject.Find("SD 2");
        carry1 = GameObject.Find("Co 1");
        carry2 = GameObject.Find("Co 2");
        carry3 = GameObject.Find("Co 3");
        carry4 = GameObject.Find("Co 4");
        F1 = GameObject.Find("F 1");
        F2 = GameObject.Find("F 2");
        F3 = GameObject.Find("F 3");
        F4 = GameObject.Find("F 4");

        InputField[] input = new InputField[10];

        input[0] = sum1.GetComponent<InputField>();
        input[1] = sum2.GetComponent<InputField>();
        input[2] = carry1.GetComponent<InputField>();
        input[3] = carry2.GetComponent<InputField>();
        input[4] = carry3.GetComponent<InputField>();
        input[5] = carry4.GetComponent<InputField>();
        input[6] = F1.GetComponent<InputField>();
        input[7] = F2.GetComponent<InputField>();
        input[8] = F3.GetComponent<InputField>();
        input[9] = F4.GetComponent<InputField>();



        input[0].text = "0111";
        input[1].text = "0011";
        input[2].text = "0";
        input[3].text = "1";
        input[4].text = "0";
        input[5].text = "0";
        input[6].text = "0111";
        input[7].text = "0011";
        input[8].text = "0011";
        input[9].text = "0000";

        btn.onClick.Invoke();

        if (input[0].text == "0111" &&
           input[1].text == "0011" &&
           input[2].text == "0" &&
           input[3].text == "1" &&
           input[4].text == "0" &&
           input[5].text == "0" &&
           input[6].text == "0111" &&
           input[7].text == "0011" &&
           input[8].text == "0011" &&
           input[9].text == "0001")
        {
            yield break;
        }
        else if (message.text == "That's wrong. " + "Try again.")
        {
            yield return new WaitForSeconds(1);
            yield break;
        }

        Assert.Fail();

    }


    private void SetupScene()
    {
        SceneManager.LoadScene("Scenes/Postlab 4");
    }

}
