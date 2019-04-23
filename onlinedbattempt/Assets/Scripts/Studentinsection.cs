using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Studentinsection : MonoBehaviour {
 [SerializeField]
    public GameObject buttonTemplate;

    [SerializeField]
    private buttonListControl sectionControl;
    [SerializeField]
    private Text addstatusText;
    [SerializeField]
    private ClassManager sectionManager;
    public InputField addnameInput;
    public InputField deletenameInput;
    public Text deletestatusText;
    string email;
    string sectionTerm;
    string instructor;

    public void AddButton_Click(){
      /*   //Read input data 
        sectionName = addnameInput.text.Trim();
        sectionTerm = sectionManager.getTerm();
        instructor = sectionManager.getInstructor();
        if (sectionName == null || sectionName == ""){
            addstatusText.text = "Status: Please enter section name";
            return;
        } else if (sectionManager.searchFor(sectionName) == 1){
            addstatusText.text = "Status: Failed, the section '" + sectionName + "' already exists";
            return;
        } else {
            addstatusText.text = "Status: Success, created '" + sectionName.Trim()+"'";
        }
        //Create Button
        GameObject button = Instantiate(buttonTemplate) as GameObject;
        button.SetActive(true);
        button.GetComponent<buttonListButton>().SetText(sectionName, sectionTerm, instructor);
        button.transform.SetParent(buttonTemplate.transform.parent, false);

        //Add to list of sections
        sectionControl.sectionAdded(sectionName, sectionTerm, instructor);
        */
        StartCoroutine(Check());
        
       
    }

     IEnumerator Check(){
        email = addnameInput.text.Trim();
        sectionTerm = sectionManager.getTerm();
        instructor = sectionManager.getInstructor();
        string filler = "class";
         
         WWWForm form = new WWWForm();
        form.AddField("class", dbManager.section);
		form.AddField("email", addnameInput.text);
        form.AddField("Lab", filler);
        
        WWW www = new WWW("https://dldvirtuallab.000webhostapp.com/addStudenttoSection.php", form);
        yield return www;

        if(www.text == "0"){
        Debug.Log("Student Added Successfully");
        //Create Button
        GameObject button = Instantiate(buttonTemplate) as GameObject;
        button.SetActive(true);
        button.GetComponent<buttonListButton>().SetText(email, sectionTerm, instructor);
        button.transform.SetParent(buttonTemplate.transform.parent, false);

        //Add to list of sections
        sectionControl.sectionAdded(email, sectionTerm, instructor);
        }

        else{
            Debug.Log("Failed to create Section Error"+www.text);
        }


        
       Debug.Log(www.text);
    }
     

    public void DeleteButton_Click(){
      /*   //Read input data
        sectionName = deletenameInput.text.Trim();
        sectionTerm = sectionManager.getTerm();
        instructor = sectionManager.getInstructor();
        if (sectionName == null || sectionName == ""){
            deletestatusText.text = "Status: Please enter section name";
            return;
        } else if (sectionManager.delete(sectionName) == 1){
            deletestatusText.text = "Status: Success, deleted '" + sectionName+"'";
        } else {
            deletestatusText.text = "Status: Failed, the section '" + sectionName + "' doesn't exist";
            return;
        }
        sectionControl.deleteSection(sectionName);
        */
        StartCoroutine(Check2());
    }
    IEnumerator Check2(){
        email = deletenameInput.text.Trim();
        sectionTerm = sectionManager.getTerm();
        instructor = sectionManager.getInstructor();
        string filler = "class";
         
         WWWForm form = new WWWForm();
        form.AddField("email", deletenameInput.text);
		form.AddField("class", "0");
        form.AddField("Lab", filler);
        
        WWW www = new WWW("https://dldvirtuallab.000webhostapp.com/addStudenttoSection.php", form);
        yield return www;

        if(www.text == "0"){
        Debug.Log("Section Deleted Successfully");
        //Create Button
        sectionControl.deleteSection(email);
        }

        else{
            Debug.Log("Failed to create Section Error"+www.text);
        }


        
       Debug.Log(www.text);
    }
}

