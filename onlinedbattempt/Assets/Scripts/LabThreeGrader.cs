// written by: Andrew Koskinen
// tested by: Andrew Koskinen
// debugged by: Andrew Koskinen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LabThreeGrader : MonoBehaviour
{
    public Button Finish;
    public GameObject B2, B1, B0, Gate1, G0, G1, G2;
    List<GameObject> MarksList; //List that stores checkmark/cross game objects
    LogicManager logicManager;
    Sprite checkMarkSprite, crossMarkSprite;
    int LabThreeGrade = 80;
    // Use this for initialization
    int numTries = 0;



    void Start()
    {
        logicManager = GameObject.Find("LogicManager").GetComponent<LogicManager>();
        MarksList = new List<GameObject>();
        checkMarkSprite = Resources.Load<Sprite>("Sprites/002-tick");
        crossMarkSprite = Resources.Load<Sprite>("Sprites/001-close");
        B2.GetComponent<CheckerTagScript>().Type = GradingCONSTANTS.INPUT;
        B1.GetComponent<CheckerTagScript>().Type = GradingCONSTANTS.INPUT;
        B0.GetComponent<CheckerTagScript>().Type = GradingCONSTANTS.INPUT;
        Gate1.GetComponent<CheckerTagScript>().Type = GradingCONSTANTS.INPUT;
        G0.GetComponent<CheckerTagScript>().Type = GradingCONSTANTS.OUTPUT;
        G1.GetComponent<CheckerTagScript>().Type = GradingCONSTANTS.OUTPUT;
        G2.GetComponent<CheckerTagScript>().Type = GradingCONSTANTS.OUTPUT;

        Finish.onClick.AddListener(GradeCheckInitializer);

    }

    private void GradeCheckInitializer()
    {
        Debug.Log("Finish button clicked! Checking input and output.");
        StartCoroutine(FinishChecker());
    }

    private void AddCheckMarkOrCross(bool isCheckMark)
    {
        int count = MarksList.Count;
        if (isCheckMark)
        {
            GameObject check = new GameObject("Check");
            check.transform.parent = this.gameObject.transform;
            check.transform.position = new Vector3(-3.3f + count * .9f, 3.10f, 0);
            SpriteRenderer spriteRenderer = check.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = checkMarkSprite;
            MarksList.Add(check);

        }
        else if (!isCheckMark)
        {
            GameObject cross = new GameObject("Cross");
            cross.transform.parent = this.gameObject.transform;
            cross.transform.position = new Vector3(-3.3f + count * .9f, 3.10f, 0);
            SpriteRenderer spriteRenderer = cross.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = crossMarkSprite;
            MarksList.Add(cross);
        }
    }

   
    IEnumerator FinishChecker()
    {
        for (int i = 0; i < MarksList.Count; i++)
        {
            Destroy(MarksList[i]);
        }
        MarksList.Clear();
        CheckerTagScript B2Tag = B2.GetComponent<CheckerTagScript>();
        CheckerTagScript B1Tag = B1.GetComponent<CheckerTagScript>();
        CheckerTagScript B0Tag = B0.GetComponent<CheckerTagScript>();
        CheckerTagScript Gate1Tag = Gate1.GetComponent<CheckerTagScript>();
        CheckerTagScript G0Tag = G0.GetComponent<CheckerTagScript>();
        CheckerTagScript G1Tag = G1.GetComponent<CheckerTagScript>();
        CheckerTagScript G2Tag = G2.GetComponent<CheckerTagScript>();

        if (B2Tag.GetCollidingObject() == null || B1Tag.GetCollidingObject() == null
            || B0Tag.GetCollidingObject() == null || Gate1Tag.GetCollidingObject() == null || G0Tag.GetCollidingObject() == null
            || G1Tag.GetCollidingObject() == null || G2Tag.GetCollidingObject() == null)
        {
            Debug.Log("All tags are not SNAPPED!");
            yield break;
        }
        Switch B2Switch = B2Tag.GetCollidingObject().GetComponent<Switch>();
        Switch B1Switch = B1Tag.GetCollidingObject().GetComponent<Switch>();
        Switch B0Switch = B0Tag.GetCollidingObject().GetComponent<Switch>();
        XORGate Gate1XOR = Gate1Tag.GetCollidingObject().GetComponent<XORGate>();
        LEDScript G0LED = G0Tag.GetCollidingObject().GetComponent<LEDScript>();
        LEDScript G1LED = G1Tag.GetCollidingObject().GetComponent<LEDScript>();
        LEDScript G2LED = G2Tag.GetCollidingObject().GetComponent<LEDScript>();


        // b000 - g000
        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip(); 
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip(); 
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip(); 
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip(); 
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip(); 
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip(); 
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip(); 
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        // b001 - g001
        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip(); 
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip(); 
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip(); 
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip(); 
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip(); 
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip(); 
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        // b010 - g011
        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);        

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);
        
        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);
        
        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);
        
        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);
        
        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);
        
        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);
        
        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);
        
        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        // b011 - g010
        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != true && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        // b100 - g110
        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        // b101 - g111
        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != true && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        // d110 - g101
        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != true && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        // d111 - g100
        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (!G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && !G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && !G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        B2Switch.ToggleSwitch(false); B1Switch.ToggleSwitch(false); B0Switch.ToggleSwitch(false); Gate1XOR.ClearChip();
        logicManager.ResetAllLogic();
        yield return new WaitForSecondsRealtime(1);
        if (G0LED.isLEDON() && G1LED.isLEDON() && G2LED.isLEDON() && B2Switch.SwitchUp != false && B1Switch.SwitchUp != false && B0Switch.SwitchUp != false && !Gate1XOR.IsDeviceOn() == true && numTries <= 3)
        {
            Debug.Log("Incorrect Output");
            LabThreeGrade -= 5;
            numTries++;
            AddCheckMarkOrCross(false);
            yield break;
        }
        AddCheckMarkOrCross(true);

        Debug.Log("Correct output!");
        DataInsert.inputLab1Grade += LabThreeGrade;
        yield return new WaitForSecondsRealtime(5);
        SceneManager.LoadScene("Scenes/Postlab3");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator pushtoDataBase()
    {

        string email = dbManager.email;
        int grade = DataInsert.inputPrelab4Grade;
        string lab = "labThree";
        //preLabOne
        //postLabOne

        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("grade", grade);
        form.AddField("Lab", lab);

        WWW www = new WWW("https://dldvirtuallab.000webhostapp.com/labGrade.php", form);
        yield return www;

        if (www.text == "0")
        {
            Debug.Log("Grade entered successfully");
        }
        else
        {
            Debug.Log("Error" + www.text);
        }



    }


}

