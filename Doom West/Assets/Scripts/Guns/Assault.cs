using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Assault : Gun
{
    
    private int rounds = 3;
    
    // POLYMORPHISM
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

            Invoke("CanFireAgain", fireRate);
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

   
}
