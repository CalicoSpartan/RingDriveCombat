using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;


public class LevelManager : MonoBehaviour {

    // Use this for initialization
    public GameObject enemy;
    public GameObject bridge2;
    public RingManager ring;
    public PlayerController player;
    public GUIManager guiManager;
    public Transform powerupSpawnPoint;
    public Transform coinSpawnPoint;
    public GameObject powerupPrefab;
    public GameObject coinPrefab;


    public int currentWave = 1;
    int enemiesThisWave = 0;
    int currentIndex = 0;
    bool spawningWave = true;
    bool bCheckIfWaveFinished = false;
    int playerPoints = 0;
    public float timePointDelay = 5f;
    public int pointsPerKill = 3;
    public int pointsPerWave = 10;
    public int pointsPerCoin = 5;
    float waveStartTime;


    public List<Bridge> bridges;
    public List<EnemyController> enemies;
    public List<int> powerupsThisWave = new List<int>(10);
    public List<int> coinsThisWave = new List<int>(10);
    public List<float> maxVertTurnTime = new List<float>(10);
    public List<float> ringSpeeds = new List<float>(10);
    public List<float> minVertTurnTime = new List<float>(10);
    public List<float> maxHorzTurnTime = new List<float>(10);
    public List<float> minHorzTurnTime = new List<float>(10);
    public List<float> maxMoveTime = new List<float>(10);
    public List<float> minMoveTime = new List<float>(10);
    public List<float> shotFrequency = new List<float>(10);
    public List<float> shotDelay = new List<float>(10);
    public List<float> startingHealth = new List<float>(10);
    public int enemiesKilled = 0;
    bool gameRunning = true;
    float lastTime = 0f;
    float powerupSpawnTime; 
    float coinSpawnTime;
    public static bool bPaused = false;

    private void Awake()
    {
        UnPauseGame();
    }

    void Start() {

        bridges = Object.FindObjectsOfType<Bridge>().ToList<Bridge>();
        ring.rotationSpeed = ringSpeeds[currentWave - 1];
        guiManager.waveAnouncementGUI.text = "Wave " + currentWave.ToString();
        guiManager.DisplayWaveNumber();
        waveStartTime = Time.timeSinceLevelLoad;
        StartSpawningPickups();

    }

    void StartSpawningPickups()
    {
        for (int i = 0; i < powerupsThisWave[currentWave - 1];i++)
        {
            float tempTime = Random.Range(1f, 25f);
            StartCoroutine(powerupSpawnDelay(tempTime));
        }
        for (int i = 0; i < coinsThisWave[currentWave - 1]; i++)
        {
            float tempTime = Random.Range(1f, 25f);
            StartCoroutine(coinSpawnDelay(tempTime));
        }
        powerupsThisWave[currentWave - 1] = 0;
        coinsThisWave[currentWave - 1] = 0 ;
    }

    IEnumerator coinSpawnDelay(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        if (gameRunning)
        {
            SpawnCoin();
        }
        
    }

    IEnumerator powerupSpawnDelay(float waitTime)
    {
        Debug.Log("waiting for " + waitTime + " seconds");

        yield return new WaitForSeconds(waitTime);
        if (gameRunning)
        {
            Debug.Log("SpawnedPowerup");
            SpawnPowerup();
        }
    }


    public void EndGame()
    {
        gameRunning = false;
        guiManager.ShowGameOverGUI(currentWave, playerPoints);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


    }



    public void PauseGame()
    {
        if (!bPaused)
        {

            Time.timeScale = 0;
            bPaused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            guiManager.ShowPauseMenu(true);

        }

    }

