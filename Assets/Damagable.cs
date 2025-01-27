using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    Animator animator;
    [SerializeField]
    private float _maxHealth;
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

            if (_health < 0)
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
    public bool IsAlive { get
        {
            return _isAlive;
        }
        set
        { 
         _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
