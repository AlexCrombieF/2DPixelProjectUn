using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D),typeof(TouchingDirections))]
public class Knight : MonoBehaviour
{
    public float walkSpeed = 3f;

    Rigidbody2D rb;
    TouchingDirections touchingDirections;

    public enum WalkabeleDirection {  Right , Left };   

    private WalkabeleDirection _walkableDirection;
    private Vector2 walkDirectionVector = Vector2.right;
    
    public WalkabeleDirection WalkDirection 
    {
        get { return _walkableDirection; }
        set
        {
            if (_walkableDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if(value == WalkabeleDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                } else if (value == WalkabeleDirection.Left)
                { 
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkableDirection = value;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }

        rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
    }

    private void FlipDirection()
    {
        if(WalkDirection == WalkabeleDirection.Right)
        {
            WalkDirection = WalkabeleDirection.Left;
        }
        else if(WalkDirection == WalkabeleDirection.Left)
        {
            WalkDirection = WalkabeleDirection.Right;
        } else
        {
            Debug.LogError("Current walkable direction aint legal no left or right values bruh");
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
