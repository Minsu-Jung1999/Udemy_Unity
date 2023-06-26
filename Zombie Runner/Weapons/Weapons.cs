using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{

    [SerializeField] Camera FPCamera;
    [SerializeField] float damage = 20f;
    [SerializeField] float range = 100f;
    [SerializeField] ParticleSystem muzzleFalsh;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] float timeBetweenShoot = 0.5f;
    [SerializeField] AmmoType ammoType;

    bool canShoot = true;

    private void OnEnable()
    {
        canShoot = true;    
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot == true)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMuzzleFlash();
            ProcessRayCast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }
        yield return new WaitForSeconds(timeBetweenShoot);
        canShoot = true;
    }

    private void PlayMuzzleFlash()
    {
        muzzleFalsh.Play();
    }

    private void ProcessRayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 0.1f);
    }
}
