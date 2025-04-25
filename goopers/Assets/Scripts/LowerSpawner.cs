using UnityEngine;

public class LowerSpawner : MonoBehaviour
{
    [SerializeField] private GameBoss gameBoss;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] float minSpawnTime = 2.5f;
    [SerializeField] private float maxSpawnTime = 10.0f;

    public bool firstLowerSpawn = true;
    private int waveEnemyCount;
    private int currentEnemyCount;
    private bool spawnOn;
    private float timeUntilSpawn;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnOn = gameBoss.spawnOn;
        timeUntilSpawn -= Time.deltaTime;
        if (spawnOn)
        {
            if (firstLowerSpawn)
            {
                // scale spawn times based on difficulty
                if (gameBoss.globalDifficulty >= 2.5f)
                {
                    float difficultyDivider = (1f / (gameBoss.globalDifficulty * 0.3f));
                    minSpawnTime = (minSpawnTime * difficultyDivider);
                    maxSpawnTime = (maxSpawnTime * difficultyDivider);
                }

                if (gameBoss.globalDifficulty < 2.5f)
                {
                    minSpawnTime = 0.5f;
                    maxSpawnTime = 3f;
                }

                waveEnemyCount = gameBoss.lowerEnemyCount;
                currentEnemyCount = 1;
                
                Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                SetSpawnTime();
                
                firstLowerSpawn = false;
            }

            if (timeUntilSpawn <= 0 && currentEnemyCount <= waveEnemyCount)
            {
                float x = Random.value;
                if (x >= 0.1f)
                {
                    Instantiate(enemyPrefab, transform.position, Quaternion.identity); 
                    currentEnemyCount++;
                }
                else if (currentEnemyCount < (waveEnemyCount - 1))
                {
                    Instantiate(enemyPrefab, transform.position, Quaternion.identity); 
                    Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                    currentEnemyCount += 2;
                }
                SetSpawnTime();
            }

            if (currentEnemyCount >= waveEnemyCount)
            {
                spawnOn = false;
            }
        }
    }

    private void SetSpawnTime()
    {
        timeUntilSpawn = Random.Range(minSpawnTime, maxSpawnTime);
    }
}
