using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ownership : MonoBehaviour
{
    public int ownerID = 0;
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
        if (collision.gameObject.tag == "Player")
        {
            ownerID = collision.gameObject.GetComponent<CharacterController>().PlayerID;
            Debug.Log("I am owned by Player " + collision.gameObject.GetComponent<CharacterController>().PlayerID);
        }
    }
}
