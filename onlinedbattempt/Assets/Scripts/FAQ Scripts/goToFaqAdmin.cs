using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToFaqAdmin : MonoBehaviour {

    public void faqPageAdmin() {

        SceneManager.LoadScene("Scenes/FAQAdmin");
            
    }
    public void faqPageStudent()
    {

        SceneManager.LoadScene("Scenes/FAQStudent");

    }
}
