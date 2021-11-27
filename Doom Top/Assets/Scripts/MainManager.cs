using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Manager { get; private set; }

    public GameObject selectedGun;

    private void Awake()
    {
        if (Manager != null)
        {
            Destroy(gameObject);
            return;
        }

        Manager = this;
        DontDestroyOnLoad(gameObject);

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
