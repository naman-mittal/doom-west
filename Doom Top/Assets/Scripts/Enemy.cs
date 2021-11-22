using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float gap = 5.0f;
    private GameObject player;
    private Rigidbody enemyRb;
    private Animator enemyAnim;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        enemyRb = GetComponent<Rigidbody>();
        enemyAnim = GetComponent<Animator>();
    }

    // Update is called once per frame

    

    void FixedUpdate()
    {
        transform.LookAt(player.transform);
       
        if(Vector3.Distance(transform.position,player.transform.position) > gap)
        {
            enemyAnim.SetFloat("Speed_f", 0.3f);
            enemyRb.MovePosition(transform.position + (transform.forward * speed * Time.deltaTime) );
        }
        else
        {
            enemyAnim.SetFloat("Speed_f", 0f);
        }
        
    }
}
