using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    private float nextSpawnTime = 0;
    private GameObject itemPrefab;
    private bool spawn;
    private float spawnDelay = 8;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {   if(shouldSpawn()){
            spawnItem();
        }
    }

    private bool shouldSpawn(){
        return Time.time >= nextSpawnTime;
    }

    private void spawnItem(){
        nextSpawnTime = Time.time + spawnDelay;
        Instantiate(itemPrefab, new Vector3(Random.Range(0, 20), Random.Range(0, 20), 0), Quaternion.identity);
    }
}
