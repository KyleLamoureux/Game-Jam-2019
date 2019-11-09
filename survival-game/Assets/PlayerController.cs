using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    Rigidbody2D rigidbody;
    public Text collectedText;
    public static int collectedAmount = 0;

    public static bool dead = false;
    
    private float nextSpawnTime = 0;
    public GameObject itemPrefab;
    private bool spawn;
    private float spawnDelay = 8;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldSpawn()){
            spawnItem();
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rigidbody.velocity = new Vector3(horizontal * speed, vertical * speed, 0);
        collectedText.text = "Items collected: " + collectedAmount;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Enemy"){
            PlayerController.dead = true;
            speed = 0;
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
