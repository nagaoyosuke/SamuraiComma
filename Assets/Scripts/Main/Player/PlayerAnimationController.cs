using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamuraiComma.Main.Player
{

    /// <summary>
    /// 2019/10/31 toyoda
    /// Playerのアニメーションをスクリプトから操作するときに使用する。
    /// </summary>

    public class PlayerAnimationController : MonoBehaviour
    {

        private Animator animator;

        private const string keyIdle = "isIdle";
        private const string keyAttack = "isAttack";
        private const string keyDeath = "isDeath";

        private void Start()
        {
            animator = this.gameObject.GetComponent<Animator>();
        }

        public void PlayAnimation(string key)
        {
            animator.SetBool(key, true);
            StartCoroutine(DelayClass.DelayCoroutin(1, () => animator.SetBool(key, false)));

        }
    }
}


