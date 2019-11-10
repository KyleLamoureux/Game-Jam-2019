using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState{
    Wander,
    Follow
};

public class EnemyController : MonoBehaviour
{
    public int moveTimer;
    public Vector3 toMoveTo;
    GameObject player;
    public EnemyState currState = EnemyState.Wander;

    public float range;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        moveTimer = 0;
        toMoveTo = new Vector3(0, 0, 0);
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

    void wander(){
        if (moveTimer == 0)
        {
            toMoveTo = new Vector3(Random.Range(-10f, 12f), Random.Range(-7.5f, 10f), 0);
            moveTimer = (int) (Random.Range(1, 5) * (1/Time.deltaTime));
        }
        else
            moveTimer--;
        transform.position = Vector2.MoveTowards(transform.position, toMoveTo, speed * Time.deltaTime);
        if(isPlayerInRange(range)){
            currState = EnemyState.Follow;
        }
    }

    void follow(){
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            toMoveTo = new Vector3(Random.Range(-10f, 12f), Random.Range(-7.5f, 10f), 0);
        }
    }
}