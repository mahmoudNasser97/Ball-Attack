using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBallGenerator : MonoBehaviour
{
    public GameObject enemyBallPrefab;
    public float spawnInterval = 2f;
    public Vector3 spawnPosition;
    public Transform transformparent;
    public int poolSize;
    public EnemyAI[] enemies;
    public float difficultyIncreaseInterval = 30f;

    private GameObject[] enemyBalls;
    private int currBallIndex;


    void Start()
    {
        enemyBalls = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            enemyBalls[i] = Instantiate(enemyBallPrefab, transformparent);
            enemyBalls[i].SetActive(false);
        }
        StartCoroutine(SpawnEnemyBalls());
        StartCoroutine(IncreaseDifficultyOverTime());
    }

    IEnumerator SpawnEnemyBalls()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            GameObject enemyBall = enemyBalls[currBallIndex];
            spawnPosition = new Vector3(Random.Range(-10f, 100f), 0.5f, Random.Range(-10f, 100f));
            float randomSize = Random.Range(10f, 30f);
            enemyBall.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
            enemyBall.SetActive(true);
            currBallIndex = (currBallIndex + 1) % poolSize;
            enemyBall.GetComponent<Rigidbody>().mass = randomSize;
        }
    }
    IEnumerator IncreaseDifficultyOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(difficultyIncreaseInterval);
            foreach (var enemy in enemies)
            {
                enemy.IncreaseDifficulty();
            }
        }
    }

}
