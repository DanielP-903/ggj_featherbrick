using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //player ID to track which player is controlling this character
    public int PlayerID = 1;

    //Speed that the player moves at
    public float MovementSpeed = 2.0f;
    //The force applied to the player jump
    public float JumpForce = 5.0f;
    //Check for if the player is touching the ground
    public bool IsGrounded = false;
    //Movement direction indicated by the left analogue stick
    Vector3 MovementDirection = new Vector2();
    //Aim direction indicated by the right analogue stick
    Vector3 AimDirection = new Vector2();
    //The force applied to the trash when thrown
    public float ThrowForce = 10.0f;
    //Reference to the picked up piece of trash
    public GameObject Trash;
    //LayerMask to detect trash for pickup
    public LayerMask trashMask;
    //Detects whether the player is currently holding the traash
    bool HoldingTrash = false;
    //Tracks the button state of the left trigger
    bool LeftTriggerDown = false;

    float previousTriggerState = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void HandleInput()
    {

        //Set the movement direction along the x-axis using the left analogue stick 
        MovementDirection = new Vector3(Input.GetAxisRaw("Player" + PlayerID + "LH"), 0.0f, 0.0f);
        //Set the aim direction using the right analogue stick
        AimDirection = new Vector3(Input.GetAxisRaw("Player" + PlayerID + "RH"), Input.GetAxisRaw("Player" + PlayerID + "RV"), 0.0f);


        if (Input.GetAxisRaw("Player" + PlayerID + "LT") != 0)
        {
            LeftTriggerDown = true;
        }
        else if (Input.GetAxisRaw("Player" + PlayerID + "LT") == 0)
        {
            LeftTriggerDown = false;
        }



        ////If the left trigger is pressed pickup the trash
        //if (Input.GetAxisRaw("Player" + PlayerID + "LT") != 0)
        //{
        //    PickupTrash();
        //}

        //If the right trigger is pressed throw the trash
        if (Input.GetAxisRaw("Player" + PlayerID + "RT") != 0 && HoldingTrash)
        {
            ThrowTrash();
        }

        //If the A button is pressed then jump
        if (Input.GetAxisRaw("Player" + PlayerID + "A") != 0)
        {
            Jump();
        }
    }


    // Update is called once per frame
    void Update()
    {
        HandleInput();

        

        if (LeftTriggerDown && !HoldingTrash)
        {
            PickupTrash();
        }
        else if(!LeftTriggerDown && HoldingTrash)
        {
            HoldingTrash = false;
        }


        if(HoldingTrash && !thrown)
        {
            Trash.transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z), Trash.transform.rotation);
        }

        GetComponent<Rigidbody>().AddForce(MovementDirection * MovementSpeed, ForceMode.VelocityChange);
        previousTriggerState = Input.GetAxisRaw("Player" + PlayerID + "LT");
    }

    void Jump()
    {
        if(IsGrounded)
        {
            GetComponent<Rigidbody>().AddForce(JumpForce * Vector3.up, ForceMode.Impulse);
        }
    }
       
    void PickupTrash()
    {
        Collider[] trashProximity = Physics.OverlapSphere(transform.position, 1, trashMask);

        if (trashProximity.Length > 0)
        {
            Trash = trashProximity[0].gameObject;
            HoldingTrash = true;
        }
        else
        {
            HoldingTrash = false;
            Trash = null;
        }
    }

    bool thrown = false;
    void ThrowTrash()
    {
        LeftTriggerDown = false;
        HoldingTrash = false;

        Trash.GetComponent<Rigidbody>().AddForce(AimDirection * 50.0f);
       
    }

    void ActivateShield()
    {

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Building")
        {
            IsGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Building")
        {
            IsGrounded = false;
        }
    }
}
