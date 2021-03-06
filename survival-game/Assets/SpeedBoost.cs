﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
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
            player.speed = 10;
            Destroy(gameObject);
            PlayerController.collectedAmount = PlayerController.collectedAmount + 1;
            player.speedTime = (int) (3 * (1/Time.deltaTime));
        }
    }
}
