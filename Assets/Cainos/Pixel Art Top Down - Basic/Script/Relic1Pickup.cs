using UnityEngine;

public class Relic1Pickup : MonoBehaviour
{
    public string itemName = "Relic";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player mengambil: " + itemName);
            // Bisa tambahkan logika menyimpan ke inventori jika ada
            Destroy(gameObject);
        }
    }
}
