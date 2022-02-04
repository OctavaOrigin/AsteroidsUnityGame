using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject laserBeam;
    [SerializeField] float timeBetweenShots = 1f;
    public bool canFire;

    private delegate void FireDelegate();
    FireDelegate Fire;
    Coroutine currentRunning;

    GameObject laser;
    Vector3 direction;

    private void Awake()
    {
        LaserBeam.SetUpLaser();
    }

    private void Start()
    {
        Fire = FireProjectile;
    }
    private void Update()
    {
        CountDirection();
        if (canFire)
            LaserBeam.CountLaserReset();

        Fire();
    }

    private void FireProjectile()
    {
        if (canFire && (currentRunning == null))
        {
            currentRunning = StartCoroutine(SpawnBullet());
        }
        else if (!canFire && (currentRunning != null))
        {
            StopCoroutine(currentRunning);
            currentRunning = null;
        }        
    }

    IEnumerator SpawnBullet()
    {
        while (true)
        {
            GameObject bullet = Instantiate(projectile,transform.position,Quaternion.identity);
            bullet.GetComponent<ProjectileScript>().vector = direction; // give bullet a direcion where to go
            bullet.transform.eulerAngles = transform.eulerAngles; // turn bullet into correct direcion

            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    private void CountDirection()
    {
        float cosRaw = Mathf.Cos(transform.localEulerAngles.z * Mathf.Deg2Rad);
        float sinRaw = Mathf.Sin(transform.localEulerAngles.z * Mathf.Deg2Rad * -1);

        direction = new Vector3(sinRaw,cosRaw);
    }

    public void TryShootingBeam()
    {
        if (LaserBeam.isReadyToFire() && canFire)
        {
            canFire = false;
            if (currentRunning != null) StopCoroutine(currentRunning);
            currentRunning = StartCoroutine(SpawnLaser());   
            Fire = ShootLaserBeam;
        }
    }

    private void ShootLaserBeam()
    {
        laser.transform.position = transform.position;
        laser.transform.eulerAngles = transform.eulerAngles;
    }

    IEnumerator SpawnLaser()
    {

        laser = Instantiate(laserBeam,transform.position,transform.rotation);
        yield return new WaitForSeconds(LaserBeam.GetUsageDuration());
        Destroy(laser.gameObject);
        LaserBeam.UseLaser();
        Fire = FireProjectile;
        canFire = true;
        currentRunning = null;
    }


}
