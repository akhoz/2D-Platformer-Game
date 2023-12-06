using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator anim;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        Run();
        FlipSprite();
    }
    
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void Run()
    {
        rb.velocity = new Vector2(moveInput.x * speed, moveInput.y );
        
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
