//written by George Melman-Kenny
//Debugged by George Melman-Kenny
//Tested by George Melman-Kenny
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class newuser : MonoBehaviour {


	public Button BtnEnter; 
	public Button BtnExit;
    public InputField Inputemail;
    public InputField InputFirstname;
    public InputField Inputlastname;


    public InputField InputPw1;

    public InputField InputPw2;

    /*
=======
>>>>>>> 4e1e4a10e74206e2e61a05187535abaaf76d3cde
    
	public linkedListStack studentList;
	public class Node{
        internal string email;
        internal string first;
        internal string last;
        internal string pass;
        internal Node next;

        public Node(string  email, string  first, string  last, string  pass){
            this.email = email;
            this.first = first;
            this.last = last;
            this.pass = pass;
            next = null;
        }
    }

    public class linkedListStack{
        Node head;

        public linkedListStack(){
            this.head = null;
        }
        internal void push(string email, string  first, string  last, string  pass){
           Node temp = new Node(email,first,last,pass);
		    if (head == null){
                temp.next = null;
            }
            else{
                temp.next = head;
            }
            head = temp;
        }
        internal void pop(){
            if(head != null){
                head = head.next;
            }
        }

		internal bool searchForSameEmail(string email){
			Node iter = head;
			while(iter != null){
				
				if(iter.email == email){
				 return true;
				}
				
				iter = iter.next;
				
			}
			return false;
		}
    }

	private void OnLoginClick() //rename to w/e button name
    {
        if (string.IsNullOrEmpty(Inputemail.text) || string.IsNullOrEmpty(InputFirstname.text) || string.IsNullOrEmpty(Inputlastname.text))
        {
            Toast.Instance.Show("Please fill in every field");
            return;
        }
        StartCoroutine(Check());
    }

	IEnumerator Check()
    {
        
        if (studentList.searchForSameEmail(Inputemail.text))
        {
            
                Debug.Log("This email is already in the system");
				SceneManager.LoadScene("Scenes/newuser");
           yield break;
        }

		else{
			studentList.push(Inputemail.text,InputFirstname.text,Inputlastname.text, "temp" );
			SceneManager.LoadScene("Scenes/AdminSubsystem");
			yield break;
		}
    }



	// Use this for initialization
	void Start () {
		BtnLogin.onClick.AddListener(OnLoginClick);
        BtnExit.onClick.AddListener(() => Application.Quit());
	}

    */


	public void CallRegister(){
        StartCoroutine(Register());
    }

    IEnumerator Register(){
         WWWForm form = new WWWForm();
        form.AddField("firstname", InputFirstname.text);
        form.AddField("lastname", Inputlastname.text);
        form.AddField("email", Inputemail.text);
        form.AddField("password", InputPw1.text);
        
        WWW www = new WWW("https://dldvirtuallab.000webhostapp.com/register.php", form);
        yield return www;
        
        if(www.text == "0"){
            SceneManager.LoadScene("Login");
        }

        else{
       Debug.Log(www.text);
        };
       
        
    }

	public void verifyInputs(){
        BtnEnter.interactable = (Inputemail.text.Length >= 1 && InputFirstname.text.Length >= 1 && Inputlastname.text.Length >= 1 && InputPw1.text.Length >=1 && InputPw2.text.Length >= 1 && InputPw1.text == InputPw2.text);
    }
}


	
	


