using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ListUpdate : MonoBehaviour {

	[SerializeField]
    public GameObject buttonTemplate;

    [SerializeField]
    private buttonListControl sectionControl;
    [SerializeField]
    private ClassManager sectionManager;
    string sectionName;
    string sectionTerm;
    string instructor;

    public string[] Text;

    public int numcreated = 0;
	
	void Start () {

		StartCoroutine(listUpdate1());
		
	}

   /* void OnDestroy(){
        
        while(numcreated >= 0){
            sectionControl.deleteSection(Text[numcreated]);
        }

        numcreated-=1;

   }*/
	
	IEnumerator listUpdate1(){

        WWW www = new WWW("https://dldvirtuallab.000webhostapp.com/listUpdate.php");
        yield return www;
		if(www.text != "10" && www.text != ""){
		string itemsDataString = www.text;

        Text = itemsDataString.Split('|'); 
		
        //print(Text[1]);
        int j =0;
        int i = 0;
        /* while(Text[j] != ""){
            sectionControl.deleteSection(Text[j]);
            j+=1;
        }*/
        while(Text[i]!= ""){
            sectionTerm = sectionManager.getTerm();
        instructor = sectionManager.getInstructor();

		
		 //Create Button
        GameObject button = Instantiate(buttonTemplate) as GameObject;
        button.SetActive(true);
        button.GetComponent<buttonListButton>().SetText(Text[i], sectionTerm, instructor);
        button.transform.SetParent(buttonTemplate.transform.parent, false);

        //Add to list of sections
        sectionControl.sectionAdded2(Text[i], sectionTerm, instructor);

        i+=1;
        numcreated+=1;
        }
        
        
	}
		

	}

    IEnumerator listUpdate2(){

        WWW www = new WWW("http://localhost/sqlconnect/listUpdate.php");
        yield return www;
		if(www.text != "10" && www.text != ""){
		string itemsDataString = www.text;

        Text = itemsDataString.Split('|'); 
		
        //print(Text[1]);
        
        int i = 0;

        while(Text[i]!= ""){
            sectionTerm = sectionManager.getTerm();
        instructor = sectionManager.getInstructor();
		
		 //Create Button
        sectionControl.deleteSection(Text[i]);

        //Add to list of sections
        

        i+=1;
        
        }
        
        
	}
		

	}

}


