using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AboutButton : MonoBehaviour
{
    public GameObject Back;
    public void goBack()
    {
        Debug.Log("Back Button Clicked");
        SceneManager.LoadScene("Scenes/StudentSubsystem");
    }

}
