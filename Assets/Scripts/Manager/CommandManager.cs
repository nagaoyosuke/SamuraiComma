using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    private List<Joycon> m_joycons;
    private Joycon joyconL;
    private Joycon joyconR;
    public bool Lswing;
    public bool Rswing;
    // Start is called before the first frame update
    void Start()
    {
        m_joycons = JoyconManager.Instance.j;
        joyconL = m_joycons.Find(c => c.isLeft);
        joyconR = m_joycons.Find(c => !c.isLeft);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 accelL = joyconL.GetGyro();
        Vector3 accelR = joyconR.GetGyro();
        //コントローラーから方向を受け取る部分
        //RswingおよびLswingで判定する
        if (accelL.x >= 1.0|| accelL.y >= 1.0|| accelL.z >= 1.0)
        {
            Rswing = false;
            Lswing = true;
        }
        if (accelR.x >= 1.0 || accelR.y >= 1.0 || accelR.z >= 1.0)
        {
            Lswing = false;
            Rswing = true;
        }
    }
    
}
