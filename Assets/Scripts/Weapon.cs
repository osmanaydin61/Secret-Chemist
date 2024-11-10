using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 25f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }



    }
    
    private void Shoot()//ateş ettiğimizde muzzle animasyonu ve çarpma animasyonunu çalıştırır
    {
        PlayMuzzleFlash();
        ProcessRayCast();

    }

    private void PlayMuzzleFlash()
    {
         if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }
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
        else return;
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        
        
           GameObject impact = Instantiate(hitEffect,hit.point,Quaternion.LookRotation(hit.normal));
            Destroy(impact,1f); 
        
        
    }
}
