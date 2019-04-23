//written by George Melman-Kenny
//Debugged by George Melman-Kenny
//Tested by George Melman-Kenny
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class dbManager  {

public static string email;

public static string section;

public static string labgradeRequired;

public static bool LoggedIn { get { return email != null;}}
	
	public static void LogOut(){
		email = null;
	}
}
