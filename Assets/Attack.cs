using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage;
    public Vector2 knockback = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable damagable = collision.GetComponent<Damagable>();

        if (damagable != null)
        {
         bool gotHit = damagable.Hit(attackDamage, knockback);
            if (gotHit)
            Debug.Log(collision.name + "hit for" + attackDamage);

        }

    }
}
