using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    
    private Animator _animator;
    private static readonly int OpenProp = Animator.StringToHash("Open");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    [ContextMenu("Open Door")]
    private void Open()
    {
        _animator.SetTrigger(OpenProp);
    }

    private void Update()
    {
        
    }
}
