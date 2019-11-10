using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState2{
    Sentry,
    Return,
    Follow
};
public class DarkPurpleAI : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemyState2 currState = EnemyState2.Sentry;

    public float capSpeed;
    private float speed;
    public float range;
    public float moveTime;
    private float move;

    public float homeX;
    public float homeY;
    private Vector2 homeVector;

    GameObject player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        homeVector = new Vector3(homeX, homeY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currState);

        if(currState == EnemyState2.Return && isHome()){
            currState = EnemyState2.Sentry;
            //speed = 0;
        }
        switch(currState){
            case(EnemyState2.Sentry):
                sentry();
                break;
            case(EnemyState2.Follow):
                if(!(capSpeed == null)){
                    speed = capSpeed;
                } else{
                    speed = 6;
                }
                follow();
                move--;
                if (move <= 0){
                    currState = EnemyState2.Return;
                }
                break;
            case(EnemyState2.Return):
                returnHome();
                break;
        }

    }

    public bool isHome(){
        Debug.Log(Vector3.Distance(transform.position, homeVector));
        return Vector3.Distance(transform.position, homeVector) <= .2f;
    }

    public void returnHome(){
        transform.position = Vector2.MoveTowards(transform.position, homeVector, speed * Time.deltaTime);
    }

    public void sentry(){
        if(isPlayerInRange(range)){
            currState = EnemyState2.Follow;
            speed=0;
            move=moveTime;
        }
    }
    private bool isPlayerInRange(float range){
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    public void follow(){
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }


}
