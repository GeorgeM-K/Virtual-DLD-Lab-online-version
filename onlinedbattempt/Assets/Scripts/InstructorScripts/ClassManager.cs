using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ClassManager : MonoBehaviour
{ 
    public class Section{
        public string name;
        public string term;
        public string instructor;

        public static Section createSection(string name, string term, string instructor){
            Section newSection = new Section();
            newSection.name = name;
            newSection.term = term;
            newSection.instructor = instructor;
            return newSection;
        }
    }

    static public List<Section> allSections = new List<Section>();
    static public int numSections = 10;
    static public string instructorName = "InstructorName";
    static public string term = "Spring 2019";
    [SerializeField]
    private Text currentInstructor;
    [SerializeField]
    private Text currentTerm;
    static ClassManager instance;

    void Start(){
        if (instance != null){
            Destroy(this.gameObject);
            return;
        }
        instructorName = "InstructorName";
        term = "Spring 2019";
        instance = this;
        currentInstructor.text = instructorName+"'s Sections";
        currentTerm.text = term;

        //Populate List of all sections, need to implement network connection
        /*
        for(int i = 1; i<(numSections+1); i++){
            allSections[i-1] = (Section.createSection("Section "+i.ToString(), "Spring 2019", "InstructorName"));
        }
        */
        
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    public string getTerm(){
        return  currentTerm.text;
    }

    public string getInstructor(){
        return currentInstructor.text;
    }

    public int searchFor(string target){
        var item = allSections.FirstOrDefault(o => o.name == target);
        if (item != null){
            if (item.term != currentTerm.text || item.instructor != currentInstructor.text){
                return 0; //Same name ut different term or instructor
            }
            return 1; //Found item
        } else {
            return 0;
        }
    }

    public int delete(string target){
        var item = allSections.SingleOrDefault(o => o.name == target);
        if (item != null){
            if (item.term != currentTerm.text || item.instructor != currentInstructor.text){
                return 0; //Same name but different term or instructor
            }
            allSections.Remove(item);
            numSections-=1;
            return 1; //Found item
        } else {
            return 0;
        }
    }
}