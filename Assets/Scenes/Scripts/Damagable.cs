using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damagableHit;
    Animator animator;
    [SerializeField]
    private float _maxHealth = 100;
    public float MaxHealth
    {  get 
        { 
            return _maxHealth; 
        }
        set
        { 
            _maxHealth = value;
        } 
    }
    [SerializeField]
    private float _health = 100;
    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;

            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    [SerializeField]
    private bool _isAlive = true;

    [SerializeField]
    private bool isInvincible = false;

    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    public bool IsAlive 
    { 
        get
        {
            return _isAlive;
        }
        set
        { 
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive set" + value);
        } 
    }
    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }
    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
    }
    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        { 
          Health -= damage;
          isInvincible = true;
          animator.SetTrigger(AnimationStrings.hitTrigger);
          LockVelocity = true;
          damagableHit?.Invoke(damage, knockback);
          CharacterEvents.characterDamaged.Invoke(gameObject, damage);
          return true;
        }
        return false;
    }
}
