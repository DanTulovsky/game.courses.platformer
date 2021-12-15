using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float speed = 1;


    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal") * speed;

        var rigidBody2D = GetComponent<Rigidbody2D>();
        rigidBody2D.velocity = new Vector2(horizontal, rigidBody2D.velocity.y);

        var animator = GetComponent<Animator>();
        bool walking = horizontal != 0;
        animator.SetBool("Walk", walking);
    }
}
