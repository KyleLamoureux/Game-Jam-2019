using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState{
    Wander,
    Follow
};

public class EnemyController : MonoBehaviour
{

    GameObject player;
    public EnemyState currState = EnemyState.Wander;

    public float range;
    public float speed;
    private bool chooseDir = false;
    private Vector3 randomDir;

    // Start is called before the first frame update
    void Start()
    {
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

        if(isPlayerInRange(range)){
            currState = EnemyState.Follow;
        } else{
            currState = EnemyState.Wander;
        }

        if(Vector2.Distance(transform.position, player.transform.position) <= 0.5f){
            PlayerController.dead = true;
        }

        if(!checkInBounds(transform)){
            changeDirection();
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
        randomDir = new Vector3(0, 0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(randomDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
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
}