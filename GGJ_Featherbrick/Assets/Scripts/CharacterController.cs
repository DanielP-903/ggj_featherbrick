using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //player ID to track which player is controlling this character
    public int playerID = 0;

    //Speed that the player moves at
    public float movementSpeed = 2.0f;
    //The force applied to the player jump
    public float jumpForce = 5.0f;
    //Check for if the player is touching the ground
    public bool isGrounded = false;
    //Movement direction indicated by the left analogue stick
    Vector2 MovementDirection = new Vector2();
    //Aim direction indicated by the right analogue stick
    Vector2 AimDirection = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void HandleInput()
    {

        //Set the movement direction along the x-axis using the left analogue stick 
        MovementDirection = new Vector2(Input.GetAxisRaw("Player" + playerID + "LH"), 0.0f);
        //Set the aim direction using the right analogue stick
        AimDirection = new Vector2(Input.GetAxisRaw("Player" + playerID + "RH"), Input.GetAxisRaw("Player" + playerID + "RV"));
        
        //If the left trigger is pressed pickup the trash
        if (Input.GetAxisRaw("Player" + playerID + "LT") != 0)
        {
            Debug.Log("Player " + playerID + " pressed Left Trigger");
            PickupTrash();
        }

        //If the right trigger is pressed throw the trash
        if (Input.GetAxisRaw("Player" + playerID + "RT") != 0)
        {
            Debug.Log("Player " + playerID + " pressed Right Trigger");
            ThrowTrash();
        }

        //If the A button is pressed then jump
        if (Input.GetAxisRaw("Player" + playerID + "A") != 0)
        {
            Debug.Log("Player " + playerID + " pressed A");
            Jump();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Jump()
    {

    }

    void PickupTrash()
    {

    }

    void ThrowTrash()
    {

    }

    void ActivateShield()
    {

    }
}
