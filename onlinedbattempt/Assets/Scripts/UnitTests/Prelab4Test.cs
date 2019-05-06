using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Prelab4Test : MonoBehaviour
{

    public GameObject sum1;
    public GameObject sum2;
    public GameObject sum3;
    public GameObject sum4;
    public GameObject carry1;
    public GameObject carry2;
    public GameObject carry3;
    public GameObject carry4;


    [UnityTest]
    public IEnumerator Input1()
    {

        SetupScene();

        yield return new WaitForSecondsRealtime(1);

        GameObject promptMessage = GameObject.Find("PromptMessage");
        Text message = promptMessage.GetComponent<Text>();

        GameObject button1 = GameObject.Find("Button");
        Button btn = button1.GetComponent<Button>();

        sum1 = GameObject.Find("Sum1");
        sum2 = GameObject.Find("Sum2");
        sum3 = GameObject.Find("Sum3");
        sum4 = GameObject.Find("Sum4");
        carry1 = GameObject.Find("Carry1");
        carry2 = GameObject.Find("Carry2");
        carry3 = GameObject.Find("Carry3");
        carry4 = GameObject.Find("Carry4");

        InputField[] input = new InputField[8];

        input[0] = sum1.GetComponent<InputField>();
        input[1] = sum2.GetComponent<InputField>();
        input[2] = sum3.GetComponent<InputField>();
        input[3] = sum4.GetComponent<InputField>();
        input[4] = carry1.GetComponent<InputField>();
        input[5] = carry2.GetComponent<InputField>();
        input[6] = carry3.GetComponent<InputField>();
        input[7] = carry4.GetComponent<InputField>();


        input[0].text = "1";
        input[1].text = "1";
        input[2].text = "1";
        input[3].text = "0";
        input[4].text = "0";
        input[5].text = "0";
        input[6].text = "0";
        input[7].text = "1";

        btn.onClick.Invoke();

        if (input[0].text == "0" &&
           input[1].text == "1" &&
           input[2].text == "1" &&
           input[3].text == "0" &&
           input[4].text == "0" &&
           input[5].text == "0" &&
           input[6].text == "0" &&
           input[7].text == "1" &&
           message.text == "That's right!")
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

        sum1 = GameObject.Find("Sum1");
        sum2 = GameObject.Find("Sum2");
        sum3 = GameObject.Find("Sum3");
        sum4 = GameObject.Find("Sum4");
        carry1 = GameObject.Find("Carry1");
        carry2 = GameObject.Find("Carry2");
        carry3 = GameObject.Find("Carry3");
        carry4 = GameObject.Find("Carry4");

        InputField[] input = new InputField[8];

        input[0] = sum1.GetComponent<InputField>();
        input[1] = sum2.GetComponent<InputField>();
        input[2] = sum3.GetComponent<InputField>();
        input[3] = sum4.GetComponent<InputField>();
        input[4] = carry1.GetComponent<InputField>();
        input[5] = carry2.GetComponent<InputField>();
        input[6] = carry3.GetComponent<InputField>();
        input[7] = carry4.GetComponent<InputField>();


        input[0].text = "1";
        input[1].text = "1";
        input[2].text = "1";
        input[3].text = "1";
        input[4].text = "1";
        input[5].text = "1";
        input[6].text = "1";
        input[7].text = "1";

        btn.onClick.Invoke();

        if (input[0].text == "0" &&
           input[1].text == "1" &&
           input[2].text == "1" &&
           input[3].text == "0" &&
           input[4].text == "0" &&
           input[5].text == "0" &&
           input[6].text == "0" &&
           input[7].text == "1" &&
           message.text == "That's right!")
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

        sum1 = GameObject.Find("Sum1");
        sum2 = GameObject.Find("Sum2");
        sum3 = GameObject.Find("Sum3");
        sum4 = GameObject.Find("Sum3");
        carry1 = GameObject.Find("Carry1");
        carry2 = GameObject.Find("Carry2");
        carry3 = GameObject.Find("Carry3");
        carry4 = GameObject.Find("Carry4");

        InputField[] input = new InputField[8];

        input[0] = sum1.GetComponent<InputField>();
        input[1] = sum2.GetComponent<InputField>();
        input[2] = sum3.GetComponent<InputField>();
        input[3] = sum4.GetComponent<InputField>();
        input[4] = carry1.GetComponent<InputField>();
        input[5] = carry2.GetComponent<InputField>();
        input[6] = carry3.GetComponent<InputField>();
        input[7] = carry4.GetComponent<InputField>();


        input[0].text = "0";
        input[1].text = "1";
        input[2].text = "1";
        input[3].text = "0";
        input[4].text = "0";
        input[5].text = "0";
        input[6].text = "0";
        input[7].text = "1";

        btn.onClick.Invoke();

        if (input[0].text == "0" &&
           input[1].text == "1" &&
           input[2].text == "1" &&
           input[3].text == "0" &&
           input[4].text == "0" &&
           input[5].text == "0" &&
           input[6].text == "0" &&
           input[7].text == "1" &&
           message.text == "That's right!")
        {
            yield break;
        }
        else if (message.text == "That's wrong. " + "Try again.")
        {
            //yield return new WaitForSeconds(2);
            yield break;
        }

        Assert.Fail();
        SceneManager.LoadScene("Scenes/Lab 4");

    }


    private void SetupScene()
    {
        SceneManager.LoadScene("Scenes/Prelab4");
    }
}