using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    [SerializeField]
    List<Message1> messageList = new List<Message1>();
    public string username;
    public int maxMessages = 20;
    public GameObject chatPanel, QuestionObject;
    public InputField Question;
    public InputField Answer;
    public Color playerMessage, info;
    public string listItem;
    public int messageCount=1;

    public string[] Questiontext;
    public string[] Answertext;

    void Start()
    {
        messageCount += 1;
        listItem = messageCount + ". Question: How do I place wires?\n" + messageCount + ". Answer: While in a lab or sandbox mode, press the 'W' key on your keyboard.\n";
        SendMessageToChat(listItem, Message1.MessageType.playerMessage);
        Question.text = "";
        Answer.text = "";


        messageCount += 1;
        string listItem1 = messageCount + ". Question: How can I do well in the experiments?\n" + messageCount + ". Answer: Practice using sandbox mode and review the logic behind the gates.\n";
        SendMessageToChat(listItem1, Message1.MessageType.playerMessage);
        Question.text = "";
        Answer.text = "";

        messageCount += 1;
        string listItem2 = messageCount + ". Question: How long do I have to complete the lab?\n" + messageCount + ". Answer: As of now the time given for each lab to be completed is unlimited however there are deadlines and a limited number of attempts. An attempt has to be made in order to receive credit.\n";
        SendMessageToChat(listItem2, Message1.MessageType.playerMessage);
        Question.text = "";
        Answer.text = "";

        StartCoroutine(addMessagesFromDatabase());


    }
    /*void Update()
    {
        if (Question.text != "" && Answer.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                listItem = messageCount + " Question: " + Question.text + "\n" + messageCount + " Answer: " + Answer.text +"\n";
                SendMessageToChat(listItem, Message1.MessageType.playerMessage);
                Question.text = "";
                Answer.text = "";
            }
        }
        else
        {
            if (!Question.isFocused && Input.GetKeyDown(KeyCode.Return))
            {
                Question.ActivateInputField();
                Answer.ActivateInputField();
            }
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
            SendMessageToChat(listItem, Message1.MessageType.playerMessage);
            Question.text = "";
            Answer.text = "";
                 
                 
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
    
    string GetQuestionText(string data, string index){
        string value = data.Substring(data.IndexOf(index)+index.Length);
        return value;
    }

    public void Send()
    {


        if (Question.text != "" && Answer.text != "")
        {
            /* messageCount += 1;
            listItem = messageCount + ". Question: " + Question.text + "\n" + messageCount + ". Answer: " + Answer.text + "\n";
            SendMessageToChat(listItem, Message1.MessageType.playerMessage);

            Question.text = "";
            Answer.text = "";
            */
            StartCoroutine(addFAQ());
        }
        else
        {
            Question.ActivateInputField();
            Answer.ActivateInputField();
        }

    }

    public void SendMessageToChat(string text, Message1.MessageType messageType)
    {

        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].QuestionObject.gameObject);
            messageList.Remove(messageList[0]);

        }


        Message1 newMessage = new Message1();

        newMessage.text1 = text;

        GameObject newText = Instantiate(QuestionObject, chatPanel.transform);

        newMessage.QuestionObject = newText.GetComponent<Text>();

        newMessage.QuestionObject.text = newMessage.text1;
        

        messageList.Add(newMessage);

    }




     IEnumerator addFAQ(){
        
         
         WWWForm form = new WWWForm();
        form.AddField("Question", Question.text);
        form.AddField("Answer", Answer.text);
        
        WWW www = new WWW("https://dldvirtuallab.000webhostapp.com/FAQcreate.php", form);
        yield return www;

        if(www.text == "0"){
        Debug.Log("FAQ Created Successfully");
            messageCount += 1;
            listItem = messageCount + ". Question: " + Question.text + "\n" + messageCount + ". Answer: " + Answer.text + "\n";
            SendMessageToChat(listItem, Message1.MessageType.playerMessage);

            Question.text = "";
            Answer.text = "";
        }

        else{
            Debug.Log("Failed to create Section Error"+www.text);
        }


        
       Debug.Log(www.text);
     }


}

[System.Serializable]
public class Message1
{

    public string text1;
    public MessageType messageType;
    internal Text QuestionObject;

    public enum MessageType
    {
        playerMessage,
        info,
    }
}


