using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class assignListText : MonoBehaviour
{
    [SerializeField] private gradeListControl gradeControl;
	[SerializeField] private Text cell;

    public void SetText(string target){
		this.gameObject.GetComponent<Text>().text = target;
		this.gameObject.name = target;
    }
}