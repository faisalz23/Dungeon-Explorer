using UnityEngine;

public class AttackAreaDetector : MonoBehaviour
{
    public int damage = 25; // Sesuaikan tipe ke int jika EnemyHealth pakai int

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
