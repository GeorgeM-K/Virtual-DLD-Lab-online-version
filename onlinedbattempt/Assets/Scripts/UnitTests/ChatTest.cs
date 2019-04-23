using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ChatTest : MonoBehaviour {

    public GameObject inputfield1;

    [UnityTest]
    public IEnumerator SendMessage()
    {
        SetupScene();
        yield return null;

        GameObject button1 = GameObject.Find("Button");
        Button btn = button1.GetComponent<Button>();
        yield return new WaitForSecondsRealtime(1);
        btn.onClick.Invoke();
        yield return new WaitForSecondsRealtime(2);

        InputField[] input = new InputField[5];
        inputfield1 = GameObject.Find("InputField");

        input[0] = inputfield1.GetComponent<InputField>();
        input[0].text = "Hello world";
        yield return new WaitForSecondsRealtime(2);

        GameObject button2 = GameObject.Find("Send Message");
        Button btn2 = button2.GetComponent<Button>();
        btn2.onClick.Invoke();
        yield return new WaitForSecondsRealtime(5);
    }

    //[UnityTest]
    public IEnumerator Tips()
    {
        GameObject tipsbtn1 = GameObject.Find("Tips");
        Button tipsbtn = tipsbtn1.GetComponent<Button>();
        tipsbtn.onClick.Invoke();
        yield return new WaitForSecondsRealtime(5);
    }
    

    private void SetupScene()
    {
        SceneManager.LoadScene("Scenes/SandboxLab");
    }
}
