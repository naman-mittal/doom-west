using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> enemies;
    private float xSpawnRange = 10;
    private float zSpawnRange = 12;
    private float spawnDelay = 2;
    private float spawnInterval = 2;

    private int wave = 1;

    public TextMeshProUGUI enemiesLeftText;
    // Start is called before the first frame update
    void Start()
    {
        enemiesLeftText.text = "Enemies Left: " + wave;
    }

    // Update is called once per frame
    void Update()
    {
        int count = GameObject.FindObjectsOfType<Enemy>().Length;
        enemiesLeftText.text = "Enemies Left: " + count;

        if (count == 0)
        {
            SpawnEnemy(wave);
            wave++;
        }
    }

    void SpawnEnemy(int wave)
    {
        for(int i = 0; i < wave; i++)
        {
            int index = Random.Range(0, enemies.Count);

            Instantiate(enemies[index], RandomPosition(), enemies[index].transform.rotation);
        }
        
    }

    Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-xSpawnRange,xSpawnRange),0, Random.Range(-zSpawnRange, zSpawnRange));
    }
}
