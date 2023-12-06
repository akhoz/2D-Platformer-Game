using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator anim;
    CapsuleCollider2D bodyCollider;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
    }
    
    void Update()
    {
        Run();
        FlipSprite();
    }
    
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log("Move");
    }
    
    void OnJump(InputValue value)
    {
        if (value.isPressed && bodyCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
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
    
    void FlipSprite()
    {
        bool isMoving = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon; //Mathf.Epsilons basically 0
        
        if (isMoving)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }
}
