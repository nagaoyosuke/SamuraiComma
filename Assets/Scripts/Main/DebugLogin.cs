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

        // ログイン
        yield return new WaitForSeconds(1.0f);
        JsonManager.Send.LoginJson json = new JsonManager.Send.LoginJson(-1, "test1", "koji", "hirakata");
        SamuraiComma.Main.WS.WSManager.Send(json.ToJson());

        // マッチング
        yield return new WaitForSeconds(1.0f);
        JsonManager.Send.MatchingJson json2 = new JsonManager.Send.MatchingJson(1, "ここ", true);
        SamuraiComma.Main.WS.WSManager.Send(json2.ToJson());

        // 初期化シーン
        yield return new WaitForSeconds(1.0f);
        JsonManager.Send.InitializingJson initializingJson = new JsonManager.Send.InitializingJson("Yosuke", "Unimaro", "Japan");
        SamuraiComma.Main.WS.WSManager.Send(initializingJson.ToJson());

        //サーバーにログインしている人のリスト
        yield return new WaitForSeconds(1.0f);
        JsonManager.Send.APIJson json3 = new JsonManager.Send.APIJson("MemberList", 1, "ss");
        print(json3.ToJson());
        SamuraiComma.Main.WS.WSManager.Send(json3.ToJson());

    }
}
