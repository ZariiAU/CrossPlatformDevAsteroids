using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class CharacterStats : MonoBehaviour
{
    public UnityEvent HealthValueChanged;
    public UnityEvent ShieldValueChanged;

    public ParticleSystem explosionEffect;
    public AudioClip explosionSFX;
    public AudioSource audio;
    public GameObject itemDeathDrop;

    bool isPlayer;
    bool isAlive = true;
    
    [Header("Element Toggle")]
    public bool hasShield = false;
    public bool hasHealth = true;
    public bool dropOnDeath = false;

    [Header("Health Stats")]
    [SerializeField]
    private float m_health;

    [field: SerializeField]
    public float MaxHealth
    { get; set; }

    [Header("Shield Stats")]
    [SerializeField]
    private float m_shield;

    [field: SerializeField]
    public float MaxShield
    { get; set; }

    public float Health
    { 
        get 
        {
            return m_health;
        } 
        set 
        {
            m_health = value;
            HealthValueChanged.Invoke();
        } 
    }

    public float Shield
    {
        get
        {
            return m_shield;
        }
        set
        {
            m_shield = value;
            ShieldValueChanged.Invoke();
        }
    }

    [ContextMenu("TestDamage")]
    public void Damage()
    {
        Health -= 10;
    }
    private void Awake()
    {
        if (CompareTag("Player"))
        {
            isPlayer = true;
        }

        audio = GetComponent<AudioSource>();
    }

    public void FixedUpdate()
    {
        if (m_health <= 0 && !isPlayer)
        {
            if (isAlive)
            {
                StartCoroutine(Explode());
            }
            isAlive = false;
        } 
    }
    public IEnumerator Explode()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        mesh.enabled = false;

        explosionEffect.Play();
        audio.PlayOneShot(explosionSFX);

        if (dropOnDeath)
            Instantiate(itemDeathDrop, gameObject.transform.position, gameObject.transform.rotation);

        yield return new WaitForSeconds(2);

        Destroy(gameObject);
    }
}
