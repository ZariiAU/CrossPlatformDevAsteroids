using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleController : MonoBehaviour
{
    public GameObject gameObject;
    [Header("Max Size")]
    public float maxSizeX;
    public float maxSizeY;
    public float maxSizeZ;
    [Header("Min Size")]
    public float minSizeX;
    public float minSizeY;
    public float minSizeZ;

    [ContextMenu("Randomise Size")]
    public void RandomiseSize()
    {
        gameObject.transform.localScale = new Vector3(Random.Range(minSizeX, maxSizeX)
            , Random.Range(minSizeY, maxSizeY), Random.Range(minSizeZ, maxSizeZ));
    }

    void Start()
    {
        RandomiseSize();
    }
}
