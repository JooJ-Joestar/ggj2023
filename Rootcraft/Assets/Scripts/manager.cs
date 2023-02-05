using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Potato: 0
    // Carrot: 1
    // Beat: 2
    // Cassava: 3
    // Radish: 4

    int[][] recipes = new int[][]
    {
        new int[] { 2, 4 },
        new int[] { 3, 1 },
        new int[] { 3, 0 },
        new int[] { 0, 4 },
        new int[] { 3, 2, 0 },
        new int[] { 2, 3, 3 },
        new int[] { 4, 1, 3 },
        new int[] { 0, 0, 4 },
        new int[] { 3, 1, 3, 1 },
        new int[] { 2, 3, 3, 4 },
        new int[] { 1, 3, 1, 2 },
        new int[] { 0, 2, 1, 2 },
        new int[] { 4, 1, 3, 0, 4 },
        new int[] { 2, 4, 2, 4, 3 },
        new int[] { 0, 4, 2, 4, 0 },
        new int[] { 1, 0, 0, 2, 3 }
    };

    public List<GameObject> roots;
    Vector2 spawnPosition;
    Vector2 screenSize;
    float timer = 0.0f;
    float spawnTimer = 3.0f;
    float leftSpawnScreenPercentageBoundarie;
    float rightSpawnScreenPercentageBoundarie;

    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
        leftSpawnScreenPercentageBoundarie = 0.6f;
        rightSpawnScreenPercentageBoundarie = 0.44f;

        screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        spawnPosition = new Vector2(screenSize.x * UnityEngine.Random.Range(leftSpawnScreenPercentageBoundarie, rightSpawnScreenPercentageBoundarie), 10);
    }

    // Update is called once per frame
    void Update()
    {
        SpawnRoot();
    }

    void SpawnRoot()
    {
        if (Time.time - timer > spawnTimer)
        {
            int rootIndex = UnityEngine.Random.Range(0, roots.Count);
            GameObject clone = (GameObject)Instantiate(roots[rootIndex], spawnPosition, Quaternion.identity);

            Destroy(clone, 10);

            spawnPosition = new Vector2(screenSize.x * UnityEngine.Random.Range(leftSpawnScreenPercentageBoundarie, rightSpawnScreenPercentageBoundarie), 10);

            if (spawnTimer > 0.5f)
            {
                spawnTimer -= 0.1f;
            }
            timer = Time.time;
        }
    }
}
