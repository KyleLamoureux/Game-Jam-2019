using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState2{
    Sentry,
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

    GameObject player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(move);
        if(move > 0)
        {
            move--;
            if (move <= 0){
                speed = 0;
                currState = EnemyState2.Sentry;
            }
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
                break;
        }

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
