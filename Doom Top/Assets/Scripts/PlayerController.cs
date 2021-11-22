using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Gun gun;

    [SerializeField] private float speed = 5.0f;
    private float horizontalInput;
    private float verticalInput;

    private Rigidbody playerRb;
    private Animator playerAnim;

    private Vector3 inputVector;

    private float yRot;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAnim.SetInteger("WeaponType_int", 1);
        gun = GameObject.Find("Gun").GetComponent<Gun>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireBullet();
            playerAnim.SetBool("Shoot_b", true);
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            playerAnim.SetBool("Shoot_b", false);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        yRot += Input.GetAxis("Horizontal") * speed;

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, yRot, transform.localEulerAngles.z);
     
        playerRb.MovePosition(transform.position + (transform.forward * speed * verticalInput * Time.deltaTime));

        if(verticalInput == 0)
        {
            playerAnim.SetFloat("Speed_f", 0);
        }
        else
        {
            playerAnim.SetFloat("Speed_f", 0.3f);
        }
      
    }

    void FireBullet()
    {
       //Vector3 bulletRotation = gun.bullet.transform.localRotation;
        

        Instantiate(gun.bullet, gun.firePosition.position,gun.firePosition.rotation);
    }
}
