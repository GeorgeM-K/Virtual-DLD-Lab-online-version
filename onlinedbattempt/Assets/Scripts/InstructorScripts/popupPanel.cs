using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popupPanel : MonoBehaviour{
    public GameObject targetPanel;
	[SerializeField] public GameObject button;
	[SerializeField] public GameObject otherButton;
    public void toggle(){
        bool isActive = targetPanel.activeSelf;
        targetPanel.SetActive(!isActive);
    }

	public void open(){
		var colors = GetComponent<Button>().colors;
        colors.normalColor = Color.white;
		button.GetComponent<Button>().colors= colors;
		colors.normalColor = new Color(1f,1f,1f,0.2f);
		otherButton.GetComponent<Button>().colors= colors;
		
		targetPanel.SetActive(true);
	}

	public void close(){
		var colors = GetComponent<Button>().colors;
        colors.normalColor = Color.white;
		button.GetComponent<Button>().colors= colors;
		colors.normalColor = new Color(1f,1f,1f,0.2f);
		otherButton.GetComponent<Button>().colors= colors;
		
		targetPanel.SetActive(false);
	}

}
