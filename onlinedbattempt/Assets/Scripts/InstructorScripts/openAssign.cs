using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class openAssign : MonoBehaviour{
	[SerializeField] public GameObject button;
	[SerializeField] public Text title;
	//[SerializeField] private barChart graph;

	private GameObject cellTemplate;

	public GameObject content;
	[SerializeField] private assignListControl dataController;

	private int[] values2;
	private string[] names2;
	private string[] Text2;
	public void loadAssign(){
		title.GetComponent<Text>().text = button.GetComponentInChildren<Text>().text;

		/* foreach (Transform child in content.transform){
			if(child.name == "Name"){
				continue;
			} else {
				GameObject.Destroy(child.gameObject);
			}
		}*/
		/* //HARD CODE 
		int numStudents = 15; //input number of students

		string[] names = new string[numStudents];
		//Input students names in string array
		for (int i =0; i<numStudents; i++){
			names[i] = "Student "+(i+1).ToString();
		}
		int[] values = new int[numStudents];
		//Input grades in int array names[i] has a grade of values[i]
		for (int i =0; i<numStudents; i++){
			values[i] = 50+i;
		} */
		dataController.title.text = title.GetComponent<Text>().text;
		StartCoroutine(listUpdate3());
		
		//dataController.fillData(names2, values2);
	}

IEnumerator listUpdate3(){
		WWWForm form = new WWWForm();
        print(dbManager.section);
		print(title.text);
		dbManager.labgradeRequired = title.text;
		form.AddField("class", dbManager.section);
		form.AddField("Lab", title.text);
        WWW www = new WWW("http://localhost/sqlconnect/AssignmentUpdate.php", form);
        yield return www;
		if(www.text != "10"){
		string itemsDataString = www.text;

        Text2 = itemsDataString.Split('|'); 
		
		GameObject text = Instantiate(cellTemplate) as GameObject;
				text.SetActive(true);
				text.GetComponent<assignListText>().SetText("Final");
				text.transform.SetParent(cellTemplate.transform.parent, false);

        //print(Text[1]);
        
        int i = 0;
		

        print(Text2[i]);
		print(Text2[i+1]);
		

        while(Text2[i]!= ""){
            GameObject text3 = Instantiate(cellTemplate) as GameObject;
			text3.SetActive(true);
			text3.GetComponent<assignListText>().SetText(Text2[i]); /*Set student name here */
			text3.transform.SetParent(cellTemplate.transform.parent, false);

			GameObject data = Instantiate(cellTemplate) as GameObject;
					data.SetActive(true);
					print(Text2[i+1]+"value");
					data.GetComponent<assignListText>().SetText(Text2[i+1].ToString()); /* set final grade here*/
					data.transform.SetParent(cellTemplate.transform.parent, false);




        i+=2;
		}

		
        
	}
}



}