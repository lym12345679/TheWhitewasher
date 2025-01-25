using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public bool isGround;
    public bool isLeftWall;
    public bool isRightWall;
    // Update is called once per frame
    void Update()
    {
        Jump();
    }
    void FixedUpdate()
    {
        CheckIsGround();
        Move();
    }
    private void Move()
    {
        CheckIsWall();
        if (Input.GetKey(KeyCode.A) && !isLeftWall)
        {
            transform.Translate(Vector3.left * Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D) && !isRightWall)
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime);
        }

    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpForce);
        }
    }

    private void CheckIsGround()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, .3f, 1 << this.gameObject.layer);
        if (hits.Length > 1)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }
    private void CheckIsWall()
    {
        RaycastHit2D[] hits1 = Physics2D.RaycastAll(transform.position, Vector2.right, .3f, 1 << this.gameObject.layer);
        RaycastHit2D[] hits2 = Physics2D.RaycastAll(transform.position, Vector2.left, .3f, 1 << this.gameObject.layer);
        //Debug.Log(hits1.Length);
        if (hits2.Length > 1)
        {
            isLeftWall = true;
        }
        else
        {
            isLeftWall = false;
        }
        if (hits1.Length > 1)
        {
            isRightWall = true;
        }
        else
        {
            isRightWall = false;
        }

    }

}
