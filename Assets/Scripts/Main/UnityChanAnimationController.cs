using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanAnimationController : MonoBehaviour
{

    private Animator animator;

    private const string keyIdle = "isIdle";
    private const string keyAttack = "isAttack";

    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayAnimation(string key){
        animator.SetBool(key, true);
        StartCoroutine(DelayClass.DelayCoroutin(1, () => animator.SetBool(key, false)));

    }
}
