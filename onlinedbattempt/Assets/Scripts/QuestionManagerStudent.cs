using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class QuestionManagerStudent : MonoBehaviour
{
    [SerializeField]
    List<Message2> messageList = new List<Message2>();
    public int maxMessages = 20;
    public GameObject chatPanel, SquestionObject;
    public Color playerMessage, info;
    public string listItem;
    public int messageCount = 3;
    public int x = 0;
    public Button goBack, defaults;

    public string[] Questiontext;
    public string[] Answertext;

  
    void Start()
    {
        messageCount += 1;
        listItem ="\n"+messageCount + ". Question: How do I place wires?\n" + messageCount + ". Answer: While in a lab or sandbox mode, press the 'W' key on your keyboard.\n";
        SendMessageToChat(listItem, Message2.MessageType.playerMessage);

        messageCount += 1;
        string listItem1 = messageCount + ". Question: How can I do well in the experiments?\n" + messageCount + ". Answer: Practice using sandbox mode and review the logic behind the gates.\n";
        SendMessageToChat(listItem1, Message2.MessageType.playerMessage);

        messageCount += 1;
        string listItem2 = messageCount + ". Question: How long do I have to complete the lab?\n" + messageCount + ". Answer: As of now the time given for each lab to be completed is unlimited however there are deadlines and a limited number of attempts. An attempt has to be made in order to receive credit.\n";
        SendMessageToChat(listItem2, Message2.MessageType.playerMessage);




        goBack.onClick.AddListener(GotoStudentpage);
        defaults.onClick.AddListener(defaultQs);
        StartCoroutine(addMessagesFromDatabase());

    }

    private void defaultQs()
    {
        messageCount += 1;
        listItem = messageCount + ". Question: How do I place wires?\n" + messageCount + ". Answer: While in a lab or sandbox mode, press the 'W' key on your keyboard.\n";
        SendMessageToChat(listItem, Message2.MessageType.playerMessage);

        messageCount += 1;
        string listItem1 = messageCount + ". Question: How can I do well in the experiments?\n" + messageCount + ". Answer: Practice using sandbox mode and review the logic behind the gates.\n";
        SendMessageToChat(listItem1, Message2.MessageType.playerMessage);

        messageCount += 1;
        string listItem2 = messageCount + ". Question: How long do I have to complete the lab?\n" + messageCount + ". Answer: As of now the time given for each lab to be completed is unlimited however there are deadlines and a limited number of attempts. An attempt has to be made in order to receive credit.\n";
        SendMessageToChat(listItem2, Message2.MessageType.playerMessage);

    }

    private void GotoStudentpage()
    {
        x = 0;
        SceneManager.LoadScene("Scenes/StudentSubsystem");
    }


    void Update()
    {
        /*if (question.text != "" && answer.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                listItem = messageCount + " Question: " + question.text + "\n" + messageCount + " Answer: " + answer.text +"\n";
                SendMessageToChat(listItem, Message1.MessageType.playerMessage);
                question.text = "";
                answer.text = "";
            }
        }
        else
        {
            if (!question.isFocused && Input.GetKeyDown(KeyCode.Return))
            {
                question.ActivateInputField();
                answer.ActivateInputField();
            }
        }*/
        
    }

    /*public void Send()
    {


        if (question.text != "" && answer.text != "")
        {
            messageCount += 1;
            listItem = messageCount + ". Question: " + question.text + "\n" + messageCount + ". Answer: " + answer.text + "\n";
            SendMessageToChat(listItem, Message1.MessageType.playerMessage);

            question.text = "";
            answer.text = "";
        }
        else
        {
            question.ActivateInputField();
            answer.ActivateInputField();
        }

    }*/


    IEnumerator addMessagesFromDatabase(){
        WWW www = new WWW("https://dldvirtuallab.000webhostapp.com/FAQupdatequestion.php");
        yield return www;
        WWW aaa = new WWW("https://dldvirtuallab.000webhostapp.com/FAQupdateanswer.php");

        yield return aaa;
        if(aaa.text != "11" && www.text != "10" && aaa.text != "" && www.text != ""){
             
            string itemsDataString = www.text;

             Questiontext = itemsDataString.Split('|'); 

             string AnswersString = aaa.text;

             Answertext = AnswersString.Split('|');
             //print(Answertext[1]);
            int i =0;
             while(Answertext[i+1] != "" || Questiontext[i] != ""){
                 messageCount += 1;
            listItem = messageCount + ". Question: " + Questiontext[i] + "\n" + messageCount + ". Answer: " + Answertext[i+1] + "\n";
            SendMessageToChat(listItem, Message2.MessageType.playerMessage);
            
                 
                 
                // print(Questiontext[i]);
                // print(Answertext[i+1]);
                 i+=2;
             }
            
             /* messageCount += 1;
        listItem = messageCount + ". Question: " + Questiontext + "\n" + messageCount + ". Answer: " + Answertext + "\n";
        SendMessageToChat(listItem, Message1.MessageType.playerMessage);
        Question.text = "";
        Answer.text = "";*/
          // print(www.text);
           //print(Questiontext[2]);
           //print(GetQuestionText(Questiontext[1], "t"));

        }
		
		
		 //Create Button
        
    }

    public void SendMessageToChat(string text, Message2.MessageType messageType)
    {
        

        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].SquestionObject.gameObject);
            messageList.Remove(messageList[0]);

        }


        Message2 newMessage = new Message2();

        newMessage.text1 = text;

        GameObject newText = Instantiate(SquestionObject, chatPanel.transform);

        newMessage.SquestionObject = newText.GetComponent<Text>();

        newMessage.SquestionObject.text = newMessage.text1;


        messageList.Add(newMessage);

    }


}

[System.Serializable]
public class Message2
{

    public string text1;
    public MessageType messageType;
    internal Text SquestionObject;

    public enum MessageType
    {
        playerMessage,
        info,
    }
}


