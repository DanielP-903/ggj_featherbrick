using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinCollector : MonoBehaviour
{
    public static int[] score = new int[4];
    // Start is called before the first frame update
    void Start()
    {
        //score[0] = player 1, score[1] = player 2 etc.
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
                        score[0]++; // Add to player 1 score
                        Debug.Log("P1 score updated to: " + score[0]);
                        Destroy(collision.gameObject);
                        break;
                    }
                case 2:
                    {
                        score[1]++; // Add to player 2 score
                        Debug.Log("P2 score updated to: " + score[1]);
                        Destroy(collision.gameObject);
                        break;
                    }
                case 3:
                    {
                        score[2]++; // Add to player 3 score
                        Debug.Log("P3 score updated to: " + score[2]);
                        Destroy(collision.gameObject);
                        break;
                    }
                case 4:
                    {
                        score[3]++; // Add to player 4 score
                        Debug.Log("P4 score updated to: " + score[3]);
                        Destroy(collision.gameObject);
                        break;
                    }
            }
        }
    }
}
