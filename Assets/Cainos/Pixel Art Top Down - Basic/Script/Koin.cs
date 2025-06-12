using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koin : MonoBehaviour
{
    public int coinValue = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playermovement player = collision.GetComponent<playermovement>();
        if(player != null)
        {
            player.addCoin(coinValue);
            Destroy(gameObject);
        }
    }
}
