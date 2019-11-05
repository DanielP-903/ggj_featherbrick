using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed = 2;
    public bool is_grounded;
    Vector2 movement_position;
    public Sprite idle;
    //public Sprite walking;
    private CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
        Vector3 jumpforce = new Vector3();

        // movement = movement.normalized * speed * Time.deltaTime;

        if (Input.GetButtonDown("Jump") == true && is_grounded == true)
        {
            jumpforce = Vector3.up;
            //float jumping = 100.0f * Time.deltaTime;
            //this.GetComponent<Rigidbody>().MovePosition(new Vector3(transform.position.x, transform.position.y + jumping, 0));
            is_grounded = false;
        }
        else
        {
            jumpforce = Vector3.zero;
        }

        //this.GetComponent<Rigidbody>().MovePosition(new Vector3(transform.position.x + movement.x, transform.position.y, 0));
        this.GetComponent<Rigidbody>().AddForce(((movement * 0.75f)) + (jumpforce * 5.0f), ForceMode.Impulse);

        if (movement.x > 0)
        {
            this.GetComponent<Animator>().Play("TigerAnimation");
            this.GetComponent<SpriteRenderer>().flipX = true;
            this.GetComponent<Animator>().enabled = true;// = false;
        }
        else if (movement.x < 0)
        {
            this.GetComponent<Animator>().Play("TigerAnimation");
            this.GetComponent<SpriteRenderer>().flipX = false;
            this.GetComponent<Animator>().enabled = true;// = false;
        }
        else if (movement.x == 0)
        {
            this.GetComponent<SpriteRenderer>().sprite = idle;
            this.GetComponent<Animator>().enabled = false;// = false;
        }
    }

    private void FixedUpdate()
    {
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Building")
        {
            is_grounded = true;
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Building")
        {
            is_grounded = false;
        }
    }
}
