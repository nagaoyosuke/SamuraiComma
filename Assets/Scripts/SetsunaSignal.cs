using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Start is called before the first frame update
    void Start()
    {
        isFinishedDirection = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFinishedDirection)
        {
            isFinishedDirection = false;
            StartCoroutine(Until());
        }

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

        if (!isDisplaySignal && !isQuicklyPush)
          {
                if (Input.anyKeyDown)
                {
                    print("お手つき");
                battle.BeBeaten();
                isQuicklyPush = true;
                }

            }
        }

    private IEnumerator Until(){
        float untilShowSignalTime = Random.Range(4.0f, 8.0f);
        yield return new WaitForSeconds(untilShowSignalTime);
        if (!isQuicklyPush)
        {
            signalImage.enabled = true;
            isDisplaySignal = true;
            Sound.PlaySe("taiko01");
        }

    }
}
