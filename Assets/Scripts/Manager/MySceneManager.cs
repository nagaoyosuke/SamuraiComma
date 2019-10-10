using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : SingletonMonoBehaviour<MySceneManager> {

	public static void GoTitle(){
		SceneManager.LoadScene("Title");
	}

    public static void GoMain()
    {
        SceneManager.LoadScene("Main");
    }

    public static void GoMenu()
    {
        SceneManager.LoadScene("Menu");
    }

	public static void GoTutorial(){
		SceneManager.LoadScene("Tutorial");
	}

	public static void GoResult(){
		SceneManager.LoadScene("Result");
	}

	public static void GoModeSelect(){
		SceneManager.LoadScene("ModeSelect");
	}

	public static void GoCredit(){
		SceneManager.LoadScene("Credit");
	}

	public static void GoExplanatoryText(){
		SceneManager.LoadScene("ExplanatoryText");
	}

}
