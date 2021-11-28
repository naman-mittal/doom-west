using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    private float timelastFired = 0.0f;
    private bool canFire = true;
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

            StartCoroutine(Fired());
        }
        
    }

    private IEnumerator Fired()
    {
        yield return new WaitForSeconds(fireRate);
        canFire = true;
        
    }
}
