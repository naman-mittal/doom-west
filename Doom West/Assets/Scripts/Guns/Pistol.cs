using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Pistol : Gun
{
    // POLYMORPHISM
    public override void Fire(string firedBy)
    {
        if (canFire)
        {
            canFire = false;
 
            GameObject bg = Instantiate(bullet, firePosition.position, transform.rotation);
            Bullet b = bg.GetComponent<Bullet>();
            b.firedBy = firedBy;
            Destroy(bg, fireDistance / b.speed);

            Invoke("CanFireAgain", poweredUp ? 0 : fireRate);
        }
        
    }

}
