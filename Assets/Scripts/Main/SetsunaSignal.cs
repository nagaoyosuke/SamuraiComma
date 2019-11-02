using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//仮
public class SetsunaSignal : MonoBehaviour
{   
    [SerializeField]
    private Image signalImage;

    //カメラ演出などが終わったら
    public bool isFinishedDirection;
    private bool isDisplaySignal;
    [SerializeField]
    private TempBattle battle;
    [SerializeField]
    private ScreenFader screenFader;

    //早く押しすぎの場合
    private bool isQuicklyPush;

    // Update is called once per frame
    void Update()
    {

        if (isDisplaySignal)
        {
            if (Input.anyKeyDown)
            {
                isDisplaySignal = false;
                isQuicklyPush = true;   //仮
                signalImage.enabled = false;
                Sound.PlaySe("syakin");
                battle.setsunaButton.SetActive(true);
                screenFader.isFadeOutTranslucent = true;
            }
        }

        }

    public IEnumerator Until(float time){
        /*
        if (!isDisplaySignal && !isQuicklyPush)
        {
            battle.BeBeaten();
            isQuicklyPush = true;
        }
        */

        float untilShowSignalTime = time;
        yield return new WaitForSeconds(untilShowSignalTime);
        if (!isQuicklyPush)
        {
            signalImage.enabled = true;
            isDisplaySignal = true;
            Sound.PlaySe("taiko01");
        }

    }
}
