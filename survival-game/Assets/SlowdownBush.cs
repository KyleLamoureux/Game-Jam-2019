using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowdownBush : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerController player = collider.GetComponent<PlayerController>();
        if (player != null)
        {
            player.speed = 2;
            PlayerController.collectedAmount = PlayerController.collectedAmount + 2;
            Destroy(gameObject);
            player.speedTime = (int)(3 * (1 / Time.deltaTime));
        }
    }

}
