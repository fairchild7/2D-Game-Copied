using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 250;
    [SerializeField] private float jumpForce = 400;

    private bool isGrounded = true;
    private bool isJumping = false;
    private bool isAttack = false;
    private bool isDeath = false;

    private float horizontal;

    private int coin = 0;

    private Vector3 savePoint;

    // Start is called before the first frame update
    void Start()
    {
        SavePoint();
        OnInit();
    }

    void Update()
    {
        if (isDeath)
        {
            return;
        }

        isGrounded = CheckGrounded();

        horizontal = Input.GetAxisRaw("Horizontal");

        if (isAttack)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (isGrounded)
        {
            if (isJumping)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.X) && isGrounded)
            {
                Debug.Log("X");
                Jump();
            }

            if (Mathf.Abs(horizontal) > 0.1f)
            {
                ChangeAnim("run");
            }

            if (Input.GetKeyDown(KeyCode.Z) && isGrounded)
            {
                Debug.Log("Z");
                Attack();
            }
            
            if (Input.GetKeyDown(KeyCode.C) && isGrounded)
            {
                Debug.Log("C");
                Throw();
            }
        }

        if (!isGrounded && rb.velocity.y < 0)
        {
            ChangeAnim("fall");
            isJumping = false;
        }

        if (Mathf.Abs(horizontal) > 0.1f)
        {
            rb.velocity = new Vector2(horizontal * Time.fixedDeltaTime * speed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0? 0 : 180, 0));
            //transform.localScale = new Vector3(horizontal, 1, 1);
        }
        else if (isGrounded)
        {
            ChangeAnim("idle");
            rb.velocity = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        
    }

    public void OnInit()
    {
        isDeath = false;
        isAttack = false;
        transform.position = savePoint;
        ChangeAnim("idle");
    }

    private bool CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);

        return hit.collider != null;
    }

    private void Attack()
    { 
        ChangeAnim("attack");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
    }

    private void Throw()
    {
        ChangeAnim("throw");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
    }
    
    private void ResetAttack()
    {
        ChangeAnim("idle");
        isAttack = false;
    }

    private void Jump()
    {
        isJumping = true;
        ChangeAnim("jump");
        rb.AddForce(jumpForce * Vector2.up);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            coin++;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "DeathZone")
        {
            isDeath = true;
            ChangeAnim("die");

            Invoke(nameof(OnInit), 1f);
        }
    }

    internal void SavePoint()
    {
        savePoint = transform.position;
    }
}
