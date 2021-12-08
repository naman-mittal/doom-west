using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 30.0f;
    public string firedBy // ENCAPSULATION
    {
        get
        {
            return _firedBy;
        }
        set
        {
            if(value.Equals("enemy") || value.Equals("player"))
            {
                _firedBy = value;
            }
            else
            {
                _firedBy = null;
                Debug.Log("Invalid value");
            }
        }
    }

    private string _firedBy;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
       // rb.AddForce(Vector3.forward * speed);
    }

   

    
}
