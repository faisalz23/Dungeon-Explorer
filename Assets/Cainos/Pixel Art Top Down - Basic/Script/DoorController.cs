using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject levelCompletePanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player triggered the door.");

            if (EnemyTracker.instance != null && EnemyTracker.instance.AllEnemiesDefeated())
            {
                Debug.Log("All enemies defeated. Showing level complete panel...");

                // 🔊 Mainkan suara level selesai
                FindObjectOfType<PlayerAudioManager>()?.PlayLevelCompleteSound();

                if (levelCompletePanel != null)
                    levelCompletePanel.SetActive(true);

                // Optional: disable player control
                collision.GetComponent<playermovement>().enabled = false;
            }
            else
            {
                Debug.Log("Belum semua musuh dikalahkan!");
            }
        }
    }
}
