using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    [Header("UI HP Bar")]
    public GameObject hpBarPrefab;  // Prefab berisi HPBarController
    private HPBarController hpBar;
    private Transform hpBarTransform;

    private void Start()
    {
        currentHealth = maxHealth;

        // Cek apakah prefab di-assign
        if (hpBarPrefab != null)
        {
            GameObject canvas = GameObject.Find("Canvas");
            if (canvas != null)
            {
                GameObject hpGO = Instantiate(
                    hpBarPrefab,
                    transform.position + Vector3.up * 1.5f,
                    Quaternion.identity,
                    canvas.transform
                );

                hpBar = hpGO.GetComponent<HPBarController>();
                hpBarTransform = hpGO.transform;

                if (hpBar != null)
                    hpBar.SetHP(currentHealth, maxHealth);
                else
                    Debug.LogError("HPBarController tidak ditemukan di prefab HP bar!");
            }
            else
            {
                Debug.LogError("Canvas tidak ditemukan di scene! Pastikan ada GameObject bernama 'Canvas'.");
            }
        }
        else
        {
            Debug.LogError("hpBarPrefab belum di-assign di Inspector!");
        }
    }

    private void Update()
    {
        // Posisi HP bar mengikuti musuh
        if (hpBarTransform != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1.5f);
            hpBarTransform.position = screenPos;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10); // Tekan T saat game jalan untuk mengurangi HP musuh
        }

    // Update posisi HP bar biar selalu di atas musuh
        if (hpBarTransform != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1.5f);
            hpBarTransform.position = screenPos;
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Musuh menerima damage: " + damage);

        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth); // Hindari < 0

        if (hpBar != null)
        hpBar.SetHP(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            if (hpBarTransform != null)
                Destroy(hpBarTransform.gameObject); // Hapus bar HP

            Destroy(gameObject); // Hapus musuh
        }
    }
}
