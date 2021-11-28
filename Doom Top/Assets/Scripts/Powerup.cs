using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerupType
{
    InvinciblePowerup,
    SpeedBoostPowerup,
    ScatterShotPowerup
}

public class Powerup : MonoBehaviour
{
    public PowerupType powerupType;
    public float duration = 4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !other.gameObject.GetComponent<PlayerController>().hasPowerup)
        {
            Pickup(other.gameObject);
        }
    }

    void Pickup(GameObject player)
    {
        PlayerController playerScr = player.GetComponent<PlayerController>();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        switch (powerupType)
        {
            case PowerupType.InvinciblePowerup:
                {
                    StartCoroutine(ProvideInvincibility(playerScr));
                }
                break;
            case PowerupType.SpeedBoostPowerup:
                {
                    StartCoroutine(ProvideSpeedBoost(playerScr));
                }
                break;
            case PowerupType.ScatterShotPowerup:
                {
                    StartCoroutine(ProvideScatterShot(playerScr));
                }
                break;
            default: Debug.Log("Invalid powerup");
                break;
        }
    }

    IEnumerator ProvideInvincibility(PlayerController player)
    {
        player.hasPowerup = true;
        player.powerIndicator.SetActive(true);
        player.canDie = false;
        yield return new WaitForSeconds(duration);
        player.canDie = true;
        player.powerIndicator.SetActive(false);
        player.hasPowerup = false;
        Destroy(gameObject);
    }

    IEnumerator ProvideSpeedBoost(PlayerController player)
    {
        float speedMultiplier = 2;
        player.hasPowerup = true;
        player.powerIndicator.SetActive(true);
        player.speed *= speedMultiplier;
        yield return new WaitForSeconds(duration);
        player.speed /= speedMultiplier;
        player.powerIndicator.SetActive(false);
        player.hasPowerup = false;
        Destroy(gameObject);
    }

    IEnumerator ProvideScatterShot(PlayerController player)
    {
        player.hasPowerup = true;
        player.powerIndicator.SetActive(true);
        player.gun.SetPoweredup(true);
        yield return new WaitForSeconds(duration);
        player.gun.SetPoweredup(false);
        player.powerIndicator.SetActive(false);
        player.hasPowerup = false;
        Destroy(gameObject);
    }
}
