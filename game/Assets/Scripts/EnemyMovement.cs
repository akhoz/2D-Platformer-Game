using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] float speed = 1f;
    
    Rigidbody2D rb;
    BoxCollider2D turnAroundCollider;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        rb.velocity = new Vector2(speed, 0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        speed = -speed;
        FlipEnemyFacing();
    }
    
    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)), 1f);
    }
}
