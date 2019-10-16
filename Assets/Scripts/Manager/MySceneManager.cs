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

	public static void GoResult(){
		SceneManager.LoadScene("Result");
	}

	public static void GoExplanatoryText(){
		SceneManager.LoadScene("ExplanatoryText");
	}

}
