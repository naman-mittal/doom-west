using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    private bool canFire = true;

    public override void Fire(string firedBy)
    {
        if (canFire)
        {
            canFire = false;

            for (int i = -10; i <= 10; i += 10)
            {
                Quaternion shotAngle = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + i, transform.eulerAngles.z));
                GameObject bg = Instantiate(bullet, firePosition.position, shotAngle);
                Bullet b = bg.GetComponent<Bullet>();
                b.firedBy = firedBy;
                Destroy(bg, fireDistance / b.speed);
            }

            StartCoroutine(Fired());
        }
        
    }

    private IEnumerator Fired()
    {
        yield return new WaitForSeconds(fireRate);
        canFire = true;

    }

}
