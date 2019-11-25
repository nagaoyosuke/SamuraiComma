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
    public int SwingLineL;
    public int SwingLineR;
    [SerializeField, Range(0.1f, 10.0f)] float SwingSence = 1.0f;
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
        if (accelL.x >= SwingSence || accelL.x <= -SwingSence || accelL.y >= SwingSence || accelL.y <= -SwingSence || accelL.z >= SwingSence || accelL.z <= -SwingSence)
        {
            Lswing = true;
            
            if (accelL.x >= -SwingSence && accelL.x <= SwingSence && accelL.y >= SwingSence)
            {
                SwingLineL = 8;
            }
            if (accelL.x >= SwingSence && accelL.y >= -SwingSence && accelL.y <= SwingSence)
            {
                SwingLineL = 4;
            }
            if (accelL.x <= -SwingSence && accelL.y >= -SwingSence && accelL.y <= SwingSence)
            {
                SwingLineL = 6;
            }
            if (accelL.x >= -SwingSence && accelL.x <= SwingSence && accelL.y <= -SwingSence)
            {
                SwingLineL = 2;
            }
            

        }
        if (accelR.x >= SwingSence || accelR.x <= -SwingSence || accelR.y >= SwingSence || accelR.y <= -SwingSence || accelR.z >= SwingSence || accelR.z <= -SwingSence)
        {
            Rswing = true;
            
            if (accelR.x >= -SwingSence && accelR.x <= SwingSence && accelR.y >= SwingSence)
            {
                SwingLineR = 8;
            }
            if (accelR.x >= SwingSence && accelR.y >= -SwingSence && accelR.y <= SwingSence)
            {
                SwingLineR = 4;
            }
            if (accelR.x <= -SwingSence && accelR.y >= -SwingSence && accelR.y <= SwingSence)
            {
                SwingLineR = 6;
            }
            if (accelR.x >= -SwingSence && accelR.x <= SwingSence && accelR.y <= -SwingSence)
            {
                SwingLineR = 2;
            }
            
        }
    }
    
}
