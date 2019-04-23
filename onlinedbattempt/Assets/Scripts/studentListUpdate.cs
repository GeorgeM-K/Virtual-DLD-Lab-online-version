using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class studentListUpdate : MonoBehaviour {
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
	
	void Start () {

		StartCoroutine(listUpdate1());
		
	}
	
	IEnumerator listUpdate1(){
		WWWForm form = new WWWForm();
		form.AddField("class", dbManager.section);
        WWW www = new WWW("https://dldvirtuallab.000webhostapp.com/studentListUpdate.php", form);
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
        GameObject button = Instantiate(buttonTemplate) as GameObject;
        button.SetActive(true);
        button.GetComponent<buttonListButton>().SetText(Text[i], sectionTerm, instructor);
        button.transform.SetParent(buttonTemplate.transform.parent, false);

        //Add to list of sections
        sectionControl.sectionAdded(Text[i], sectionTerm, instructor);

        i+=1;
        }
        
        
	}
		

	}

}
