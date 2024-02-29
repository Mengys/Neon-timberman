using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timberman : MonoBehaviour
{
    private Animator _animator;
    public static Timberman Instance;
    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
        }
        _animator = GetComponent<Animator>();
    }
    public void AnimationTrigger(string TriggerName)
    {
        _animator.SetTrigger(TriggerName);
    }
}
