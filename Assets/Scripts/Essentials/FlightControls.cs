using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlightControls : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private PlayerInput input;

    [Header("Acceleration Settings")]
    public float maxSpeed = 100f; // Max Speed
    public float accelerationMultiplier = 2f; // Speed Modifier

    [Header("Rotation Speed Settings")]
    public float rollSpeed = 2f; // Speed Modifier
    public float pitchSpeed = 2f;
    public float yawSpeed = 2f;
    [Range(0, 10f)]
    public float pitchYawSmoothing = 10.0f;

    [Header("Axis Inversion Settings")]
    public bool invertX = false;
    public bool invertY = false;

    public float height;

    float rotationX = 0;
    float rotationY = 0;
    float rotationZ = 0;

    Vector2 pitchYawCurrent;
    Vector2 rollCurrent;
    Vector3 cameraPos;
    Camera cam;

    private void Awake()
    {
        transform.rotation = new Quaternion(rotationX, rotationY, rotationZ, 0);
        playerRigidbody = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();

        cam = Camera.main;
        cameraPos = cam.transform.localPosition;
        cam.transform.SetParent(null);
        cam.transform.TransformPoint(cameraPos);
        

    }

    public void FixedUpdate()
    {
        InputAction move = input.actions["Move"]; // Store a reference to the Move input action
        InputAction vertical = input.actions["VerticalThrust"];
        InputAction roll = input.actions["Roll"];
        InputAction rotate = input.actions["PitchYaw"];

        Vector2 inputMovement = move.ReadValue<Vector2>(); // Set a Vector2 to store the Move input value
        Vector2 vertMovement = vertical.ReadValue<Vector2>(); // Set a Vector2 to store the VerticalThrust input value

        Vector3 thrust = new Vector3(inputMovement.x, vertMovement.y, inputMovement.y);
        Thrust(thrust);

        Roll(-roll.ReadValue<Vector2>());

        PitchYaw(rotate.ReadValue<Vector2>());

        Vector3 currentVelocity = playerRigidbody.velocity;
        if (currentVelocity.magnitude > maxSpeed)
        {
            playerRigidbody.velocity = currentVelocity.normalized * maxSpeed; // Normalise the velocity to turn controls from a "Square" to a "Circle"
            transform.InverseTransformDirection(currentVelocity);
            // MAKE A COOL UI WITH THIS ^
        }
    }

    private void LateUpdate()
    {
        // store cam.transform.localPosition

        transform.Rotate(((invertY ? 1.0f : -1.0f) * pitchYawCurrent.y * Time.deltaTime * pitchSpeed), 0, 0);
        transform.Rotate(0, (invertX ? -1.0f : 1.0f) * (pitchYawCurrent.x * Time.deltaTime * yawSpeed), 0);
        transform.Rotate(0, 0, rollCurrent.x * Time.deltaTime * rollSpeed);

        cam.transform.position = transform.position + transform.up * height;
        cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, transform.rotation, Time.deltaTime * 10.0f);      
    }

    public void Thrust(Vector3 thrust)
    {
        // Lateral Thrust
        playerRigidbody.AddForce(transform.right * thrust.x * accelerationMultiplier, ForceMode.VelocityChange);     
        // Vertical Thrust
        playerRigidbody.AddForce(transform.up * thrust.y * accelerationMultiplier, ForceMode.VelocityChange);          
        // Forward/Back Thrust
        playerRigidbody.AddForce(transform.forward * thrust.z * accelerationMultiplier, ForceMode.VelocityChange);               
    }

    public void Roll(Vector2 roll)
    {
        rollCurrent = Vector2.Lerp(rollCurrent, roll, Time.fixedDeltaTime * 10.0f);
    }

    public void RollRight()
    {
        transform.Rotate(0, 0, 1 * Time.fixedDeltaTime * rollSpeed);
    }

    public void PitchYaw(Vector2 pitchYaw)
    {
        pitchYawCurrent = Vector2.Lerp(pitchYawCurrent, pitchYaw, Time.fixedDeltaTime * pitchYawSmoothing);

        
    }
}
