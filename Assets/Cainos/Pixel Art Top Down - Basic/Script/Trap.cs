using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    // Start is called before the first frame update
   public int damage = 20;
    public float knockbackForce = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playermovement player = collision.GetComponent<playermovement>();
        if (player != null)
        {
            Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;
            player.TakeDamage(damage, knockbackDir, true);
            Debug.Log("Player kena TRAP!");

        }
    }
} 
