using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assault : Gun
{
    // Start is called before the first frame update
    public override void Fire(string firedBy)
    {
        StartCoroutine(FireX(firedBy));
    }

    private IEnumerator FireX(string firedBy)
    {
        for(int i = 0; i < 3; i++)
        {
            GameObject bg = Instantiate(bullet, firePosition.position, transform.rotation);
            bg.GetComponent<Bullet>().firedBy = firedBy;
            yield return new WaitForSeconds(0.2f);
        }
    } 
}
