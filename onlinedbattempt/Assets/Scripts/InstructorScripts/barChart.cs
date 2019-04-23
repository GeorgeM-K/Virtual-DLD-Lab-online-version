using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
public class barChart : MonoBehaviour {

	public bar barPrefab;
	private int[] inputValues;
	private string[] labels;
	public Text title;
	public Text inputText;
	
	List<bar> bars = new List<bar>();

	public float chartHeight;

	private string[] Text2;

	private int[] Text3;

	void Start () {
		if (chartHeight == 0){
			chartHeight  = 200;
		}
		inputValues = new int[10];
		/*Place input values here. On start, use first assignment on list*/
		/*the input values should represent how many are in each range of 10*/
		/*so for input[0] = how many people got scores of [0-10] 
			input[1] = how many people got scores of [11-20]
			input[9] = how many people got scores of [91-100]*/
		/* for (int i =0; i< 10; i++){
			inputValues[i] = i;
		}*/
		// Labels
		labels = new string[10];
		labels[0] = "0 to 10";
		for (int i = 1; i<10; i++){
			labels[i] = ((i*10)+1).ToString()+" to "+ ((i*10)+10).ToString();
		}
		//title.text = inputText.text; //first assignment in the list
		//DisplayGraph(inputValues);

			StartCoroutine(chartUpdate());
	}

	public void DisplayGraph(int[] vals){
		int maxValue = vals.Max();
		title.text = "Distribution of Grades"; //Get title from assignment button

		for (int i = 0; i<vals.Length; i++){
			bar newBar = Instantiate(barPrefab) as bar;
			newBar.transform.SetParent(transform);
			newBar.GetComponent<RectTransform>().localScale = new Vector2(1f,1f);
			//size bar
			RectTransform rt = newBar.barImage.GetComponent<RectTransform>();
			float normalizedValue = ((float)vals[i]/(float)maxValue) * 0.9f;
			rt.sizeDelta = new Vector2(rt.sizeDelta.x, chartHeight * normalizedValue);

			//set label
			if (labels.Length <= i){
				newBar.label.text = "UNDEFINED";
			} else {
				newBar.label.text = labels[i];
			}

			//set value label
			newBar.barValue.text = vals[i].ToString();
			//if height is too small, move label to top of bar
			if (rt.sizeDelta.y < 30f){
				newBar.barValue.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
				newBar.barValue.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			}
		}
	}
	
	IEnumerator chartUpdate(){

		WWWForm form = new WWWForm();
        //print(dbManager.section);
		//print(dbManager.labgradeRequired); //NEED A WAY OF GETTING THE NAME OF THE ITEM WHOSE GRADE WE NEED
		form.AddField("class", dbManager.section);
		if(dbManager.labgradeRequired == null){
			form.AddField("Lab", "preLabOne");
		}
		else{
		form.AddField("Lab", dbManager.labgradeRequired);
		}
        WWW www = new WWW("http://localhost/sqlconnect/barChart.php", form);
        yield return www;
		//print(www.text);
		if(www.text != "10"){
		string itemsDataString = www.text;
		

		Text2 = itemsDataString.Split('|'); 
		int count2 = 0;
		for(int z = 0; z<Text2.Length; z++){
			if(Text2[z] != ""){
				count2+=1;
			}
		}


		//int test = int.Parse("3");
		//print(test);

			Text3 = new int[count2];

        
		//Text3[0] = int.Parse("3");
		//print(Text3[0]);
		for(int j = 0; j< itemsDataString.Length; j++){
		if(Text2[j] != ""){
			string temps;
			int temp;
			temps = Text2[j];
			temp = int.Parse(temps);
			Text3[j] = temp;
			print(Text2[j]);
			print(Text3[j]);
		}
	     	//print(Text2[j]);
		}

		for(int m = 0; m<Text3.Length;m++){
			if(Text3[m]>=0 && Text3[m] <= 10){
				inputValues[0]++;
			}
			else if(Text3[m]>=11 && Text3[m] <= 20){
				inputValues[1]++;
			}
			else if(Text3[m]>=21 && Text3[m] <= 30){
				inputValues[2]++;
			}
			else if(Text3[m]>=31 && Text3[m] <= 40){
				inputValues[3]++;
			}
			else if(Text3[m]>=41 && Text3[m] <= 50){
				inputValues[4]++;
			}
			else if(Text3[m]>=51 && Text3[m] <= 60){
				inputValues[5]++;
			}
			else if(Text3[m]>=61 && Text3[m] <= 70){
				inputValues[6]++;
			}
			else if(Text3[m]>=71 && Text3[m] <= 80){
				inputValues[7]++;
			}
			else if(Text3[m]>=81 && Text3[m] <= 90){
				inputValues[8]++;
			}
			else if(Text3[m]>=91 && Text3[m] <= 100){
				inputValues[9]++;
			}
		}
		
		
			//Text3 = Array.ConvertAll(Text2, delegate(string s) { return int.Parse(s);});
		
		//print(Text3[0]);
		

		//bool result2 = int.TryParse(Text2[0], out Text3[0]);

		//	print(result2);
			//Text3[i] = Int32.Parse(Text2[i]);
		//	print(Text3[0]);

		/* int i = 0;
		//print(Text2[i]);
		while(Text3[i] != null){
			
			//bool result = int.TryParse(Text2[i], out Text3[i]);

			//print(result);
			//Text3[i] = Int32.Parse(Text2[i]);
			//print(Text3[0]);
			
			int maxValue = inputValues.Max();
			//title.text = "Distribution of Grades";
			bar newBar = Instantiate(barPrefab) as bar;
			newBar.transform.SetParent(transform);
			newBar.GetComponent<RectTransform>().localScale = new Vector2(1f,1f);
			//size bar
			RectTransform rt = newBar.barImage.GetComponent<RectTransform>();
			float normalizedValue = ((float)inputValues[i]/(float)maxValue) * 0.9f;
			rt.sizeDelta = new Vector2(rt.sizeDelta.x, chartHeight * normalizedValue);

			//set label
			if (labels.Length <= i){
				newBar.label.text = "UNDEFINED";
			} else {
				newBar.label.text = labels[i];
			}

			//set value label
			newBar.barValue.text = Text3[i].ToString();
			//if height is too small, move label to top of bar
			if (rt.sizeDelta.y < 30f){
				newBar.barValue.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
				newBar.barValue.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			} 
			
			i+=1;
		}


	}*/

		int maxValue = inputValues.Max();
		title.text = "Distribution of Grades"; //Get title from assignment button

		for (int i = 0; i<inputValues.Length; i++){
			bar newBar = Instantiate(barPrefab) as bar;
			newBar.transform.SetParent(transform);
			newBar.GetComponent<RectTransform>().localScale = new Vector2(1f,1f);
			//size bar
			RectTransform rt = newBar.barImage.GetComponent<RectTransform>();
			float normalizedValue = ((float)inputValues[i]/(float)maxValue) * 0.9f;
			rt.sizeDelta = new Vector2(rt.sizeDelta.x, chartHeight * normalizedValue);

			//set label
			if (labels.Length <= i){
				newBar.label.text = "UNDEFINED";
			} else {
				newBar.label.text = labels[i];
			}

			//set value label
			newBar.barValue.text = inputValues[i].ToString();
			//if height is too small, move label to top of bar
			if (rt.sizeDelta.y < 30f){
				newBar.barValue.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
				newBar.barValue.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			}
		}

}
}
}