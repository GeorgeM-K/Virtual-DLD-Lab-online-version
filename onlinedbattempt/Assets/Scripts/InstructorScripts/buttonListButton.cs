using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonListButton : MonoBehaviour
{
    [SerializeField]
    private Text sectionName;
    [SerializeField]
    private buttonListControl sectionControl;
    private string sectionTerm;
    private string instructor;

    public void SetText(string nameString, string termString, string instructorString){
        sectionName.text = nameString;
        sectionTerm = termString;
        instructor = instructorString;
        this.gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
        this.gameObject.GetComponent<Button>().name = nameString;
    }

    public void OnClick(){
        sectionControl.ButtonClicked(sectionName.text, sectionTerm, instructor);
    }
}