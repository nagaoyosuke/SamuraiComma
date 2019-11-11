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
        }
        if (_commandManager.Lswing)
        {
            
            text.text = "左";
        }
    }
}
