using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] Transform spawner1;
    [SerializeField] Transform spawner2;
    [SerializeField] Transform spawner3;
    [SerializeField] Transform spawner4;
    [SerializeField] Transform spawner5;
    [SerializeField] Transform spawner6;
    [SerializeField] Transform spawner7;

    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;
    [SerializeField] GameObject enemy3;
    [SerializeField] GameObject enemy4;
    [SerializeField] GameObject enemy5;
    [SerializeField] GameObject enemy6;
    [SerializeField] GameObject enemy7;
    [SerializeField] GameObject enemy8;

    [SerializeField] GameObject boss1;
    [SerializeField] GameObject boss2;
    [SerializeField] GameObject boss3;

    [SerializeField] float smallEnemySpawnCD;
    [SerializeField] float mediumEnemySpawnCD;
    

    private GameObject boss1Clone;
    private bool boss1Spawned;
    private GameObject boss2Clone;
    private bool boss2Spawned;
    private GameObject boss3Clone;
    private bool boss3Spawned;

    private List<Action> spawnSmallEnemyMethods;
    private List<Action> spawnMediumEnemyMethods;


    private float smallEnemySpawnTimer = 0f;
    private float mediumEnemySpawnTimer = 0f;
    private float bossEnemySpawnTimer = 0f;

    void Start()
    {
        spawnSmallEnemyMethods = new List<Action>
        {
             SpawnLeftToRightEnemy1,
             SpawnRightToLeftEnemy1,
             SpawnTopLeftToRightEnemy1,
             SpawnTopRightToLeftEnemy1,
             SpawnTopLeftToBotEnemy1Enemy1,
             SpawnTopRightToBotEnemy1,
             SpawnTopCenterToBotEnemy1,
             SpawnTopRightToLeftEnemy5,
             SpawnTopRightToTopLeftEnemy5,
             SpawnTopLeftToRightEnemy5,
             SpawnTopLeftToTopRightEnemy5,
             SpawnTopLeftToBottomEnemy5,
             SpawnTopRightToBottomEnemy5
        };

        spawnMediumEnemyMethods = new List<Action>
        {
            SpawnEnemy2,
            SpawnEnemy3LeftAndRight,
            SpawnEnemy3CenterAndRight,
            SpawnEnemy3CenterAndLeft,
            SpawnEnemy4LeftAndRight,
            SpawnEnemy4CenterAndRight,
            SpawnEnemy4CenterAndLeft,
            SpawnEnemy6Left,
            SpawnEnemy6Right,
            SpawnEnemy6Center,
            SpawnEnemy7Left,
            SpawnEnemy7Right,
            SpawnEnemy7Center,
            SpawnEnemy8
        };

    }

    private void Update()
    {
        smallEnemySpawnTimer += Time.deltaTime;
        mediumEnemySpawnTimer += Time.deltaTime;
        bossEnemySpawnTimer += Time.deltaTime;
        if(smallEnemySpawnTimer > smallEnemySpawnCD)
        {
            smallEnemySpawnTimer = 0f;
            int index = UnityEngine.Random.Range(0, spawnSmallEnemyMethods.Count);
            spawnSmallEnemyMethods[index]();
        }
        if(mediumEnemySpawnTimer > mediumEnemySpawnCD && !IsBossAlive())
        {
            mediumEnemySpawnTimer = 0f;
            int index = UnityEngine.Random.Range(0, spawnMediumEnemyMethods.Count);
            spawnMediumEnemyMethods[index]();
        }
        if(bossEnemySpawnTimer > 60f && !IsBossAlive() && !boss1Spawned)
        {
            boss1Clone = Instantiate(boss1, spawner1.position, Quaternion.Euler(0, 0, -180));
            boss1Spawned = true;
            SoundManager.instance.playWarningSound();
            SoundManager.instance.changeToBossAudioClip();
        }
        if(bossEnemySpawnTimer >= 120f && !IsBossAlive() && !boss2Spawned)
        {
            boss2Clone = Instantiate(boss2, spawner1.position, Quaternion.Euler(0, 0, -180));
            boss2Spawned = true;
            SoundManager.instance.playWarningSound();
            SoundManager.instance.changeToBossAudioClip();
        }
        if (bossEnemySpawnTimer >= 180f && !IsBossAlive() && !boss3Spawned)
        {
            boss3Clone = Instantiate(boss3, spawner1.position, Quaternion.Euler(0, 0, -180));
            boss3Spawned = true;
            SoundManager.instance.playWarningSound();
            SoundManager.instance.changeToBossAudioClip();
        }

        if(boss1Spawned && SceneManager.GetActiveScene().name == "Easy Level" && !IsBossAlive())
        {
            GameSessionScript.instance.Win();
        }
        if (boss2Spawned && SceneManager.GetActiveScene().name == "Medium Level" && !IsBossAlive())
        {
            GameSessionScript.instance.Win();
        }
        if (boss3Spawned && SceneManager.GetActiveScene().name == "Hard Level" && !IsBossAlive())
        {
            GameSessionScript.instance.Win();
        }

    }
    private bool IsBossAlive()
    {
        return boss1Clone != null || boss2Clone != null || boss3Clone != null;
    }

    //Methods to spawn enemy1
    private void SpawnLeftToRightEnemy1()
    {
        StartCoroutine(SpawnWaveOfEnemy1(spawner6, -100, 5));
    }
    private void SpawnRightToLeftEnemy1()
    {
        StartCoroutine(SpawnWaveOfEnemy1(spawner7, 100, 5));
    }
    private void SpawnTopLeftToRightEnemy1()
    {
        StartCoroutine(SpawnWaveOfEnemy1(spawner4, -110, 5));
    }
    private void SpawnTopRightToLeftEnemy1()
    {
        StartCoroutine(SpawnWaveOfEnemy1(spawner5, 110, 5));
    }
    private void SpawnTopLeftToBotEnemy1Enemy1()
    {
        StartCoroutine(SpawnWaveOfEnemy1(spawner4, -180, 5));
    }
    private void SpawnTopRightToBotEnemy1()
    {
        StartCoroutine(SpawnWaveOfEnemy1(spawner5, -180, 5));
    }
    private void SpawnTopCenterToBotEnemy1()
    {
        StartCoroutine(SpawnWaveOfEnemy1(spawner1, -180, 5));
    }
    //Methods to spawn enemy2
    private void SpawnEnemy2()
    {
        Instantiate(enemy2, spawner1.position, Quaternion.Euler(0, 0, -180));
        Instantiate(enemy2, spawner2.position, Quaternion.Euler(0, 0, -180));
        Instantiate(enemy2, spawner3.position, Quaternion.Euler(0, 0, -180));
    }
    
    //Methods to spawn enemy3
    private void SpawnEnemy3LeftAndRight()
    {
        Instantiate(enemy3, spawner2.position, Quaternion.Euler(0, 0, -180));
        Instantiate(enemy3, spawner3.position, Quaternion.Euler(0, 0, -180));
    }
    private void SpawnEnemy3CenterAndRight()
    {
        Instantiate(enemy3, spawner1.position, Quaternion.Euler(0, 0, -180));
        Instantiate(enemy3, spawner3.position, Quaternion.Euler(0, 0, -180));
    }
    private void SpawnEnemy3CenterAndLeft()
    {
        Instantiate(enemy3, spawner1.position, Quaternion.Euler(0, 0, -180));
        Instantiate(enemy3, spawner2.position, Quaternion.Euler(0, 0, -180));
    }
    //Methods to spawn enemy4
    private void SpawnEnemy4LeftAndRight()
    {
        Instantiate(enemy4, spawner2.position, Quaternion.Euler(0, 0, -180));
        Instantiate(enemy4, spawner3.position, Quaternion.Euler(0, 0, -180));
    }
    private void SpawnEnemy4CenterAndRight()
    {
        Instantiate(enemy4, spawner1.position, Quaternion.Euler(0, 0, -180));
        Instantiate(enemy4, spawner3.position, Quaternion.Euler(0, 0, -180));
    }
    private void SpawnEnemy4CenterAndLeft()
    {
        Instantiate(enemy4, spawner1.position, Quaternion.Euler(0, 0, -180));
        Instantiate(enemy4, spawner2.position, Quaternion.Euler(0, 0, -180));
    }
    

    //Methods to spawn enemy5
    private void SpawnTopRightToLeftEnemy5()
    {
        StartCoroutine(SpawnWaveOfEnemy5(spawner5, -45, 1, 2, -180, 5));
    }
    private void SpawnTopRightToTopLeftEnemy5()
    {
        StartCoroutine(SpawnWaveOfEnemy5(spawner5, -90, 1, 2, -180, 5));
    }

    private void SpawnTopLeftToRightEnemy5()
    {
        StartCoroutine(SpawnWaveOfEnemy5(spawner4, -45, 1, 2, -180, 5));
    }

    private void SpawnTopLeftToTopRightEnemy5()
    {
        StartCoroutine(SpawnWaveOfEnemy5(spawner4, 90, 1, 2, -180, 5));
    }
    private void SpawnTopLeftToBottomEnemy5()
    {
        StartCoroutine(SpawnWaveOfEnemy5(spawner6, -45, 2, 2, -90, 5));
    }
    private void SpawnTopRightToBottomEnemy5()
    {
        StartCoroutine(SpawnWaveOfEnemy5(spawner7, 45, 1, 2, 90, 5));
    }

    //Methods to spawn enemy6
    private void SpawnEnemy6Left()
    {
        Instantiate(enemy6, spawner2.position, Quaternion.Euler(0, 0, -180));
    }
    private void SpawnEnemy6Right()
    {
        Instantiate(enemy6, spawner3.position, Quaternion.Euler(0, 0, -180));
    }
    private void SpawnEnemy6Center()
    {
        Instantiate(enemy6, spawner1.position, Quaternion.Euler(0, 0, -180));
    }
    //Methods to spawn enemy7
    private void SpawnEnemy7Left()
    {
        Instantiate(enemy7, spawner2.position, Quaternion.Euler(0, 0, -180));
    }
    private void SpawnEnemy7Right()
    {
        Instantiate(enemy7, spawner3.position, Quaternion.Euler(0, 0, -180));
    }
    private void SpawnEnemy7Center()
    {
        Instantiate(enemy7, spawner1.position, Quaternion.Euler(0, 0, -180));
    }
    //Methods to spawn enemy8
    private void SpawnEnemy8()
    {
        Instantiate(enemy8, spawner3.position, Quaternion.Euler(0, 0, -180));
        Instantiate(enemy8, spawner2.position, Quaternion.Euler(0, 0, -180));
        Instantiate(enemy8, spawner3.position, Quaternion.Euler(0, 0, -180));

    }

    private IEnumerator SpawnWaveOfEnemy1(
        Transform spawner, 
        float spawnAngle,
        int numberOfEnemySpawn)
    {
        for (int i = 0; i < numberOfEnemySpawn; i++)
        {
            Instantiate(enemy1, spawner.position, Quaternion.Euler(0, 0, spawnAngle));
            yield return new WaitForSeconds(0.3f);
        }
    }

    private IEnumerator SpawnWaveOfEnemy5(
        Transform spawner,
        float rotationSpeed,
        float rotationStart,
        float rotationDuration,
        float spawnAngle,
        int numberOfEnemySpawn)
    {
        for (int i = 0; i < numberOfEnemySpawn; i++)
        {
            var enemy5Instance = Instantiate(enemy5, spawner.position, Quaternion.Euler(0, 0, spawnAngle));
            Enemy5Script enemy5Script = enemy5Instance.GetComponent<Enemy5Script>();
            enemy5Script.rotationSpeed = rotationSpeed;
            enemy5Script.rotationStart = rotationStart;
            enemy5Script.rotationDuration = rotationDuration;
            yield return new WaitForSeconds(0.3f);
        }
    }
}
