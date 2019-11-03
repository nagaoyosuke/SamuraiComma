using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//joy-conのやつができるまでの繋ぎ
public class TempBattle : MonoBehaviour
{
    public GameObject setsunaButton;
    [SerializeField]
    private Image winImage;
    public Image loseImage;
    public UnityChanAnimationController unityChanAnimation;
    public UnityChanAnimationController opponentUnityChanAnimation;

    public ScreenFader screenFader;


    // Start is called before the first frame update
    void Start()
    {
        Sound.PlayBgm("wind");
    }

    public void OnClickedTempButton(){
        Sound.PlaySe("hero01");
        screenFader.isFadeIn = true;//連打するとフェイドインされないバグ
        setsunaButton.SetActive(false);
        unityChanAnimation.PlayAnimation("isAttack");
        StartCoroutine(DelayClass.DelayCoroutin(80, () => opponentUnityChanAnimation.PlayAnimation("isDamaged")));
        StartCoroutine(DelayClass.DelayCoroutin(90, () => Sound.PlaySe("itawari01")));
        StartCoroutine(DelayClass.DelayCoroutin(150, () => winImage.enabled = true));
    }


    public void BeBeaten(){
        opponentUnityChanAnimation.PlayAnimation("isAttack");
        StartCoroutine(DelayClass.DelayCoroutin(80, () => unityChanAnimation.PlayAnimation("isDamaged")));
        StartCoroutine(DelayClass.DelayCoroutin(90, () => Sound.PlaySe("itawari01")));
        StartCoroutine(DelayClass.DelayCoroutin(150, () => loseImage.enabled = true));

    }
}
