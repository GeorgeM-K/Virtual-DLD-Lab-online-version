//written by George Melman-Kenny
//Debugged by George Melman-Kenny
//Tested by George Melman-Kenny
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class newuser_admin : MonoBehaviour {

	public Button BtnEnter; 
	public Button BtnExit;
    public InputField Inputemail;
    public InputField InputFirstname;
    public InputField Inputlastname;


    public InputField InputPw1;

    public InputField InputPw2;

	public void CallRegister(){
        StartCoroutine(Register());
    }

    IEnumerator Register(){
         WWWForm form = new WWWForm();
        form.AddField("firstname", InputFirstname.text);
        form.AddField("lastname", Inputlastname.text);
        form.AddField("email", Inputemail.text);
        form.AddField("password", InputPw1.text);
        
        WWW www = new WWW("https://dldvirtuallab.000webhostapp.com/registeradmin.php", form);
        yield return www;
        
        if(www.text == "0"){
            SceneManager.LoadScene("Login");
        }

        else{
       Debug.Log(www.text);
        };
       
        
    }

	public void verifyInputs(){
        BtnEnter.interactable = (Inputemail.text.Length >= 1 && InputFirstname.text.Length >= 1 && Inputlastname.text.Length >= 1 && InputPw1.text.Length >=1 && InputPw2.text.Length >= 1 && InputPw1.text == InputPw2.text);
    }
}
