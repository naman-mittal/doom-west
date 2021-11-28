using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePosition;
    public int bulletCount = 1;
    public float fireRate = 1;
    public float fireDistance = 1;

    public abstract void Fire(string firedBy);

}
