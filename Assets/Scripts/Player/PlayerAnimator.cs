using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public Animator OutlineAnimator;
    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    public Vector2 velocity
    {
        get
        {
            return rb.velocity;
        }
    }
    public bool IsGround
    {
        set
        {
            animator.SetBool("IsGround", value);
            OutlineAnimator.SetBool("IsGround", value);
        }
    }
    public bool IsWalking
    {
        set
        {
            animator.SetBool("IsWalking", value);
            OutlineAnimator.SetBool("IsWalking", value);
        }
    }
    public void Jump()
    {
        animator.SetTrigger("Jump");
        OutlineAnimator.SetTrigger("Jump");
    }
    void Update()
    {
        animator.SetFloat("VelocityY", velocity.y);
        OutlineAnimator.SetFloat("VelocityY", velocity.y);
    }

}
