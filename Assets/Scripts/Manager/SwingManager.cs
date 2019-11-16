using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwingManager : MonoBehaviour
{
    public GameObject LRmoji = null;
    [SerializeField] private CommandManager _commandManager;
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = LRmoji.GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        //JoyCon(R)、JoyCon(L)を振ると右、左と出力
        if (_commandManager.Rswing)
        {
            text.text = "右";
            switch (_commandManager.SwingLine)
            {
                case 1:
                    text.text = "右 / 左下";
                    break;
                case 2:
                    text.text = "右 / 下";
                    break;
                case 3:
                    text.text = "右 / 右下";
                    break;
                case 4:
                    text.text = "右 / 左";
                    break;
                case 5:
                    text.text = "右 / 突";
                    break;
                case 6:
                    text.text = "右 / 右";
                    break;
                case 7:
                    text.text = "右 / 左上";
                    break;
                case 8:
                    text.text = "右 / 上";
                    break;
                case 9:
                    text.text = "右 / 右上";
                    break;
                default:
                    break;
            }
        }
        if (_commandManager.Lswing)
        {
            text.text = "左";
            switch (_commandManager.SwingLine)
            {
                case 1:
                    text.text = "左 / 左下";
                    break;
                case 2:
                    text.text = "左 / 下";
                    break;
                case 3:
                    text.text = "左 / 右下";
                    break;
                case 4:
                    text.text = "左 / 左";
                    break;
                case 5:
                    text.text = "左 / 突";
                    break;
                case 6:
                    text.text = "左 / 右";
                    break;
                case 7:
                    text.text = "左 / 左上";
                    break;
                case 8:
                    text.text = "左 / 上";
                    break;
                case 9:
                    text.text = "左 / 右上";
                    break;
                default:
                    break;
            }
        }
    }
}
