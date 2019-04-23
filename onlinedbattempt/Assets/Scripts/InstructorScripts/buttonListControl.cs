using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class buttonListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;
    public GameObject content;
    public static string currentSection;
    public static string currentTerm;
    public static string currentInstructor;
    public void Start(){
        
        //Create buttons for each section
        /* for (int i = 0; i<ClassManager.numSections;i++){
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);
            //Call Set Text function for each button to store their variables
            button.GetComponent<buttonListButton>().SetText(ClassManager.allSections[i].name, 
                    ClassManager.allSections[i].term, ClassManager.allSections[i].instructor);

            button.transform.SetParent(buttonTemplate.transform.parent, false);
        }*/
    }

    public void ButtonClicked(string sectionName, string sectionTerm, string instructor){
        //When a button is clicked on, the clicked section's variales are saved and class scene is loaded.
        PlayerPrefs.SetString("currentInstructor", instructor);
        PlayerPrefs.SetString("currentTerm", sectionTerm);
        PlayerPrefs.SetString("currentSection", sectionName);
        dbManager.section = sectionName;
        SceneManager.LoadScene("ClassPage");
    }

    public void sectionAdded(string name, string term, string instructor){
        ClassManager.allSections.Add(ClassManager.Section.createSection(name, term, instructor));
        ClassManager.numSections+=1;
        //print(ClassManager.numSections);
    }
    public void sectionAdded2(string name, string term, string instructor){
        ClassManager.allSections.Add(ClassManager.Section.createSection(name, term, instructor));
        //ClassManager.numSections+=1;
        //print(ClassManager.numSections);
    }

    public void deleteSection(string target){
        Destroy(content.transform.Find(target).gameObject);
        //ClassManager.numSections -=1;
    }
}