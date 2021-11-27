using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{

    public override void Fire(string firedBy)
    {
        for (int i = -10; i <= 10; i += 10)
        {
            Debug.Log(transform.rotation);
            Quaternion shotAngle = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + i, transform.eulerAngles.z));
            GameObject bg = Instantiate(bullet, firePosition.position, shotAngle);
            bg.GetComponent<Bullet>().firedBy = firedBy;
        }
    }



}
