using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState{
    Wander,
    Follow
};

public class EnemyController : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject player;
    public EnemyState currState = EnemyState.Wander;
    public float prevX;
    public float prevY;
    public bool movingX;
    public bool movingY;

    public float range;
    public float speed;
    private bool chooseDir = false;
    private Vector3 randomDir;
    private int gtfoTimer;

    // Start is called before the first frame update
    void Start()
    {
        gtfoTimer = 0;
        prevX = gameObject.transform.position.x;
        prevY = gameObject.transform.position.y;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.dead == true)
        {
            speed = 0;
        }
        switch(currState){
            case(EnemyState.Wander):
                wander();
                break;
            case(EnemyState.Follow):
                follow();
                break;
        }

        float newX = gameObject.transform.position.x;
        float newY = gameObject.transform.position.y;

        rb.velocity = new Vector3((prevX - newX) * speed, (prevY - newY) * speed, 0);

        Debug.Log(gtfoTimer);
        if (gtfoTimer == 0)
        {
            switch (currState)
            {
                case (EnemyState.Wander):
                    wander();
                    break;
                case (EnemyState.Follow):
                    follow();
                    break;
            }

            if (isPlayerInRange(range))
            {
                currState = EnemyState.Follow;
            }
            else
            {
                currState = EnemyState.Wander;
            }

            if (Vector2.Distance(transform.position, player.transform.position) <= 0.5f)
            {
                PlayerController.dead = true;
            }

            if (!checkInBounds(transform))
            {
                changeDirection();
            }
            prevX = newX;
            prevY = newY;
        }
        else if (isPlayerInRange(range)) {
            gtfoTimer = 0;
            currState = EnemyState.Follow;
            follow();
        }
        else
        {
            currState = EnemyState.Follow;
            follow();
            gtfoTimer--;
        }
    }

    public bool checkInBounds(Transform obj){
        if(obj.position.x < 12 && obj.position.x > -10 && obj.position.y < 10 && obj.position.y > -7.5){
            return true;
        }
        return false;
    }

    private bool isPlayerInRange(float range){
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection(){
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        changeDirection();
        chooseDir = false;
    }

    private void changeDirection(){
        //Debug.Log("Hit something with a velocity: " + GetComponent<Rigidbody2D>().velocity);

       // rb.velocity = new Vector3(-rb.velocity.x, -rb.velocity.y, 0);


        /*
        randomDir = new Vector3(0, 0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(randomDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        */
        
    }

    void wander(){
        if(!chooseDir){
            StartCoroutine(ChooseDirection());
        }
        transform.position += -transform.right * speed * Time.deltaTime;
        if(isPlayerInRange(range)){
            currState = EnemyState.Follow;
        }
    }

    void follow(){
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wall")
            gtfoTimer = (int) (5 * (1 / Time.deltaTime));
    }
}