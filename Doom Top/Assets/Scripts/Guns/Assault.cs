using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assault : Gun
{
    private bool canFire = true;
    private int rounds = 3;
    // Start is called before the first frame update
    public override void Fire(string firedBy)
    {
        if (canFire)
        {
            canFire = false;
            if (poweredUp)
            {
                StartCoroutine(FireNRounds(firedBy, rounds * 2));
            }
            else
            {
                StartCoroutine(FireNRounds(firedBy, rounds));
            }
            
            StartCoroutine(Fired());
        }
        
    }

    private IEnumerator FireNRounds(string firedBy, int n)
    {
        for(int i = 0; i < n; i++)
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
