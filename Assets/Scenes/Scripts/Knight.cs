using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D),typeof(TouchingDirections))]
public class Knight : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float walkStopRate =  0.6f;
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;

    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator animator;
    Damagable damagable;

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

    public bool _hasTarget = false;
    public bool HasTarget { get { return _hasTarget; } private set
       { 
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
       } 
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public float AttackCooldown
    { 
        get
        {
            return animator.GetFloat(AnimationStrings.attackCooldown);
        }
        private set
        {
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value,0));
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damagable = GetComponent<Damagable>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }
        if (!damagable.LockVelocity)
        {
            if (CanMove)
                rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
            else
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
        }

    }

    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        if(AttackCooldown > 0) { AttackCooldown -= Time.deltaTime; }      
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

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
    public void OnCliffDetected()
    {
        if (touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
    }

}
