using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SamuraiComma.Main.Manager;
/// <summary>
/// 仮 toyoda
/// </summary>
public class DebugLogin : MonoBehaviour
{
    private IEnumerator Start()
    {

        //ログインまで行けた
        yield return new WaitForSeconds(1.0f);

        JsonManager.Send.LoginJson json = new JsonManager.Send.LoginJson(-1, "test1", "koji", "hirakata");
        SamuraiComma.Main.WS.WSManager.Send(json.ToJson());

        yield return new WaitForSeconds(1.0f);

        JsonManager.Send.MatchingJson json2 = new JsonManager.Send.MatchingJson(1, "ここ", true);
        SamuraiComma.Main.WS.WSManager.Send(json2.ToJson());

    }
}
