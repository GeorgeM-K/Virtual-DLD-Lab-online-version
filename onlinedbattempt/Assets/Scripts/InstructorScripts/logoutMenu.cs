using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class logoutMenu : MonoBehaviour
{
   public void Logout(){

       SceneManager.LoadScene("Login");
       
   }
}
