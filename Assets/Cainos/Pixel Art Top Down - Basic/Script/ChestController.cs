using UnityEngine;

public class ChestController : MonoBehaviour
{
    private bool isOpened = false;

    public Animator animator;
    public GameObject itemPrefab;
    public Transform dropPoint;

    private void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isOpened) return;

        if (other.CompareTag("Player"))
        {
            OpenChest();
        }
    }

    void OpenChest()
    {
        isOpened = true;

        if (animator != null)
            animator.SetTrigger("Open");

        // 🔊 Suara buka peti
        FindObjectOfType<PlayerAudioManager>()?.PlayChestOpenSound();

        if (itemPrefab != null && dropPoint != null)
        {
            Instantiate(itemPrefab, dropPoint.position, Quaternion.identity);
        }
    }
}
