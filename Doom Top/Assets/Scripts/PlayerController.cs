using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject gunPrefab;
    Vector3 gunPosition = new Vector3(0, 1.5f, 1);
    private Gun gunScript;

    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float rotateSpeed = 4.0f;
    private float horizontalInput;
    private float verticalInput;

    private Rigidbody playerRb;
    private Animator playerAnim;

    private Vector3 inputVector;

    private float yRot;

    public bool isAlive;
    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAnim.SetInteger("WeaponType_int", 1);

        GameObject gun = Instantiate(gunPrefab, gunPosition, transform.rotation);

        gun.transform.SetParent(transform);

        gunScript = gun.GetComponent<Gun>();
    }

    private void Update()
    {

        if (!isAlive) return;

        if (Input.GetMouseButtonDown(0))
        {
            FireBullet();
            playerAnim.SetBool("Shoot_b", true);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            playerAnim.SetBool("Shoot_b", false);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isAlive) return;

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        yRot += Input.GetAxis("Horizontal") * rotateSpeed;

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

        playerAnim.SetInteger("WeaponType_int", 0);

        playerAnim.SetBool("Death_b", true);

        playerAnim.SetInteger("DeathType_Int", Random.Range(1, 3));

        Destroy(gameObject, 2);
    }

    void FireBullet()
    {
       //Vector3 bulletRotation = gun.bullet.transform.localRotation;
        

      Instantiate(gunScript.bullet,gunScript.firePosition.position,Quaternion.Euler(transform.localEulerAngles));
        //bullet.transform.localEulerAngles = transform.localEulerAngles;
    }
}
