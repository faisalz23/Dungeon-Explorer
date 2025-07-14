using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemData; // Drag ScriptableObject ke sini

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && itemData != null)
        {
            itemData.Use(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
