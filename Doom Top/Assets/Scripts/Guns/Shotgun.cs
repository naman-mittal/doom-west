using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    private int rounds = 3;

    public override void Fire(string firedBy)
    {
        if (canFire)
        {
            canFire = false;

            if (poweredUp)
            {
                FireInFanShape(firedBy, rounds * 2);
            }
            else
            {
                FireInFanShape(firedBy, rounds);
            }

            Invoke("CanFireAgain", fireRate);
        }
        
    }

    private void FireInFanShape(string firedBy,int count)
    {
        int yRot = -10 * (count / 2);
        int angleDifference = 10;

        for (int i = 0; i < (count%2==0 ? count+1 : count); i++)
        {
            if (count % 2 == 0 && yRot == 0) {

                yRot += angleDifference;
                continue; 
            }

            Quaternion shotAngle = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + yRot, transform.eulerAngles.z));
            GameObject bg = Instantiate(bullet, firePosition.position, shotAngle);
            Bullet b = bg.GetComponent<Bullet>();
            b.firedBy = firedBy;
            Destroy(bg, fireDistance / b.speed);
            yRot += angleDifference;
        }
    }

}
