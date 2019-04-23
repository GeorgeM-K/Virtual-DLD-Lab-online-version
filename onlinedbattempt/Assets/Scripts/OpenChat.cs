using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChat : MonoBehaviour
{
    public GameObject ChatCanvas;
    public void OpenChatUI()
    {
        if (ChatCanvas != null)
        {
            bool isActive = ChatCanvas.activeSelf;
            ChatCanvas.SetActive(!isActive);
        }
    }

}
