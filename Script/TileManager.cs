using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject[] map;
    private Transform playerTransform;
    private float spawnY = -27f;
    private readonly float tileLength = 13.76f;
    //private float safeZone = 15.0f;
    private readonly int amnTileOnScreen = 7;
    private int lastPrefabIndex = 0;

    private List<GameObject> activeTiles;
    // Start is called before the first frame update
    void Start()
    {
        
        // Log a message when a tile is spawned
        // string logMessage = "Tilemanager loaded" + spawnY;
        // LogToBrowserConsole(logMessage);

        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < amnTileOnScreen; i++)
        {
            SpawnTile();
        }
        for (int i = 0; i < map.Length; i++)
        {
            map[i].SetActive(false);
        }
    }

    // Update is called once per frame
    /*void Update()
    {
        if (playerTransform.position.y - safeZone > (spawnY - amnTileOnScreen * tileLength))
        {
            spawnTile();
            deleteTile();
        }
    }*/

    private void SpawnTile()
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

    // private void LogToBrowserConsole(string message)
    // {
    //     // Prepare the JavaScript code to log the message
    //     string jsCode = "console.log('" + message + "');";

    //     // Call the JavaScript code using Application.ExternalEval
    //     Application.ExternalEval(jsCode);
    // }
}
