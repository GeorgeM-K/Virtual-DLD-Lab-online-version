using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;


public class statsCalculator : MonoBehaviour {
    public Text average;
    public Text firstQuartile;
    public Text median;
    public Text thridQuartile;
    public Text min;
    public Text max;
    public Text mode;
    public Text StdDev;

    private int avg;
    private int firstq;

    private int thirdq;

    private int mediani;

    private int mini;

    private int maxi;

    private int modei;

    private int std;

    private string[] Text2;

	private int[] Text3;

    void Start () {
        string defaultAssignment = "Prelab 1";
        calculate(defaultAssignment);
    }
    
    int MeanCalculator(int[] vals) {
        int mean=0, counter=0;
        for (int i=0; i<vals.Length; i++) {
            mean += vals[i];
            counter++;
        }
        mean = mean/counter;
        return mean;    
    }

    int MinCalculator(int[] vals) {
        int minimum = vals[0]; //or whatever is the first account's grades in section
        for (int i=0; i<vals.Length; i++) {
            if (minimum > vals[i]){
                minimum = vals[i];
            }
        }
        return minimum;
    }

    int MaxCalculator(int[] vals) {
        int maximum = vals[0]; //or whatever is the first account's grades in section
        for (int i=0; i<vals.Length; i++) {
            if (maximum < vals[i]){
                maximum = vals[i];
            }

        }
        return maximum;
    }

    int MedianCalculator(int[] vals){

            if (vals.Length % 2 == 0) {  // count is even, average two middle elements
                int a = vals[(vals.Length / 2)-1];
                int b = vals[(vals.Length / 2)];
                return (a + b) / 2;
            } else { // count is odd, return the middle element
                return vals[(vals.Length/2)];
            }
    }

    int ModeCalculator(int[] vals){
        var groups = vals.GroupBy(v => v);
        int maxCount = groups.Max(g => g.Count());
        int modeOut = groups.First(g => g.Count() == maxCount).Key;
        return modeOut;
	}
    
    
    
    int Quartile25Calculator(int[] vals){
        int mid_index;
        if (vals.Length %2 == 0){ //even count
            mid_index = (vals.Length/2)-1;
            if ((mid_index+1) %2 == 0){
                int a = vals[(mid_index/2)];
                int b = vals[(mid_index/2)+1];
                return (a+b)/2;
            } else {
                return vals[(mid_index/2)+1];
            }
        } else {  //odd count
            mid_index = (vals.Length/2);
            if (mid_index %2 == 0){
                int a = vals[(mid_index/2)-1];
                int b = vals[(mid_index/2)];
                return (a+b)/2;
            } else {
                return vals[(mid_index/2)];
            } 
        }
    }

    int Quartile75Calculator(int[] vals){
        int mid_index;
        if (vals.Length %2 == 0){ //even count
            mid_index = (vals.Length/2);
            if ((mid_index) %2 == 0){
                int a = vals[mid_index+ (vals.Length - mid_index-1)/2];
                int b = vals[(mid_index+ (vals.Length - mid_index-1)/2)+1];
                return (a+b)/2;
            } else {
                return vals[mid_index+((vals.Length - mid_index)/2)];
            }
        } else {  //odd count
            mid_index = (vals.Length/2);
            if (mid_index %2 == 0){
                int a = vals[mid_index+(vals.Length - mid_index)/2];
                int b = vals[(mid_index+(vals.Length - mid_index)/2)+1];
                return (a+b)/2;
            } else {
                return vals[mid_index+(vals.Length - mid_index)/2];
            } 
        }
    }

	int CalculateStdDev(int[] values){   
  		int ret = 0;
  		if (values.Length > 0) {      
			//Compute the Average      
			double avg = values.Average();
			//Perform the Sum of (value-avg)_2_2      
			double sum = values.Sum(d => Math.Pow(d - avg, 2));
			//Put it all together      
			ret = (int) (Math.Sqrt((sum) / (values.Count()-1)));   
		}   
  		return ret;
    }

