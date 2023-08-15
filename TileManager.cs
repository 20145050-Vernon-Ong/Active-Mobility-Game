using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    private Transform playerTransform;
    private float spawnY = -6.0f;
    private float tileLength = 12.0f;
    private float safeZone = 15.0f;
    private int amnTileOnScreen = 7;
    private int lastPrefabIndex = 0;

    private List<GameObject> activeTiles;

    // Start is called before the first frame update
    void Awake()
    {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i = 0; i < amnTileOnScreen; i++)
        {
            spawnTile();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.y - safeZone > (spawnY - amnTileOnScreen * tileLength))
        {
            spawnTile();
            deleteTile();
        }
    }

    private void spawnTile(int prefabIndex = -1)
    {
        GameObject go;
        go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector2.up * spawnY;
        spawnY += tileLength;
        activeTiles.Add(go);
    }

    private void deleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
        if (tilePrefabs.Length <= 1)
            return 0;

        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;

    }
}