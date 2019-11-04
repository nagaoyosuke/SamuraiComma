using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//joy-conのやつができるまでの繋ぎ
public class TempBattle : MonoBehaviour
{
    public GameObject setsunaButton;
    public UnityChanAnimationController unityChanAnimation;
    public UnityChanAnimationController opponentUnityChanAnimation;

    public ScreenFader screenFader;
    public TempData temp;


    // Start is called before the first frame update
    void Start()
    {
        Sound.PlayBgm("wind");
    }

    public void OnClickedTempButton(){
        Sound.PlaySe("hero01");
        screenFader.isFadeIn = true;//連打するとフェイドインされないバグ
        setsunaButton.SetActive(false);

        temp.TempServer(1*60);
        temp.TempServer(5*60);
    }


    public void BeBeaten(){
        unityChanAnimation.PlayAnimation("isAttack");
        StartCoroutine(DelayClass.DelayCoroutin(80, () => opponentUnityChanAnimation.PlayAnimation("isDamaged")));
        StartCoroutine(DelayClass.DelayCoroutin(90, () => Sound.PlaySe("itawari01")));

        opponentUnityChanAnimation.PlayAnimation("isAttack");
        StartCoroutine(DelayClass.DelayCoroutin(80, () => unityChanAnimation.PlayAnimation("isDamaged")));
        StartCoroutine(DelayClass.DelayCoroutin(90, () => Sound.PlaySe("itawari01")));

    }
}
