using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emailinstructor : MonoBehaviour
{

    public void emailButton()
    {
        Application.OpenURL("www.gmail.com");
    }

    public void sakaiButton()
    {
        Application.OpenURL("https://sakai.rutgers.edu/portal");
    }

}
