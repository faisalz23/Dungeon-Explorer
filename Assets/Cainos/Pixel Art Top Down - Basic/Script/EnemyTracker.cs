using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    public static EnemyTracker instance;

    private int totalEnemies = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("✅ EnemyTracker instance dibuat.");
        }
        else
        {
            Debug.LogWarning("⚠️ EnemyTracker ganda ditemukan, akan dihancurkan.");
            Destroy(gameObject);
        }
    }

    public void RegisterEnemy()
    {
        totalEnemies++;
        Debug.Log("🟡 Enemy ditambahkan. Total sekarang: " + totalEnemies);
    }

    public void EnemyDefeated()
    {
        totalEnemies--;
        if (totalEnemies < 0) totalEnemies = 0;
        Debug.Log("🔴 Enemy dikalahkan. Sisa: " + totalEnemies);
    }

    public bool AllEnemiesDefeated()
    {
        Debug.Log("🟢 Cek musuh: Semua musuh dikalahkan? " + (totalEnemies <= 0));
        return totalEnemies <= 0;
    }
}
