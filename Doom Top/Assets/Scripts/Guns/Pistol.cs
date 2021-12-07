using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    // Start is called before the first frame update
    public override void Fire(string firedBy)
    {
        if (canFire)
        {
            canFire = false;
 
            GameObject bg = Instantiate(bullet, firePosition.position, transform.rotation);
            Bullet b = bg.GetComponent<Bullet>();
            b.firedBy = firedBy;
            Destroy(bg, fireDistance / b.speed);

            Invoke("CanFireAgain", fireRate);
        }
        
    }

}
