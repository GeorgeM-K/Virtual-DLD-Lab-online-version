using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class gradeListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject rowTemplate;
    public GameObject content;
    public static string currentSection;
    public static string currentTerm;
    public static string currentInstructor;

    private string[] Text1;
    int numStudents;
    public void Start(){
        //Number of students
		/* numStudents = 10; //<<-- Input numer and students here
    
        for (int i = 0; i<numStudents;i++){ //for each row
            GameObject text = Instantiate(rowTemplate) as GameObject;
            text.SetActive(true);
            //Call Set Grade function for each student to store their grades
            text.GetComponent<gradeListText>().SetGrade("name"+i.ToString(), i,i,i,i,i,i,i,i,i,i,i,i,i,i,i); //<<--Input name and values here
			text.name = "name"+i.ToString(); //<<-- Input name here
            text.transform.SetParent(rowTemplate.transform.parent, false);
        } */

        StartCoroutine(listUpdate1());
    }

    IEnumerator listUpdate1(){
		WWWForm form = new WWWForm();
        print(dbManager.section);
		form.AddField("class", dbManager.section);
        WWW www = new WWW("https://dldvirtuallab.000webhostapp.com/gradetableupdate.php", form);
        yield return www;
		if(www.text != "10"){
		string itemsDataString = www.text;

        Text1 = itemsDataString.Split('|'); 
		
        //print(Text[1]);
        
        int i = 0;

        print(Text1[i+15]);

        while(Text1[i]!= ""){
           GameObject text = Instantiate(rowTemplate) as GameObject;
            text.SetActive(true);
            //Call Set Text function for each button to store their variables
            text.GetComponent<gradeListText>().SetGrade(Text1[i], Text1[i+1], Text1[i+2], Text1[i+3], Text1[i+4], Text1[i+5], Text1[i+6], Text1[i+7],Text1[i+8], Text1[i+9],Text1[i+10],Text1[i+11],Text1[i+12],Text1[i+13],Text1[i+14],Text1[i+15]);
			text.name = Text1[i];
            text.transform.SetParent(rowTemplate.transform.parent, false);
           // print(Text1[i]);
            
        i+=16;
        }
        
        
	}
}
}