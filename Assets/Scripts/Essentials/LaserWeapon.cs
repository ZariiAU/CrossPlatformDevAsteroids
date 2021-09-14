using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaserWeapon : MonoBehaviour
{
    public Transform originObject;
    public InputActionReference Fire;
    public AudioSource audio;
    public AudioClip hitEffect;
    private LineRenderer laserLine;
    public Light flash;

    bool canShootLaser = true;
    bool canShoot = true;

    public int damage = 10;
    public int range = 100;
    public float laserVisibleDuration = 0.2f;

    public float xLineVarianceMax = 2f;
    public float shotDelay = 1f;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        laserLine = GetComponent<LineRenderer>();
    }

    public void OnFire(InputAction.CallbackContext value)
    {
        if (canShoot)
        {
            Shoot();
            audio.PlayOneShot(audio.clip);
            Debug.Log("OnFire" + transform.name);
        }
    }

    public void Shoot()
    {
        canShoot = false;
        StartCoroutine(ShootDelay());   
        
        RaycastHit hit;
        CharacterStats stats;

        Physics.Raycast(originObject.position, -originObject.up, out hit, range); // Raycast fire forward
        Debug.DrawRay(originObject.position, -originObject.up * range);

        
        if (hit.collider != null)
        {
            audio.PlayOneShot(hitEffect);
            if (hit.collider.CompareTag("Enemy"))
            {
                if (hit.collider.GetComponent<CharacterStats>() != null)
                {
                    stats = hit.collider.GetComponent<CharacterStats>();
                    if (stats.hasShield && stats.Shield > 0)
                    {
                        stats.Shield -= damage;
                        Debug.Log("Hit" + hit.collider.gameObject.name + "'s Shield for " + damage);
                    }
                    else if (!stats.hasShield || stats.Shield <= 0 && stats.hasHealth)
                    {
                        stats.Health -= damage;
                        Debug.Log("Hit" + hit.collider.gameObject.name + "'s Health for " + damage);
                    }
                }
            }
        }
        StartCoroutine(LaserEffect());
        canShootLaser = true;
        laserLine.enabled = true;
    }

    public IEnumerator LaserEffect()
    {
        while (canShootLaser) {

            Vector3 endPoint = Vector3.zero + Vector3.forward * range;

            laserLine.positionCount = 3;
            Vector3[] linePoints = new Vector3[3]
            {
            Vector3.zero,
            Vector3.Lerp(Vector3.zero, endPoint, 0.5f) + Vector3.right * Random.Range(-xLineVarianceMax, xLineVarianceMax),
            endPoint
            };
            laserLine.SetPositions(linePoints);

            laserLine.endWidth = 1;
            flash.range = 13;

            yield return new WaitForSeconds(0.1f);
            flash.range = 0;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(laserVisibleDuration);
        canShootLaser = false;
        laserLine.enabled = false;

        yield return new WaitForSeconds(shotDelay - laserVisibleDuration);
        canShoot = true;
    }
}
