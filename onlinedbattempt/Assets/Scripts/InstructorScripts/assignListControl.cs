using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
public class assignListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject cellTemplate;
    public GameObject content;
	public Text title;
	int numParts;
	int numStudents;

	string[] Text2;

    void Start(){
		numStudents = 15; //Number of students
		numParts = 1; //Number of parts including final grade "part" 
		content.GetComponent<GridLayoutGroup>().constraintCount = numParts+1;

		/* //HARD CODE 
		string[] names = new string[numStudents];
		for (int i =0; i<numStudents; i++){
			names[i] = "Student "+(i+1).ToString();
		}
		int[] values = new int[numStudents];
		for (int i =0; i<numStudents; i++){
			values[i] = 50+i;
		} 

		

		fillData(names, values); */
		foreach (Transform child in content.transform){
			if(child.name == "Name"){
				continue;
			} else {
				GameObject.Destroy(child.gameObject);
			}
		}

		StartCoroutine(listUpdate3());
		
    }

	public void fillData(string[] names, int[] values){
		foreach (Transform child in content.transform){
			if(child.name == "Name"){
				continue;
			} else {
				GameObject.Destroy(child.gameObject);
			}
		}
		for (int i = 0; i<numParts;i++){ //create first row, name cell is already created
			if (i == (numParts-1)){
				GameObject text = Instantiate(cellTemplate) as GameObject;
				text.SetActive(true);
				text.GetComponent<assignListText>().SetText("Final");
				text.transform.SetParent(cellTemplate.transform.parent, false);
			}  else {
				GameObject text = Instantiate(cellTemplate) as GameObject;
				text.SetActive(true);
				text.GetComponent<assignListText>().SetText("part "+(i+1).ToString());
				text.transform.SetParent(cellTemplate.transform.parent, false);
			}
			print(i);
		}
        //Create Data Cells
        for (int i = 0; i<names.Length;i++){ //for each student
			//create name cell first
			//print(names[i]+"name");
			GameObject text = Instantiate(cellTemplate) as GameObject;
			text.SetActive(true);
			text.GetComponent<assignListText>().SetText(names[i]); /*Set student name here */
			text.transform.SetParent(cellTemplate.transform.parent, false);

			//create rest of data next
			for (int j = 0; j<numParts;j++){ //create first row, name cell is already created
				if (j == (numParts-1)){ //if final grade
					GameObject data = Instantiate(cellTemplate) as GameObject;
					data.SetActive(true);
					print(values[i]+"value");
					data.GetComponent<assignListText>().SetText(values[i].ToString()); /* set final grade here*/
					data.transform.SetParent(cellTemplate.transform.parent, false);
				}  else {
					GameObject data = Instantiate(cellTemplate) as GameObject;
					data.SetActive(true);
					data.GetComponent<assignListText>().SetText((i).ToString()+"/"+(i+j).ToString()); /*set partial credit here */
					data.transform.SetParent(cellTemplate.transform.parent, false);
				}
			}
        }
	}

	
	IEnumerator listUpdate3(){
		foreach (Transform child in content.transform){
			if(child.name == "Name"){
				continue;
			} else {
				GameObject.Destroy(child.gameObject);
			}
		}
		WWWForm form = new WWWForm();
        print(dbManager.section);
		print(title.text);
		form.AddField("class", dbManager.section);
		form.AddField("Lab", title.text);
        WWW www = new WWW("https://dldvirtuallab.000webhostapp.com/AssignmentUpdate.php", form);
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
