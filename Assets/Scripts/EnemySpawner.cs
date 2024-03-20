using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    [Header("Wave Properties")]
    [SerializeField] private Transform[] spawnLocations;    //Locations to spawn enemies randomly at
    [SerializeField] private Wave[] waves; //The waves of enemies for this spawner

    [Header("Spawn Properties")]
    [SerializeField] private float spawnDelay = 1f;
    [SerializeField] private float waveDelay = 5f;

    [Header("Finished Properties")]
    [SerializeField] private float finishDelay = 5f;

    private List<GameObject> objectPool = new List<GameObject>(); //Pool of objects to pull from to use

    private int wave = 0;
    private int enemiesInWave = 0;

    private bool spawnWave;
    private bool finished;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        spawnWave = true;   //Immediately sets the spawner ready to spawn the first wave
        finished = false;
    }


    // Update is called once per frame
    void Update()
    {
        //Checks if the current spawner is finished
        if (finished)
        {
            //If the spawner is finished, return and don't move any further in the code return;
            return;
        }

        //Checks if all the waves have been completed
        if (wave >= waves.Length)
        {
            Finished(true);
            return;
        }

        //Checks if it's time to spawn the next wave
        if (spawnWave)
        {
            enemiesInWave = waves[wave].SpawnAmount;    //Sets the amount of enemies needed to be defeated in the current wave
            StartCoroutine(SpawnWave(waves[wave].EnemyPrefab, waves[wave].SpawnAmount)); //Spawns the next set of enemies
            spawnWave = false;
            return;
        }

        //Checks if all enemies have been defeated in the current wave
        if (enemiesInWave == 0)
        {
            spawnWave = true;
            wave++; //Increments to the next wave
        }
    }

    public void DefeatedEnemy(GameObject enemy = null)
    {
        if (enemy)
        {
            objectPool.Remove(enemy);
        }
        enemiesInWave--;
    }
    /// <summary>
    /// Checks if the Spawner is finished
    /// </summary>
    /// <param name="spawnerState">The current state of the spawner</param>
    private void Finished(bool spawnerState)
    {
        //Checks if the spawner is already finished
        if (spawnerState == finished)
        {
            //If the spawner is already finished, we don't want to call the same finished code over and over again return;
            return;
        }
        finished = true;

        StartCoroutine(FinishedDelay());
    }
    /// <summary>
    /// Gets a new or pooled object to use
    /// </summary>
    /// <param name="position">The position to spawn the object</param>
    /// <param name="rotation">the rotation to spawn the object</param>
    /// <returns></returns>
    private GameObject GetObject(GameObject enemy, Vector3 position, Quaternion rotation)
    {
        if (objectPool.Count != 0)
        {
            for (int i = 0; i < objectPool.Count; i++)
            {
                if (!objectPool[i].activeSelf)
                {
                    objectPool[i].transform.position = position;
                    objectPool[i].transform.rotation = rotation; objectPool[i].SetActive(true); return objectPool[i];
                }
            }
        }

        GameObject spawnedObject = Instantiate(enemy, position, Quaternion.identity);
        objectPool.Add(spawnedObject);
        return spawnedObject;
    }
    /// <summary>
    /// Gets a random position to spawn an object if there are spawn locations set, else it will spawn at the location of this object 
    /// </summary>
    /// <returns></returns>
    Vector3 GetLocation()
    {
        if (spawnLocations.Length > 0)
        {
            int randomLocation = Random.Range(0, spawnLocations.Length);
            return spawnLocations[randomLocation].position;
        }

        return transform.position;
    }
    /// <summary>
    /// Spawns the next set of enemies
    /// </summary>
    /// <param name="enemy">The enemy prefab to spawn</param>
    /// <param name="spawnAmount">Amount of enemy prefabs to spawn</param>
    /// <returns></returns>
    private IEnumerator SpawnWave(GameObject enemy, int spawnAmount)
    {
        UIManager.NewNotification($"spawning next wave in {waveDelay} seconds");
        yield return new WaitForSeconds(waveDelay); //Wave delay
        for (int i = 0; i < spawnAmount; i++)    //Increment through spawning enemies
        {
            GetObject(enemy, GetLocation(), Quaternion.identity);

            if (i < spawnAmount - 1)
            {
                yield return new WaitForSeconds(spawnDelay); //Delay in between spawns
            }
        }
    }

    /// <summary>
    /// Performs actions after finishing before and after a delay
    /// </summary>
    /// <returns></returns>
    private IEnumerator FinishedDelay()
    {
        Debug.Log("You beat the spawner!");
        UIManager.NewNotification.Invoke("Spawner has been defeated");
        UIManager.NewNotification.Invoke("advancing to next level");

        yield return new WaitForSeconds(finishDelay);
        Debug.Log("Load something after a delay.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
[System.Serializable]
public class Wave
{
    public GameObject EnemyPrefab;
    public int SpawnAmount;
}