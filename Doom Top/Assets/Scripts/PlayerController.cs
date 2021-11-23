using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject gunPrefab;
    public GameObject powerIndicator;
    Vector3 gunPosition = new Vector3(0, 1.5f, 1);
    private Gun gunScript;

    
    private float horizontalInput;
    private float verticalInput;

    private Rigidbody playerRb;
    private Animator playerAnim;

    private Vector3 startPos;
    private Quaternion startRot;

    private float yRot;

    //stats
    public float speed = 5.0f;
    public float rotateSpeed = 4.0f;
    public bool isAlive;
    public int lives = 3;
    public bool canDie = true;
    public bool scatterShot =true;
    public bool hasPowerup = false;
    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        startPos = transform.position;
        startRot = transform.rotation;
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAnim.SetInteger("WeaponType_int", 1);

        GameObject gun = Instantiate(gunPrefab, transform.position + gunPosition, transform.rotation);

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

        if (Input.GetMouseButtonDown(1))
        {
            Teleport();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isAlive) return;

        MovePlayer();

       
      
    }

    private void MovePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        yRot += Input.GetAxis("Horizontal") * rotateSpeed;

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, yRot, transform.localEulerAngles.z);

        playerRb.MovePosition(transform.position + (transform.forward * speed * verticalInput * Time.deltaTime));

        if (verticalInput == 0)
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
        if (other.gameObject.CompareTag("Bullet") && isAlive && canDie && other.gameObject.GetComponent<Bullet>().firedBy.Equals("enemy"))
        {
            
            Destroy(other.gameObject);
            Destroy(gunScript.gameObject);
            Die();
        }
    }

    void Die()
    {

        isAlive = false;

        playerAnim.SetInteger("WeaponType_int", 0);

        playerAnim.SetBool("Death_b", true);

        playerAnim.SetInteger("DeathType_int", Random.Range(1, 3));

        //Destroy(gameObject, 2);
        Invoke("Reset", 2);
    }

    private void Reset()
    {
        GameObject.Find("Game Manager").GetComponent<GameManager>().playerKilled();

        isAlive = true;

        transform.position = startPos;
        transform.rotation = startRot;
        playerAnim.Rebind();

        playerAnim.SetInteger("WeaponType_int", 1);
        GameObject gun = Instantiate(gunPrefab, startPos + gunPosition, startRot);

        gun.transform.SetParent(transform);

        gunScript = gun.GetComponent<Gun>();
    }

    void FireBullet()
    {

        if (scatterShot)
        {
            Debug.Log("Scatter shot");
            for(int  i = -20; i <= 20; i+=10)
            {
                Debug.Log(transform.rotation);
                Quaternion shotAngle = Quaternion.Euler(new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + i, transform.localEulerAngles.z));
                GameObject bullet = Instantiate(gunScript.bullet, gunScript.firePosition.position, shotAngle);
                bullet.GetComponent<Bullet>().firedBy = "player";
            }
        }
        else
        {
            GameObject bullet = Instantiate(gunScript.bullet, gunScript.firePosition.position, transform.rotation);
            bullet.GetComponent<Bullet>().firedBy = "player";
        }

      
    }


    void Fire() { 
    }
    void Teleport()
    {
        transform.position = GameObject.Find("Game Manager").GetComponent<GameManager>().RandomPosition(transform.position.y);
    }

}
         

