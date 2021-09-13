using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float speed = 100f;
    public bool rotateAroundX = false;
    public bool rotateAroundY = false;
    public bool rotateAroundZ = true;
    public bool reverseX = false;
    public bool reverseY = false;
    public bool reverseZ = false;
    void Update()
    {
        if(rotateAroundX)
            gameObject.transform.Rotate(Time.deltaTime * speed, 0, 0);
        if (rotateAroundY)
            gameObject.transform.Rotate(0, Time.deltaTime * speed, 0);
        if (rotateAroundZ)
            gameObject.transform.Rotate(0, 0, Time.deltaTime * speed);
        if (reverseX)
            gameObject.transform.Rotate(-Time.deltaTime * speed, 0, 0);
        if (reverseY)
            gameObject.transform.Rotate(0, -Time.deltaTime * speed, 0);
        if (reverseZ)
            gameObject.transform.Rotate(0, 0, -Time.deltaTime * speed);
    }
}
