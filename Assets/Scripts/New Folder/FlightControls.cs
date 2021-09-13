using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlightControls : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private PlayerInput input;

    public float maxSpeed = 100f; // Max Speed
    public float acceleration = 2f; // Speed Modifier
    public float rollSpeed = 2f; // Speed Modifier
    float rotationX = 0;
    float rotationY = 0;
    float rotationZ = 0;

    private void Awake()
    {
        transform.rotation = new Quaternion(rotationX, rotationY, rotationZ, 0);
        playerRigidbody = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
    }

    public void FixedUpdate()
    {
        InputAction move = input.actions["Move"]; // Store a reference to the Move input action
        InputAction vertical = input.actions["VerticalThrust"];
        InputAction roll = input.actions["Roll"];
        InputAction rotate = input.actions["PitchYaw"];

        Vector2 inputMovement = move.ReadValue<Vector2>(); // Set a Vector2 to store the Move input value
        if (inputMovement.y > 0) // Forward
        {
            ThrustForward();
        }
        if (inputMovement.y < 0) // Backward
        {
            ThrustBackward();
        }
        if (inputMovement.x > 0) // Right
        {
            ThrustRight();
        }
        if (inputMovement.x < 0) // Left
        {
            ThrustLeft();
        }

        Vector2 vertMovement = vertical.ReadValue<Vector2>(); // Set a Vector2 to store the VerticalThrust input value
        if (vertMovement.y > 0) // Up
        {
            ThrustUp();
        }
        if (vertMovement.y < 0) // Down
        {
            ThrustDown();
        }

        Vector2 rollMovement = roll.ReadValue<Vector2>();
        if(rollMovement.x > 0)
        {
            RollLeft();
        }
        if (rollMovement.x < 0)
        {
            RollRight();
        }

        Vector2 pitchYaw = rotate.ReadValue<Vector2>();
        if (pitchYaw.y > 0)
        {
            PitchUp();
        }
        if (pitchYaw.y < 0)
        {
            PitchDown();
        }
        if (pitchYaw.x > 0)
        {
            YawRight();
        }
        if (pitchYaw.x < 0)
        {
            YawLeft();
        }

        Vector3 currentVelocity = playerRigidbody.velocity;
        if (currentVelocity.magnitude > maxSpeed)
        {
            playerRigidbody.velocity = currentVelocity.normalized * maxSpeed; // Normalise the velocity to turn controls from a "Square" to a "Circle"
            transform.InverseTransformDirection(currentVelocity);
            // MAKE A COOL UI WITH THIS ^
        }
    }
    // FORWARD MOVEMENT
    public void ThrustForward()
    {
        float forwardVelocity = Vector3.Dot(transform.forward, playerRigidbody.velocity);

        if (forwardVelocity < maxSpeed)
            playerRigidbody.AddForce(transform.forward * acceleration, ForceMode.VelocityChange);
    }

    public void ThrustBackward()
    {
        float forwardVelocity = Vector3.Dot(-transform.forward, playerRigidbody.velocity);
        if (forwardVelocity < maxSpeed)
            playerRigidbody.AddForce(-transform.forward * acceleration, ForceMode.VelocityChange);
    }

    // LATERAL MOVEMENT
    public void ThrustRight()
    {
        float rightVelocity = Vector3.Dot(transform.right, playerRigidbody.velocity);
        if (rightVelocity < maxSpeed)
            playerRigidbody.AddForce(transform.right * acceleration, ForceMode.VelocityChange);
    }

    public void ThrustLeft()
    {
        float rightVelocity = Vector3.Dot(-transform.right, playerRigidbody.velocity);
        if (rightVelocity < maxSpeed)
            playerRigidbody.AddForce(-transform.right * acceleration, ForceMode.VelocityChange);
    }

    // VERTICAL MOVEMENT
    public void ThrustUp()
    {
        float upVelocity = Vector3.Dot(transform.up, playerRigidbody.velocity);
        if (upVelocity < maxSpeed)
            playerRigidbody.AddForce(transform.up * acceleration, ForceMode.VelocityChange);
    }

    public void ThrustDown()
    {
        float upVelocity = Vector3.Dot(-transform.up, playerRigidbody.velocity);
        if (upVelocity < maxSpeed)
            playerRigidbody.AddForce(-transform.up * acceleration, ForceMode.VelocityChange);
    }

    public void RollLeft()
    {
        transform.Rotate(0, 0, -(1 * Time.deltaTime * rollSpeed));
    }

    public void RollRight()
    {
        transform.Rotate(0, 0, 1 * Time.deltaTime * rollSpeed);
    }

    public void PitchUp()
    {
        transform.Rotate((1 * Time.deltaTime * rollSpeed), 0, 0);
    }

    public void PitchDown()
    {
        transform.Rotate(-(1 * Time.deltaTime * rollSpeed), 0, 0);
    }

    public void YawRight()
    {
        transform.Rotate(0, (1 * Time.deltaTime * rollSpeed), 0);
    }

    public void YawLeft()
    {
        transform.Rotate(0, -(1 * Time.deltaTime * rollSpeed), 0);
    }
}
