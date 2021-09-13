using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomImpulse : MonoBehaviour
{
    public float maxSpeedXAxis = 100f;
    public float maxSpeedYAxis = 100f;
    public float maxSpeedZAxis = 100f;
    public float speed = 2f;

    private Rigidbody rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        RandomSpeed();
    }

    public void RandomSpeed()
    {
        Vector3 forceDirection = new Vector3((Random.Range(0, maxSpeedXAxis)), (Random.Range(0, maxSpeedXAxis)), (Random.Range(0, maxSpeedXAxis)));
        rb.AddForce(forceDirection * speed);
    }
}
