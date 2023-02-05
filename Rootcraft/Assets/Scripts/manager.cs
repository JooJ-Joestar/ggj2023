using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
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
        leftSpawnScreenPercentageBoundarie = 0.1f;
        rightSpawnScreenPercentageBoundarie = 0.4f;

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

            if (spawnTimer > 0.6f)
            {
                spawnTimer -= 0.15f;
            }
            timer = Time.time;
        }
    }
}
