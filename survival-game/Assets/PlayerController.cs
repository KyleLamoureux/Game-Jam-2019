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
    public int speedTime;

    public static bool dead = false;

    private float nextSpeedBoostTime = 0;
    private float nextCoinTime = 0;
    public GameObject itemPrefab;
    private bool spawn;
    private float coinDelay = 8;
    private float speedDelay = 15;

    // Start is called before the first frame update
    void Start()
    {
        speedTime = 0;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldSpawnCoin()){
            spawnCoin();
        }
        if(shouldSpawnSpeedBoost())
        {
            spawnSpeedBoost();
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rigidbody.velocity = new Vector3(horizontal * speed, vertical * speed, 0);
        collectedText.text = "Items collected: " + collectedAmount;

        if(speedTime > 0)
        {
            speedTime--;
            if (speedTime == 0)
                speed = 5;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Enemy"){
            PlayerController.dead = true;
            speed = 0;
        }
    }

    private bool shouldSpawnSpeedBoost()
    {
        return Time.time >= nextSpeedBoostTime;
    }

    private void spawnSpeedBoost()
    {
        nextSpeedBoostTime = Time.time + speedDelay;
        if (itemPrefab != null)
            Instantiate(itemPrefab, new Vector3(Random.Range(-10f, 12f), Random.Range(-7.5f, 10f), 0), Quaternion.identity);
    }

    private bool shouldSpawnCoin(){
        return Time.time >= nextCoinTime;
    }

    private void spawnCoin(){
        nextCoinTime = Time.time + coinDelay;
        if(itemPrefab!=null)
            Instantiate(itemPrefab, new Vector3(Random.Range(-10f, 12f), Random.Range(-7.5f, 10f), 0), Quaternion.identity);
    }
}
