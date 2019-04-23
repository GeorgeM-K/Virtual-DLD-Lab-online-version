using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadClass : MonoBehaviour
{
    [SerializeField]
    private Text header;
    [SerializeField]
    private Text instructor;
    [SerializeField]
    private Text term;
    [SerializeField]
    private Text numStudents;

    // Start is called before the first frame update
    void Start()
    {
        int amount = 13;
        numStudents.text = "Number of Students: " + amount.ToString();
        header.text = PlayerPrefs.GetString("currentSection", "Section Error");
        instructor.text = "Instructor: "+PlayerPrefs.GetString("currentInstructor", "Instructor Name");
        term.text = PlayerPrefs.GetString("currentTerm", "TermError");


    }

    public void SetText(string nameString, string termString, string instructorString, int amount){
        header.text = nameString;
        term.text = termString;
        instructor.text = instructorString;
        numStudents.text = amount.ToString();
    }
}
