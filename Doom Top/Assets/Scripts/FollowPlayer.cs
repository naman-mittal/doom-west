using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
   
    private PlayerController player;
    private Vector3 offset = new Vector3(0, 10, 0);
    //private Vector3 offset = new Vector3(0,6,9);
    //private Vector3 offset = new Vector3(0, 2.15f, 0);
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();

       offset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.isAlive) return;
        transform.position = player.transform.position + offset;
    }
}
