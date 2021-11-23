using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> enemies;
    public List<GameObject> powerups;
    private float xSpawnRange = 10;
    private float zPosSpawnRange = 12;
    private float zNegSpawnRange = 20;
    private float spawnDelay = 8;
    private float spawnInterval = 2;

    private int wave = 1;
    private int enemyCount;

    public TextMeshProUGUI enemiesLeftText;
    // Start is called before the first frame update
    void Start()
    {
        enemiesLeftText.text = "Enemies Left: " + wave;
        enemyCount = wave;
       StartCoroutine(SpawnEnemy(wave));
        SpawnPowerup();
    }

    // Update is called once per frame
    void Update()
    {
          
    }

    IEnumerator SpawnEnemy(int wave)
    {
        for(int i = 0; i < wave; i++)
        {
            int index = Random.Range(0, enemies.Count);

            Instantiate(enemies[index], RandomPosition(enemies[index].transform.position.y), enemies[index].transform.rotation);
            yield return new WaitForSeconds(spawnDelay);
        }
        
    }

    void SpawnPowerup()
    {
        
            int index = Random.Range(0, powerups.Count);

            Instantiate(powerups[index], RandomPosition(powerups[index].transform.position.y), powerups[index].transform.rotation);
        

    }

    public Vector3 RandomPosition(float y)
    {
        return new Vector3(Random.Range(-xSpawnRange,xSpawnRange),y, Random.Range(-zNegSpawnRange, zPosSpawnRange));
    }

    public void enemyKilled()
    {
        enemyCount--;
        enemiesLeftText.text = "Enemies Left: " + enemyCount;
        if (enemyCount == 0)
        {
            wave++;
            StartCoroutine(SpawnEnemy(wave));
            SpawnPowerup();
           
            enemyCount = wave;
            enemiesLeftText.text = "Enemies Left: " + enemyCount;
        }
    }
}
