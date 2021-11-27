﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    // Start is called before the first frame update
    public override void Fire(string firedBy)
    {
        GameObject bg = Instantiate(bullet, firePosition.position, transform.rotation);
        bg.GetComponent<Bullet>().firedBy = firedBy;
    }
}
