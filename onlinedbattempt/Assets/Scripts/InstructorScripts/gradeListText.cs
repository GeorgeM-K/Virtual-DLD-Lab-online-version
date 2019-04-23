using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gradeListText : MonoBehaviour
{
    [SerializeField] private Text nameString;
    [SerializeField] private gradeListControl gradeControl;
	[SerializeField] Text prelab1;
	[SerializeField] Text prelab2;
	[SerializeField] Text prelab3;
	[SerializeField] Text prelab4;
	[SerializeField] Text prelab5;
	[SerializeField] Text lab1;
	[SerializeField] Text lab2;
	[SerializeField] Text lab3;
	[SerializeField] Text lab4;
	[SerializeField] Text lab5;
	[SerializeField] Text postlab1;
	[SerializeField] Text postlab2;
	[SerializeField] Text postlab3;
	[SerializeField] Text postlab4;
	[SerializeField] Text postlab5;
	//[SerializeField] Text final;



    public void SetGrade(string name, string pre1, string pre2, string pre3, string pre4, string pre5,
			 string l1, string l2, string l3, string l4, string l5, 
			 string post1, string post2, string post3, string post4, string post5){
		nameString.text = name;
		prelab1.text = pre1;
		prelab2.text = pre2;
		prelab3.text = pre3;
		prelab4.text = pre4;
		prelab5.text = pre5;

		lab1.text = l1;
		lab2.text = l2;
		lab3.text = l3;
		lab4.text = l4;
		lab5.text = l5;

		postlab1.text = post1;
		postlab2.text = post2;
		postlab3.text = post3;
		postlab4.text = post4;
		postlab5.text = post5;

		//final.text = f.ToString();

        //this.gameObject.GetComponent<Text>().name = name;
		
    }
}