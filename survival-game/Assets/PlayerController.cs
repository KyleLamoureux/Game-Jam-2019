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
    public static bool activate = false;
    public int speedTime;

    public static bool dead = false;
    public static bool spawnOnce = false;

    private float nextSpeedBoostTime = 0;
    private float nextCoinTime = 0;
    private float nextSlowChest = 0;

    public GameObject itemPrefab;
    public GameObject speedPrefab;
    public GameObject chestPrefab;
    public GameObject enemyPrefab;

    private bool spawn;
    private float coinDelay = 1.5f;
    private float speedDelay = 15;
    private float chestDelay = 15;
    

    // Start is called before the first frame update
    void Start()
    {
        //GameObject.FindGameObjectWithTag("Music").GetComponent<MusicClass>().PlayMusic();
        speedTime = 0;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(dead){
            speed=0;
            ControlScript.timer = 0.0f;
        }
        if(shouldSpawnCoin()){
            spawnCoin();
        }

        if (speedTime > 0)
        {
            speedTime--;
            if (speedTime == 0)
                speed = 5;
        }

        if (ControlScript.timer <= 0.0f)
        {
            PlayerController.dead = true;
            ControlScript.timer = 0.0f;
            speed = 0;
        }

        if (shouldSpawnSpeedBoost())
        {
            spawnSpeedBoost();
        }

        if(shouldSpawnSlowChest())
        {
            spawnSlowChest();
        }

        if(spawnOnce){
            spawnEnemy();
            spawnOnce = false;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rigidbody.velocity = new Vector3(horizontal * speed, vertical * speed, 0);
        collectedText.text = "Score: " + collectedAmount;   
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Enemy"){
            PlayerController.dead = true;
            ControlScript.timer = 0.0f;
            speed = 0;
        }

    }

    private bool shouldSpawnSpeedBoost()
    {
        return Time.time >= nextSpeedBoostTime;
    }

    private bool shouldSpawnSlowChest()
    {
        return Time.time >= nextSlowChest;
    }

    private bool shouldSpawnCoin()
    {
        return Time.time >= nextCoinTime;
    }

    private void spawnSlowChest()
    {
        nextSlowChest = Time.time + chestDelay;
        if (chestPrefab != null)
            Instantiate(chestPrefab, new Vector3(Random.Range(-10f, 12f), Random.Range(-7.5f, 10f), 0), Quaternion.identity);
    }

    private void spawnSpeedBoost()
    {
        nextSpeedBoostTime = Time.time + speedDelay;
        if (speedPrefab != null)
            Instantiate(speedPrefab, new Vector3(Random.Range(-10f, 12f), Random.Range(-7.5f, 10f), 0), Quaternion.identity);
    }

    private void spawnCoin(){
        nextCoinTime = Time.time + coinDelay;
        if(itemPrefab!=null)
            Instantiate(itemPrefab, new Vector3(Random.Range(-10f, 12f), Random.Range(-7.5f, 10f), 0), Quaternion.identity);
    }

    private void spawnEnemy(){
        if(enemyPrefab!=null)
            Instantiate(enemyPrefab, new Vector3(Random.Range(-9.4f, -5.0f), 6.6f , 0), Quaternion.identity);
    }
}
