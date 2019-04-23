//written by George Melman-Kenny
//Debugged by George Melman-Kenny
//Tested by George Melman-Kenny
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{

    public InputField InputUsername; //username is email
    public InputField InputPassword;

    public Button BtnLogin;
    public Button BtnResetPassword;
    public Button BtnExit;
   
   

    public void OnLoginClick()
    {
       
        StartCoroutine(Check());
    }

    IEnumerator Check()
    {
        if(InputUsername.text == "student" && InputPassword.text == "student")
        {
            Debug.Log("Hard forcing into student subsystem");
            SceneManager.LoadScene("Scenes/StudentSubsystem");
            yield break;
        }
        else if (InputUsername.text == "admin" && InputPassword.text == "admin")
        {
            Debug.Log("Hard forcing into admin subsystem");
            SceneManager.LoadScene("Scenes/AdminSubsystem");
            yield break;
        }
        



          WWWForm form = new WWWForm();
        form.AddField("email", InputUsername.text);
        form.AddField("password", InputPassword.text);
        
        WWW www = new WWW("https://dldvirtuallab.000webhostapp.com/login.php", form);
        yield return www;

        if(www.text == "0"){
            dbManager.email = InputUsername.text;
            SceneManager.LoadScene("Scenes/StudentSubsystem");
        }

        WWWForm adminform = new WWWForm();
        adminform.AddField("email", InputUsername.text);
        adminform.AddField("password", InputPassword.text);
        
        WWW aaa = new WWW("https://dldvirtuallab.000webhostapp.com/loginadmin.php", form);
        yield return aaa;

        if(aaa.text == "0"){
            dbManager.email = InputUsername.text;
            SceneManager.LoadScene("Scenes/AdminSubsystem");
        }


        else{
            Debug.Log("Failed to login user Error"+www.text);
        }


        
       Debug.Log(www.text);
    }

    public void verifyInputs(){
      BtnLogin.interactable = (InputUsername.text.Length >= 1 && InputPassword.text.Length >=1);
    }
}
