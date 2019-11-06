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
    public float ThrowForce = 2000.0f;
    //Reference to the picked up piece of trash
    public GameObject Trash;
    //LayerMask to detect trash for pickup
    public LayerMask trashMask;
    //Tracks the button state of the left trigger
    bool LeftTriggerDown = false;
    //Layer Mask to detect only other players
    public LayerMask PlayerMask;
    //How frequently the impulse can be triggered
    public float ImpulseAttackSpeed = 0.5f;
    //Cooldown timer for the impulse
    float ImpulseCooldown = 0.0f;
    //Stores the previous trigger state of the left trigger
    float PreviousTriggerState = 0.0f;

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

        if(Input.GetButtonDown("Player" + PlayerID + "RB") && ImpulseCooldown <= 0)
        {
            ActivateImpulse();
        }

        if (Input.GetAxisRaw("Player" + PlayerID + "LT") != 0)
        {
            LeftTriggerDown = true;
        }
        else if (Input.GetAxisRaw("Player" + PlayerID + "LT") == 0)
        {
            LeftTriggerDown = false;
        }


        //If the right trigger is pressed throw the trash
        if (Input.GetAxisRaw("Player" + PlayerID + "RT") != 0 && Trash != null)
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
        //Subtract delta time from the impulse cooldown
        ImpulseCooldown -= Time.deltaTime;

        //Recieve all inputs
        HandleInput();

        //Handle the picking up/holding of the trash

        if (LeftTriggerDown && PreviousTriggerState != 0 && Trash != null)
        {
            //If the Left Trigger is held down move the trash object above the player
            Trash.transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z), Trash.transform.rotation);
        }
        else if(LeftTriggerDown && PreviousTriggerState == 0)
        {
            //If the left trigger is pressed after being in the released state pickup the trash
            PickupTrash();
        }
        else
        {
            //If the left trigger is released, remove the reference to the trash object and allow it to fall in place
            Trash = null;
        }
      
        //Player movement along the x-axis
        GetComponent<Rigidbody>().AddForce(MovementDirection * MovementSpeed, ForceMode.VelocityChange);

        //Store the current state of the left trigger for use in the next frame
        PreviousTriggerState = Input.GetAxisRaw("Player" + PlayerID + "LT");
    }

    //This function will apply an upward force to the player if they are not midair when called
    void Jump()
    {
        if(IsGrounded)
        {
            GetComponent<Rigidbody>().AddForce(JumpForce * Vector3.up, ForceMode.Impulse);
        }
    }
       
    //This function will cheack a nearby radius around the player for trash and pick it up when called
    void PickupTrash()
    {
        //Get an array of all the nearby colliders, whose game objects are on the trash layer
        Collider[] trashProximity = Physics.OverlapSphere(transform.position, 1, trashMask);

        //Check that a piece of trash is found
        if (trashProximity.Length > 0)
        {
            //Store a local reference to the trash game object
            Trash = trashProximity[0].gameObject;
            //Move the piece of trash to be above the players head
            Trash.transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z), Trash.transform.rotation);
        }
        else
        {
            //Set the local reference to null if no trash is found
            Trash = null;
        }
    }

    //This function will apply the throw force to the trash object when called
    void ThrowTrash()
    {
        LeftTriggerDown = false;
        //Normalise the aim direction and invert the y value 
        AimDirection.Normalize();
        AimDirection.y *= -1;
        //Apply the force
        Trash.GetComponent<Rigidbody>().AddForce(AimDirection * ThrowForce);       
    }

    //This function will activate the players impulse when called
    void ActivateImpulse()
    {
        ImpulseCooldown = 1.0f / ImpulseAttackSpeed;

        Collider[] playerProximity = Physics.OverlapSphere(transform.position, 3, PlayerMask);


        if (playerProximity.Length > 0)
        {
            Debug.Log("hello");
            for (int i = 0; i < playerProximity.Length; i++)
            {
                if (playerProximity[i].gameObject != this.gameObject)
                {
                    Vector2 direction = playerProximity[i].gameObject.transform.position - transform.position;
                    direction.Normalize();
                    playerProximity[i].gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(direction.x * 10, direction.y, 0) * 100);
                }
            }
        }
    }

    //The following functions detect if the player is colliding with the ground
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
