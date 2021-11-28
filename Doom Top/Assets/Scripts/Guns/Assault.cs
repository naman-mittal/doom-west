using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assault : Gun
{
    private bool canFire = true;
    // Start is called before the first frame update
    public override void Fire(string firedBy)
    {
        if (canFire)
        {
            canFire = false;
            StartCoroutine(FireX(firedBy));
            StartCoroutine(Fired());
        }
        
    }

    private IEnumerator FireX(string firedBy)
    {
        for(int i = 0; i < 3; i++)
        {
            GameObject bg = Instantiate(bullet, firePosition.position, transform.rotation);
            Bullet b = bg.GetComponent<Bullet>();
            b.firedBy = firedBy;
            Destroy(bg, fireDistance / b.speed);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator Fired()
    {
        yield return new WaitForSeconds(fireRate);
        canFire = true;

    }
}
