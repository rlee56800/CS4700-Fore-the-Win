using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bullet;       // bullet
    public Transform firePoint;     // where the bullet fires from
    public float fireForce;         // bullet speed

    public void Fire()
    {
        GameObject projectile = Instantiate(bullet, firePoint.position, firePoint.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up*fireForce, ForceMode2D.Impulse);
    }
}
