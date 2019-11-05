using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed = 2;

    Vector2 movement_position;

    private CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

       

    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);

        movement = movement.normalized * speed * Time.deltaTime;

        this.GetComponent<Rigidbody>().MovePosition(new Vector3(transform.position.x + movement.x, 0, 0));
    }
}
