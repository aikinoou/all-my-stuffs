using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : WeaponBase
{
    [SerializeField] private Rigidbody bulletPrefab1;
    [SerializeField] private Rigidbody bulletPrefab2;
    [SerializeField] private int numBullets = 5;
    [SerializeField] private float spreadAngle = 20.0f;
    [SerializeField] private float force = 50;

    protected override void Attack(float percent)
    {
        Ray camRay = InputManager.GetCameraRay();
        
        Rigidbody selectedBulletPrefab = percent > 0.5f ? bulletPrefab2 : bulletPrefab1;

        for (int i = 0; i < numBullets; i++)//Loop multiple bullets for shot gun
        {
            Quaternion randomRotation = Quaternion.Euler(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0);// Calculate a random direction in spread angle
            
            Vector3 randomDirection = randomRotation * camRay.direction; //Gets the calculations
            Rigidbody rb = Instantiate(selectedBulletPrefab, camRay.origin, Quaternion.LookRotation(randomDirection));//create bullets on mouse
            rb.AddForce(Mathf.Max(percent, 0.1f) * force * randomDirection, ForceMode.Impulse);//Speed of bullets 
            Destroy(rb.gameObject, 2); //destory bullets after x second
        }
    }
}