﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float gap = 5.0f;
    [SerializeField] private float fireRate = 5.0f;

    public GameObject gunPrefab;
    Vector3 gunPosition = new Vector3(0,1.5f,1);
    private Gun gunScript;

    private PlayerController player;
    private Rigidbody enemyRb;
    private Animator enemyAnim;

    private bool isAlive = true;
    private int deathType;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        enemyRb = GetComponent<Rigidbody>()                 ;
        enemyAnim = GetComponent<Animator>();

        enemyAnim.SetInteger("WeaponType_int", 1);

        GameObject gun = Instantiate(gunPrefab,transform.position +  gunPosition, transform.rotation);

        gun.transform.SetParent(transform);

        gunScript = gun.GetComponent<Gun>();

        InvokeRepeating("FireBullet", 2, fireRate);
    }

    // Update is called once per frame

    

    void FixedUpdate()
    {
        if (!isAlive || !player.isAlive)
        {
            return;
        }
        transform.LookAt(player.transform);
       
        if (Vector3.Distance(transform.position,player.transform.position) > gap)
        {
            enemyAnim.SetFloat("Speed_f", 0.3f);
            enemyRb.MovePosition(transform.position + (transform.forward * speed * Time.deltaTime) );
        }
        else
        {
            enemyAnim.SetFloat("Speed_f", 0f);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(gunScript.gameObject);
            Destroy(other.gameObject);
            Die();
        }
    }

    void Die()
    {

        isAlive = false;

        enemyAnim.SetInteger("WeaponType_int", 0);

        enemyAnim.SetBool("Death_b", true);

        enemyAnim.SetInteger("DeathType_Int", Random.Range(1, 3));

        Destroy(gameObject, 2);
    }

    void FireBullet()
    {
        //Vector3 bulletRotation = gun.bullet.transform.localRotation;
        Debug.Log("Shooting");
        if (isAlive)
        {
            Instantiate(gunScript.bullet, gunScript.firePosition.position, Quaternion.Euler(transform.localEulerAngles));

        }
       
        //bullet.transform.localEulerAngles = transform.localEulerAngles;
    }
}
