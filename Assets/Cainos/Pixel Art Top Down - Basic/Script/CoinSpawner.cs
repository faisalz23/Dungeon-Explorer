using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public Tilemap groundTilemap;     // Tilemap tanah
    public Tilemap obstacleTilemap;   // Tilemap keras
    public int coinCount = 10;

    void Start()
    {
        SpawnCoins();
    }

    void SpawnCoins()
    {
        List<Vector3> validPositions = new List<Vector3>();

        // Ambil bounds dari tilemap tanah
        BoundsInt bounds = groundTilemap.cellBounds;

        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            // Pastikan tile tanah ada
            if (groundTilemap.HasTile(pos))
            {
                // Pastikan tile obstacle tidak ada di posisi itu
                if (!obstacleTilemap.HasTile(pos))
                {
                    // Ubah posisi cell ke world
                    Vector3 worldPos = groundTilemap.CellToWorld(pos) + new Vector3(0.5f, 0.5f, 0); // tengah-tengah tile
                    validPositions.Add(worldPos);
                }
            }
        }

        // Acak dari daftar posisi valid
        for (int i = 0; i < coinCount && validPositions.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, validPositions.Count);
            Vector3 spawnPos = validPositions[randomIndex];
            Instantiate(coinPrefab, spawnPos, Quaternion.identity);
            validPositions.RemoveAt(randomIndex); // biar gak dobel
        }
    }
}
