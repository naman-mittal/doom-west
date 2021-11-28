﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public List<GameObject> enemies;
    public List<GameObject> powerups;
   // public GameObject titleScreen;
    public GameObject statsScreen;
    public GameObject gameOverScreen;

    private float xSpawnRange = 10;
    private float zPosSpawnRange = 12;
    private float zNegSpawnRange = 20;
    private float spawnDelay = 8;
    private float spawnInterval = 2;

    private int wave = 1;
    private int enemyCount;

    public TextMeshProUGUI enemiesLeftText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI WaveText;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreText;

    public int score;
    public int lives;
    public int highscore;

    private bool isGameOver;

    private Vector3 startPos;
    private GameObject enemyParent;
    private GameObject powerupParent;


    public void StartGame()
    {
        


    }

    public void RestartGame()
    {
        gameOverScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //titleScreen.SetActive(true);

    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = Instantiate(playerPrefab, Vector3.zero, playerPrefab.transform.rotation);
        player.name = "Player";
        GameObject.Find("Main Camera").GetComponent<FollowPlayer>().setPlayerRef(player.GetComponent<PlayerController>());
        enemyParent = new GameObject();
        enemyParent.name = "Enemy Parent";
        powerupParent = new GameObject();
        powerupParent.name = "Powerup Parent";

        isGameOver = false;
        //titleScreen.SetActive(false);
        statsScreen.SetActive(true);
        wave = 1;
        score = 0;
        lives = 3;
        highscore = MainManager.Manager.highscore;
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;
        WaveText.text = "Wave: " + wave;
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
        yield return new WaitForSeconds(2);
        for(int i = 0; i < wave && !isGameOver; i++)
        {
            int index = Random.Range(0, enemies.Count);

           GameObject enemy = Instantiate(enemies[index], RandomPosition(enemies[index].transform.position.y), enemies[index].transform.rotation);
            enemy.transform.SetParent(enemyParent.transform);
            yield return new WaitForSeconds(spawnDelay);
        }
        
    }

    void SpawnPowerup()
    {
        
            int index = Random.Range(0, powerups.Count);

            GameObject powerup =  Instantiate(powerups[index], RandomPosition(powerups[index].transform.position.y), powerups[index].transform.rotation);

        powerup.transform.SetParent(powerupParent.transform);

    }

    public Vector3 RandomPosition(float y)
    {
        return new Vector3(Random.Range(-xSpawnRange,xSpawnRange),y, Random.Range(-zNegSpawnRange, zPosSpawnRange));
    }

    public void enemyKilled(int point)
    {
        score += point;
        scoreText.text = "Score: " + score;
        enemyCount--;
        enemiesLeftText.text = "Enemies Left: " + enemyCount;
        if (enemyCount == 0)
        {
            wave++;
            StartCoroutine(SpawnEnemy(wave));
            SpawnPowerup();
           
            enemyCount = wave;
            enemiesLeftText.text = "Enemies Left: " + enemyCount;
            WaveText.text = "Wave: " + wave;
        }
    }

    public void playerKilled()
    {
        lives--;
        livesText.text = "Lives: " + lives;
        //Instantiate(player, startPos, player.transform.rotation);

        if (lives == 0)
        {
            GameOver();
        }
        else if(enemyCount > 0)
        {
            
            RestartWave();
        }

    }

    void RestartWave()
    {
        StopAllCoroutines();
        Destroy(enemyParent);
        enemyParent = new GameObject();
        enemyParent.name = "Enemy Parent";
        powerupParent = new GameObject();
        powerupParent.name = "Powerup Parent";
        enemyCount = wave;
        enemiesLeftText.text = "Enemies Left: " + enemyCount;
        StartCoroutine(SpawnEnemy(wave));
    }

    void GameOver()
    {
        Destroy(GameObject.Find("Player"));
        Destroy(powerupParent);
        Destroy(enemyParent);
        isGameOver = true;
        gameOverScreen.SetActive(true);
        statsScreen.SetActive(false);

        finalScoreText.text = "Your Score: " + score;

        if(score > highscore)
        {
            highscore = score;
            MainManager.Manager.highscore = highscore;
            MainManager.Manager.SaveHighscore();
        }

        highScoreText.text = "HighScore: " + highscore;
    }

}
