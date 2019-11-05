using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown == true)
        {
            this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 2.0f);
        }
    }
}
