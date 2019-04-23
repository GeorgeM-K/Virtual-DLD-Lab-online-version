using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTAList : MonoBehaviour
{
    public GameObject TAPanel;
    public void OpenTAPanel()
    {
        if (TAPanel != null)
        {
            bool isActive = TAPanel.activeSelf;
            TAPanel.SetActive(!isActive);
        }
    }

}
