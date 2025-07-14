using UnityEngine;

public class PedangPickup : MonoBehaviour
{
    public string itemName = "Pedang";

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
