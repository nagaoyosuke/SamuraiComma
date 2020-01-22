using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using SamuraiComma.Main.Manager;

public class DebugDisplayGameState : MonoBehaviour
{
    [Inject] private GameStateManager _manager;
    [SerializeField] private Text _text;

    private void Update()
    {
        _text.text = _manager.CurrentGameState.Value.ToString();
    }
}
