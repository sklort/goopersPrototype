using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameBoss : MonoBehaviour
{
    //object references
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject gameText;
    [SerializeField] private GameObject startWaveText;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private LowerSpawner lowerSpawner;
    [SerializeField] private GameObject gameOver;
    
    // game bools
    public bool canPlay = false;
    public bool newGame;
    public bool canShop;
    public bool waveOn;
    public bool spawnOn;
    private bool rewardGiven;
    private bool difficultyUpped;
    
    //enemy data
    public int lowerEnemyCount;
    public int upperEnemyCount;
    public int enemiesDefeated;
    private int lowerEnemyMin = 3;
    private int lowerEnemyMax = 6;
    private int upperEnemyMin;
    private int upperEnemyMax;
    
    // game / player data
    public float playerMoney;
    private float waveTimer;
    public float globalDifficulty;
    public float globalFireSpeed;
    public float TDHealth;
    public float playerHealth;
   
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //new game crosshair removal
        if (!newGame)
        {
            TDHealth = 10f;
            gameOver.SetActive(false);
            crosshair.SetActive(false);
        }
        
        //exit enter screen
        if (Input.GetKeyDown(KeyCode.Return) && newGame == false)
        {
            globalDifficulty = 1.0f;
            globalFireSpeed = 1.0f;
            playerHealth = 5f;
            crosshair.SetActive(true);
            playerMoney = 50.0f;
            startScreen.SetActive(false);
            canPlay = true;
            newGame = true;
            gameText.SetActive(true);
            canShop = true;
        }
        
        //turn on start wave text
        if (canShop)
        {
            startWaveText.SetActive(true);
        }
        
        //start a wave
        if (Input.GetKeyDown(KeyCode.G) && newGame && waveOn == false)
        {
            
            // determine enemy count using difficulty
            if (globalDifficulty >= 2)
            {
                float lowerEnemyMinFloat = lowerEnemyMin * (globalDifficulty * 0.5f);
                lowerEnemyMin = Mathf.FloorToInt(lowerEnemyMinFloat);


                float lowerEnemyMaxFloat = lowerEnemyMax * (globalDifficulty * 0.5f);
                lowerEnemyMax = Mathf.FloorToInt(lowerEnemyMaxFloat);
            }

            lowerSpawner.firstLowerSpawn = true;
            rewardGiven = false;
            lowerEnemyCount = Random.Range(lowerEnemyMin, lowerEnemyMax);
            spawnOn = true;
            startWaveText.SetActive(false);
            waveOn = true;
            canShop = false;
            difficultyUpped = false;
        }

        //wave timer
        if (newGame && waveOn)
        {
            waveTimer += Time.deltaTime;
        }
        
        //turn off wave, back to shop
        if (waveOn && enemiesDefeated > (lowerEnemyCount + upperEnemyCount))
        {
            enemiesDefeated = 0;
            spawnOn = false;
            canShop = true;
            startWaveText.SetActive(true);
            
            // reward money
            if (!rewardGiven)
            {
                playerMoney += 50.0f * (globalDifficulty*3/4);
                rewardGiven = true;
            }

            // increase difficulty based on performance / time
            if (difficultyUpped == false)
            {
                float difficultyDivider = (10 * globalDifficulty)/ waveTimer;
                float difficultyUp = 0.5f + (1 * difficultyDivider);
                globalDifficulty += difficultyUp;
                difficultyUpped = true;
                waveTimer = 0;
            }

            waveOn = false;
        }

        if (TDHealth <= 0 && newGame)
        {
            gameText.SetActive(false);
            canPlay = false;
            canShop = false;
            waveOn = false;
            gameOver.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                newGame = false;
                startScreen.SetActive(true);
            }
        }
    }

    public void TDDamage(float damage)
    {
        TDHealth -= damage;
    }

    public void PlayerDamage(float damage)
    {
        playerHealth -= damage;
    }
    
    
}
