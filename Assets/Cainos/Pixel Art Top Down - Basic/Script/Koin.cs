using UnityEngine;

public class Koin : MonoBehaviour
{
    public int coinValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playermovement player = collision.GetComponent<playermovement>();
        if (player != null)
        {
            player.addCoin(coinValue);

            // 🔊 Mainkan suara pickup coin
            FindObjectOfType<PlayerAudioManager>()?.PlayPickupSound();

            Destroy(gameObject);
        }
    }
}
