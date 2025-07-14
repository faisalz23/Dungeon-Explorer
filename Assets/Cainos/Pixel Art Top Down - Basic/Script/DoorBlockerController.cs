using UnityEngine;

public class DoorBlockerController : MonoBehaviour
{
    private Collider2D doorCollider;

    void Start()
    {
        doorCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (EnemyTracker.instance != null && EnemyTracker.instance.AllEnemiesDefeated())
        {
            if (doorCollider != null && doorCollider.enabled)
            {
                doorCollider.enabled = false;
                Debug.Log("Musuh sudah dikalahkan. Blocker dinonaktifkan.");
            }
        }
    }
}
