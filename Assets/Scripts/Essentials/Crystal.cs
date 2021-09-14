using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    Renderer renderer;
    public Collider crystalCollider;
    public int value;
    private AudioSource audioSource;
    public AudioClip pickUpSoundEffect;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = pickUpSoundEffect;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            other.GetComponentInChildren<CrystalTracker>().CrystalCount += value;

            audioSource.PlayOneShot(pickUpSoundEffect);

            // REPLACE WITH POOLING!!!!!
            renderer.enabled = false;
            Destroy(gameObject, 2);//
            //////////////////////
        }
    }
}