    public void UnPauseGame()
    {
        guiManager.ShowPauseMenu(false);
        Time.timeScale = 1;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        bPaused = false;
    }
    // Update is called once per frame
    void Update() {
        if (gameRunning)
        {

            if (Input.GetKeyDown(KeyCode.P))
            {
                PauseGame();
            }

            if (!bPaused)
            {
                if (spawningWave)
                {
                    bool spawningFinished = true;
                    for (int i = bridges.Count - 1; i >= 0; i--)
                    {
                        if (bridges[i].occupied == false && Vector3.Magnitude(transform.position - bridges[i].transform.position) < 60f)
                        {




                            //Debug.Log("Spawning Enemies");
                            EnemyController en1 = Instantiate(enemy, bridges[i].spawnPoint1.position, bridges[i].spawnPoint1.rotation).GetComponent<EnemyController>();
                            en1.transform.SetParent(bridges[i].spawnPoint1, true);
                            en1 = SetEnemyStats(en1);
                            //en1.StartCoroutines();
                            enemies.Add(en1);
                            //en1.transform.rotation = Quaternion.Euler(0f, bridges[i].transform.rotation.eulerAngles.y, bridges[i].transform.rotation.eulerAngles.z);
                            EnemyController en2 = Instantiate(enemy, bridges[i].spawnPoint2.position, bridges[i].spawnPoint2.rotation).GetComponent<EnemyController>();
                            en2.transform.SetParent(bridges[i].spawnPoint2, true);
                            en2 = SetEnemyStats(en2);
                            //en2.StartCoroutines();
                            enemies.Add(en2);
                            EnemyController en3 = Instantiate(enemy, bridges[i].spawnPoint3.position, bridges[i].spawnPoint3.rotation).GetComponent<EnemyController>();
                            en3.transform.SetParent(bridges[i].spawnPoint3, true);
                            en3 = SetEnemyStats(en3);
                            //en3.StartCoroutines();
                            enemies.Add(en3);



                            bridges[i].occupied = true;

                        }
                        if (bridges[i].occupied == false)
                        {
                            spawningFinished = false;
                        }
                    }
                    if (spawningFinished)
                    {
                        spawningWave = false;
                        bCheckIfWaveFinished = true;

                    }
                    /*
                    if (bridges[currentIndex].occupied == false && Vector3.Magnitude(transform.position - bridges[currentIndex].transform.position) < 60f)
                    {
                        Debug.Log("Spawning Enemies");
                        EnemyController en1 =  Instantiate(enemy, bridges[currentIndex].spawnPoint1.position, Quaternion.Euler(90f,bridges[currentIndex].transform.rotation.eulerAngles.y, bridges[currentIndex].transform.rotation.eulerAngles.z)).GetComponent<EnemyController>();
                        en1.transform.SetParent(bridges[currentIndex].transform, true);
                        EnemyController en2 = Instantiate(enemy, bridges[currentIndex].spawnPoint2.position, bridges[currentIndex].transform.rotation).GetComponent<EnemyController>();
                        en2.transform.SetParent(bridges[currentIndex].transform, true);
                        EnemyController en3 = Instantiate(enemy, bridges[currentIndex].spawnPoint3.position, bridges[currentIndex].transform.rotation).GetComponent<EnemyController>();
                        en3.transform.SetParent(bridges[currentIndex].transform, true);

                        bridges[currentIndex].occupied = true;
                        currentIndex += 1;
                    }
                    */
                    /*
                    if (currentIndex == bridges.Count - 1)
                    {
                        spawningWave = false;
                    }
                    /*
                    /*
                    for (int i = 0; i < objects.Length;i++)
                    {
                        if (objects[i].occupied == false && Vector3.SqrMagnitude(transform.position - objects[i].transform.position) < 30f)
                        {
                            Instantiate(enemy, objects[i].spawnPoint1.position, objects[i].transform.rotation, objects[i].transform);
                            Instantiate(enemy, objects[i].spawnPoint2.position, objects[i].transform.rotation, objects[i].transform);
                            Instantiate(enemy, objects[i].spawnPoint3.position, objects[i].transform.rotation, objects[i].transform);
                            objects[i].occupied = true;
                        }

                    }
                    */
                }
                if (bCheckIfWaveFinished)
                {
                    bool done = true;
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (enemies[i])
                        {
                            done = false;
                        }
                    }
                    if (done)
                    {
                        bCheckIfWaveFinished = false;
                        for (int i = bridges.Count - 1; i >= 0; i--)
                        {
                            bridges[i].occupied = false;
                        }
                        float timeTaken = Time.timeSinceLevelLoad - waveStartTime;
                        if (timeTaken < 50f)
                        {
                            playerPoints = (int)(playerPoints * 1.4f);
                        }
                        else if (timeTaken < 70f)
                        {
                            playerPoints = (int)(playerPoints * 1.3f);
                        }
                        else if (timeTaken < 80f)
                        {
                            playerPoints = (int)(playerPoints * 1.2f);
                        }

                        waveStartTime = Time.timeSinceLevelLoad;
                        spawningWave = true;
                        currentWave += 1;
                        StartSpawningPickups();
                        ring.rotationSpeed = ringSpeeds[currentWave - 1];
                        guiManager.UpdateStatGUI(currentWave, playerPoints);
                        guiManager.DisplayWaveNumber();

                    }
                }
            }
        }
    }

    public void PickupCoin()
    {
        playerPoints += pointsPerCoin;
        guiManager.UpdateStatGUI(currentWave, playerPoints);
        Debug.Log("Picked up coin");
    }

    public void EnemyKilled()
    {
        enemiesKilled += 1;
        playerPoints += pointsPerKill;
        guiManager.UpdateStatGUI(currentWave, playerPoints);
    }

    public void SpawnCoin()
    {
        float offset = Random.Range(-50f, 50f);
        Instantiate(coinPrefab, coinSpawnPoint.position + new Vector3(0f, 0f, offset), coinSpawnPoint.transform.rotation).transform.SetParent(ring.transform, true);
        
    }

    public void SpawnPowerup()
    {
        
        float offset = Random.Range(-50f, 50f);
        Powerup powerup = Instantiate(powerupPrefab, powerupSpawnPoint.position + new Vector3(0f, 0f, offset), powerupSpawnPoint.transform.rotation).GetComponent<Powerup>();
        powerup.transform.SetParent(ring.transform, true);

    }



    public EnemyController SetEnemyStats(EnemyController enemy)
    {
        enemy.transform.localScale = new Vector3(1f, 1f, 1f);
        enemy.verticalTurnTime = Random.Range(minVertTurnTime[currentWave - 1], maxVertTurnTime[currentWave - 1]);
        enemy.horizontalTurnTime = Random.Range(minHorzTurnTime[currentWave - 1], maxHorzTurnTime[currentWave - 1]);
        enemy.maxMovementTime = maxMoveTime[currentWave - 1];
        enemy.minMovementTime = minMoveTime[currentWave - 1];
        enemy.shotFrequency = shotFrequency[currentWave - 1];
        enemy.shootDelay = shotDelay[currentWave - 1];
        enemy.startingHealth = startingHealth[currentWave - 1];
        return enemy;
    }
}
