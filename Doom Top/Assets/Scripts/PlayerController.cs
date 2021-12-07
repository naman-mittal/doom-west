using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject gunPrefab;
    public GameObject powerIndicator;
    Vector3 gunPosition = new Vector3(0, 1.5f, 1);
    public Gun gun;

    public AudioSource audioSource;
    private AudioClip fireSound;

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

        audioSource = GetComponent<AudioSource>();

        if(MainManager.Manager != null)
        {
            gunPrefab = MainManager.Manager.selectedGun;
        }
        
        isAlive = true;
        startPos = transform.position;
        startRot = transform.rotation;
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAnim.SetInteger("WeaponType_int", 1);

        GameObject gunGO = Instantiate(gunPrefab, transform.position + gunPosition, transform.rotation);

        gunGO.transform.SetParent(transform);

        gun = gunGO.GetComponent<Gun>();

        fireSound = gun.fireSound;

    }

    private void Update()
    {

        if (!isAlive) return;

        if (Input.GetMouseButtonDown(0) && gun.canFire)
        {
            Fire();
           
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
            Destroy(gun.gameObject);
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

    void Fire()
    {
        gun.Fire("player");
        playerAnim.SetBool("Shoot_b", true);
        audioSource.PlayOneShot(fireSound,0.3f);
    }
    private void Reset()
    {
        GameObject.Find("Game Manager").GetComponent<GameManager>().playerKilled();

        isAlive = true;

        transform.position = startPos;
        transform.rotation = startRot;
        playerAnim.Rebind();

        playerAnim.SetInteger("WeaponType_int", 1);
        GameObject gunGO = Instantiate(gunPrefab, startPos + gunPosition, startRot);

        gunGO.transform.SetParent(transform);

        gun = gunGO.GetComponent<Gun>();
    }

}
         

