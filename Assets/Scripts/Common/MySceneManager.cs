using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : SingletonMonoBehaviour<MySceneManager> {

	public static void GoTitle(){
		SceneManager.LoadScene("TitleScene");
	}

    public static void GoMain()
    {
        SceneManager.LoadScene("MainScene");
    }
}
