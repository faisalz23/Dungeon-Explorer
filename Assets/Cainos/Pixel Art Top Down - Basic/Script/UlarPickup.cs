using UnityEngine;

public class UlarPickup : MonoBehaviour
{
    public string itemName = "Ular";

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
