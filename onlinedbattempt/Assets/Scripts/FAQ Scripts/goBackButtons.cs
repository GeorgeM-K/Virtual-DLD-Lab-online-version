using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goBackButtons: MonoBehaviour {
    public void goBacktoAdminPage()
    {
        SceneManager.LoadScene("Scenes/AdminSubsystem");
    }
    public void goBacktoStudentPage()
    {
        SceneManager.LoadScene("Scenes/StudentSubsystem");
    }
}
