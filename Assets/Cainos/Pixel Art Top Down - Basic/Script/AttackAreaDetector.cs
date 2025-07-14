using UnityEngine;

public class AttackAreaDetector : MonoBehaviour
{
    public float damage = 25f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Serang musuh
        if (collision.CompareTag("Enemy"))
        {
            EnemyMovement enemy = collision.GetComponent<EnemyMovement>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("✔ Serang musuh");
            }
        }

        // Serang objek yang bisa dihancurkan
        if (collision.CompareTag("Destructible"))
        {
            DestructibleObject destructible = collision.GetComponent<DestructibleObject>();
            if (destructible != null)
            {
                destructible.TakeDamage(damage);
                Debug.Log("✔ Serang objek destructible");
            }
        }
    }
}
