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

    protected bool canFire = true;

    [SerializeField] protected bool poweredUp = false;

    public abstract void Fire(string firedBy);

    public void SetPoweredup(bool poweredUp)
    {
        this.poweredUp = poweredUp;
    }

    protected void CanFireAgain()
    {
        canFire = true;
    }

}
