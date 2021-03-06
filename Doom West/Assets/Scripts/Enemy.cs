using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float gap = 5.0f;
    [SerializeField] private int point = 10;

    public GameObject gunPrefab;
    Vector3 gunPosition = new Vector3(0,1.5f,1);
    private Gun gunScript;

    private PlayerController player = null;
    private Rigidbody enemyRb;
    private Animator enemyAnim;
    private AudioSource enemyAudioSource;

    private bool isAlive = true;
    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.Find("Player").GetComponent<PlayerController>();
        enemyRb = GetComponent<Rigidbody>()                 ;
        enemyAnim = GetComponent<Animator>();
        enemyAudioSource = GetComponent<AudioSource>();

        enemyAnim.SetInteger("WeaponType_int", 1);

        GameObject gun = Instantiate(gunPrefab,transform.position +  gunPosition, transform.rotation);

        gun.transform.SetParent(transform);

        gunScript = gun.GetComponent<Gun>();

        gap = gunScript.fireDistance;

        gunScript.bullet.GetComponent<Bullet>().firedBy = "enemy";

        StartCoroutine(FireGun());
        
    }

    // Update is called once per frame

    IEnumerator FireGun()
    {
        while (true)
        {
            yield return new WaitForSeconds(gunScript.fireRate * 3f);
            if (isAlive)
            {
                gunScript.Fire("enemy");
                enemyAudioSource.PlayOneShot(gunScript.fireSound, 0.2f);
            }
            
        }
    }

    void FixedUpdate()
    {
        if (player == null || !isAlive || !player.isAlive) return;
        
        FollowPlayer();
    }

    void FollowPlayer()
    {
        transform.LookAt(player.transform);

        if (Vector3.Distance(transform.position, player.transform.position) > gap)
        {
            enemyAnim.SetFloat("Speed_f", 0.3f);
            enemyRb.MovePosition(transform.position + (transform.forward * speed * Time.deltaTime));
        }
        else
        {
            enemyAnim.SetFloat("Speed_f", 0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet") && isAlive && other.gameObject.GetComponent<Bullet>().firedBy.Equals("player"))
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

        enemyAnim.SetInteger("DeathType_int", Random.Range(1, 3));

        GameObject.Find("Game Manager").GetComponent<GameManager>().enemyKilled(point);

        Destroy(gameObject, 2);
    }

}