    public void calculate(string assignment){
        /*Get input values from assignment name */
        /*The button will put in its name when clicked on and will call this function */
        /*
            Names will be
        
            Prelab 1    Lab 1   Postlab 1
            Prelab 2    Lab 2       .
                .         .         .
                .         .         .
                .         .         .
            Prelab 5    Lab 5   Postlab 5
         */
        /*HARD CODE */
        /*Put input values into values array */
        int numStudents = 10;
        int[] values = new int[numStudents];

        //load values into values array
        for (int i=0; i<numStudents; i++){
            values[i] = i+50;
        }

        average.text = "Average: " + avg.ToString();
        firstQuartile.text = "25 Percentile: "+firstq.ToString(); 
        median.text = "Median: "+mediani.ToString();
        thridQuartile.text = "75 Percentile: "+thirdq.ToString();
        min.text = "Minimum: "+mini.ToString();
        max.text = "Maximum: "+maxi.ToString();
        mode.text = "Mode: "+modei.ToString();
        StdDev.text = "Std Deviation: "+std.ToString();
        return;
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
        //standard deviation
        std = 0;
  		if (Text3.Length > 0) {      
			//Compute the Average      
			double avg = Text3.Average();
			//Perform the Sum of (value-avg)_2_2      
			double sum = Text3.Sum(d => Math.Pow(d - avg, 2));
			//Put it all together      
			std = (int) (Math.Sqrt((sum) / (Text3.Count()-1))); 
          }
        //thirdquartile
        int mid_index;
        if (Text3.Length %2 == 0){ //even count
            mid_index = (Text3.Length/2);
            if ((mid_index) %2 == 0){
                int a = Text3[mid_index+ (Text3.Length - mid_index-1)/2];
                int b = Text3[(mid_index+ (Text3.Length - mid_index-1)/2)+1];
                thirdq = (a+b)/2;
            } else {
                thirdq = Text3[mid_index+((Text3.Length - mid_index)/2)];
            }
        } else {  //odd count
            mid_index = (Text3.Length/2);
            if (mid_index %2 == 0){
                int a = Text3[mid_index+(Text3.Length - mid_index)/2];
                int b = Text3[(mid_index+(Text3.Length - mid_index)/2)+1];
                thirdq = (a+b)/2;
            } else {
                thirdq = Text3[mid_index+(Text3.Length - mid_index)/2];
            } 
        }

          int mid_index1;
        if (Text3.Length %2 == 0){ //even count
            mid_index1 = (Text3.Length/2)-1;
            if ((mid_index1+1) %2 == 0){
                int a = Text3[(mid_index1/2)];
                int b = Text3[(mid_index1/2)+1];
                firstq = (a+b)/2;
            } else {
                firstq = Text3[(mid_index1/2)+1];
            }
        } else {  //odd count
            mid_index1 = (Text3.Length/2);
            if (mid_index1 %2 == 0){
                int a = Text3[(mid_index1/2)-1];
                int b = Text3[(mid_index1/2)];
                firstq = (a+b)/2;
            } else {
                firstq = Text3[(mid_index1/2)];
            } 
        }

		var groups = Text3.GroupBy(v => v);
        int maxCount = groups.Max(g => g.Count());
        int modeOut = groups.First(g => g.Count() == maxCount).Key;
        modei = modeOut;

        if (Text3.Length % 2 == 0) {  // count is even, average two middle elements
                int a = Text3[(Text3.Length / 2)-1];
                int b = Text3[(Text3.Length / 2)];
                mediani = (a + b) / 2;
            } else { // count is odd, return the middle element
                mediani = Text3[(Text3.Length/2)];
            }

         maxi = Text3[0]; //or whatever is the first account's grades in section
        for (int i=0; i<Text3.Length; i++) {
            if (maxi < Text3[i]){
                maxi = Text3[i];
            }

        }

        mini = Text3[0]; //or whatever is the first account's grades in section
        for (int i=0; i<Text3.Length; i++) {
            if (mini > Text3[i]){
                mini = Text3[i];
            }
        }
        
        int counter=0;
        avg = 0;
        for (int i=0; i<Text3.Length; i++) {
            avg += Text3[i];
            counter++;
        }
        avg = avg/counter;
}
}
}
