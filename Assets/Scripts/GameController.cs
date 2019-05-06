using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazardAsteroids;
    public Vector3 spawnValues;
    public int hazardCount;

    public float spawnWait; //time beetween each particular hazard spawn
    public float startWait; //time for the player to get ready
    public float waveWait; ////time beetween each particular wave

    public Text scoreText;
    //public Text restartText;
    public Text gameOverText;
    public Text hpText;

    private bool gameOver;
    private bool restart;

    private int score;
    private int hp;

    public GameObject restartButton;

    // Start is called before the first frame update
    void Start()
    {
        hp = 100;
        gameOver = false;
        restart = false;
        //restartText.text = "";
        restartButton.SetActive(false);
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    // Update is called once per frame
    void Update()
    {
        //for keyboard
        /*if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                //UnityEngine.Application.LoadLevel(int)  reloads scene using currently loaded level 

                SceneManager.LoadScene("Main"); 
            }
        }*/
    }

    //it's a coroutine function
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
           
            for (int i = 0; i < hazardCount; i++)
            {
                if (!gameOver)
                {
                    GameObject hazardAsteroid = hazardAsteroids[Random.Range(0, hazardAsteroids.Length)];

                    Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    Quaternion spawnRotation = Quaternion.identity; //no rotation 
                    Instantiate(hazardAsteroid, spawnPosition, spawnRotation);
                    yield return new WaitForSeconds(spawnWait);
                }
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartButton.SetActive(true);
                //restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int addScoreValue)
    {
        score += addScoreValue;
        UpdateScore();
    }

    public int PlayerHp(int damage)
    {
        hp -= damage;
        AddScore(-5);
        return hp;
    }

    void UpdateScore()
    {
        hpText.text = "HP: " + hp;
        scoreText.text = "Score: " + score;
    }
        
    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
    }

}
