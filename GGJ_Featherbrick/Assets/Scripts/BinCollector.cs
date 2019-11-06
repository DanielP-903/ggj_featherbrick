﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinCollector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Trash")
        {
            switch(collision.gameObject.GetComponent<Ownership>().ownerID)
            {
                case 1:
                    {
                         GLOBAL_.player1Score++; // Add to player 1 score
                        Debug.Log("P1 score updated to: " + GLOBAL_.player1Score);
                        Destroy(collision.gameObject);
                        break;
                    }
                case 2:
                    {
                        GLOBAL_.player2Score++; // Add to player 2 score
                        Debug.Log("P2 score updated to: " + GLOBAL_.player2Score);
                        Destroy(collision.gameObject);
                        break;
                    }
                case 3:
                    {
                        GLOBAL_.player3Score++; // Add to player 3 score
                        Debug.Log("P3 score updated to: " + GLOBAL_.player3Score);
                        Destroy(collision.gameObject);
                        break;
                    }
                case 4:
                    {
                        GLOBAL_.player4Score++; // Add to player 4 score
                        Debug.Log("P4 score updated to: " + GLOBAL_.player4Score);
                        Destroy(collision.gameObject);
                        break;
                    }
            }
        }
    }
}
