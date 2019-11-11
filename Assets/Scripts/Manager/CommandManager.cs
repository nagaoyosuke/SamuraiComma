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
    public int SwingLine;
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
            Rswing = false;
            Lswing = true;
            if(accelL.x >= SwingSence && accelL.y >= SwingSence)
            {
                SwingLine = 9;
            }
            else if (accelL.x <= -SwingSence && accelL.y >= SwingSence)
            {
                SwingLine = 7;
            }
            else if (accelL.x >= SwingSence && accelL.y <= -SwingSence)
            {
                SwingLine = 3;
            }
            else if (accelL.x <= -SwingSence && accelL.y <= -SwingSence)
            {
                SwingLine = 1;
            }
            else if (accelL.x >= -SwingSence && accelL.x <= SwingSence && accelL.y >= SwingSence)
            {
                SwingLine = 8;
            }
            else if (accelL.x >= SwingSence && accelL.y >= -SwingSence && accelL.y <= SwingSence)
            {
                SwingLine = 6;
            }
            else if (accelL.x <= -SwingSence && accelL.y >= -SwingSence && accelL.y <= SwingSence)
            {
                SwingLine = 4;
            }
            else if (accelL.x >= -SwingSence && accelL.x <= SwingSence && accelL.y <= -SwingSence)
            {
                SwingLine = 2;
            }
            else if (accelL.x >= -SwingSence && accelL.x <= SwingSence && accelL.y >= SwingSence && accelL.y <= SwingSence && accelL.z >= SwingSence)
            {
                SwingLine = 5;
            }

        }
        if (accelR.x >= SwingSence || accelR.x <= -SwingSence || accelR.y >= SwingSence || accelR.y <= -SwingSence || accelR.z >= SwingSence || accelR.z <= -SwingSence)
        {
            Lswing = false;
            Rswing = true;
            if (accelR.x >= SwingSence && accelR.y >= SwingSence)
            {
                SwingLine = 9;
            }
            else if (accelR.x <= -SwingSence && accelR.y >= SwingSence)
            {
                SwingLine = 7;
            }
            else if (accelR.x >= SwingSence && accelR.y <= -SwingSence)
            {
                SwingLine = 3;
            }
            else if (accelR.x <= -SwingSence && accelR.y <= -SwingSence)
            {
                SwingLine = 1;
            }
            else if (accelR.x >= -SwingSence && accelR.x <= SwingSence && accelR.y >= SwingSence)
            {
                SwingLine = 8;
            }
            else if (accelR.x >= SwingSence && accelR.y >= -SwingSence && accelR.y <= SwingSence)
            {
                SwingLine = 6;
            }
            else if (accelR.x <= -SwingSence && accelR.y >= -SwingSence && accelR.y <= SwingSence)
            {
                SwingLine = 4;
            }
            else if (accelR.x >= -SwingSence && accelR.x <= SwingSence && accelR.y <= -SwingSence)
            {
                SwingLine = 2;
            }
            else if (accelR.x >= -SwingSence && accelR.x <= SwingSence && accelR.y >= SwingSence && accelR.y <= SwingSence && accelR.z >= SwingSence)
            {
                SwingLine = 5;
            }
        }
    }
    
}
