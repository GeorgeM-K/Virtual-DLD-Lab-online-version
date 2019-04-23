using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotHandler : MonoBehaviour {

    private static ScreenshotHandler instance;
    private Camera myCamera;
    private bool takeSSonNextFrame;

    private void Awake()
    {
        instance = this;
        myCamera = gameObject.GetComponent<Camera>();
    }

    private void OnPostRender()
    {
        if (takeSSonNextFrame)
        {
            takeSSonNextFrame = false;
            RenderTexture renderTexture = myCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);
            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/CameraScreenshot.png", byteArray);
            Debug.Log("Saved CameraScreenshot.png");

            RenderTexture.ReleaseTemporary(renderTexture);
            myCamera.targetTexture = null;
        }
    }

    private void TakeScreenshot(int width, int height)
    {
        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeSSonNextFrame = true;
    }


    public static void TakeScreenshot_static(int width, int height)
    {
        instance.TakeScreenshot(width, height);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Slash))
        {
            if (Input.GetKey(KeyCode.S))
            {
                TakeScreenshot_static(Screen.width, Screen.height);
            }
        }
    }

    //public void buttonForSS()
    //{
    //    TakeScreenshot(Screen.width, Screen.height);
    //}

}
