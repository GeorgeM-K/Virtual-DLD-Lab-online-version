//written by George Melman-Kenny
//Debugged by George Melman-Kenny
//tested by George Melman-Kenny
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour {
    public void mainMenu(string menu){
        SceneManager.LoadScene(menu);
    }
}
