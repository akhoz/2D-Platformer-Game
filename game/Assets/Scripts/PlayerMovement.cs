using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 7f;
    [SerializeField] float deathKick = 5f;
    
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator anim;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D feetCollider;
    float gravityScaleAtStart;

    bool isAlive = true;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = rb.gravityScale;
    }
    
    void Update()
    {
        if (!isAlive) { return; }
        
        Run();
        FlipSprite();
        Climb();
        Die();
    }
    
    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        
        moveInput = value.Get<Vector2>();
        Debug.Log("Move");
    }
    
    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        
        if (value.isPressed && feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            rb.velocity += new Vector2(0f, jumpSpeed);
            Debug.Log("Jump");
        }
    }

    void Run()
    {
        rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y );
        
        bool isMoving = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon; //Mathf.Epsilons basically 0
        anim.SetBool("isRunning", isMoving);
    }

    void Climb()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rb.velocity = new Vector2(rb.velocity.x, moveInput.y * climbSpeed);
            rb.gravityScale = 0f;
            bool isMoving = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
            anim.SetBool("isClimbing", isMoving);
        }
        else
        {
            rb.gravityScale = gravityScaleAtStart;
            anim.SetBool("isClimbing", false);
        }
    }
    
    void FlipSprite()
    {
        bool isMoving = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon; //Mathf.Epsilons basically 0
        
        if (isMoving)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }

    void Die()
    {
        if (rb.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            anim.SetTrigger("Dying");
            rb.velocity = new Vector2(0f, deathKick);
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}
