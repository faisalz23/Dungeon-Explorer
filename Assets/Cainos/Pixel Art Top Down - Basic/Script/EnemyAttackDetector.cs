using UnityEngine;

public class EnemyAttackDetector : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playermovement player = collision.GetComponent<playermovement>();
        if (player != null)
        {
            // Serang player tanpa knockback
            player.TakeDamage(damage, Vector2.zero, false);
        }
    }
}
