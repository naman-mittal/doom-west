using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePosition;

    public float fireRate = 1;
    public float fireDistance = 1;
    public AudioClip fireSound;

    public bool canFire = true;

    protected bool poweredUp = false;

    void Start()
    {

    }

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
