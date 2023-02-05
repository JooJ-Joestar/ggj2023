using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Beat: 0
    // Carrot: 1
    // Potato: 2
    // Cassava: 3
    // Yam: 4

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
    public GameObject topOrderIcon;
    Vector2 spawnPosition;
    Vector2 screenSize;
    float timer = 0.0f;
    float timer2 = 0.0f;
    float spawnTimer = 4.0f;
    float orderTimer = 3.0f;
    bool firstOrder = true;
    float leftSpawnScreenPercentageBoundarie;
    float rightSpawnScreenPercentageBoundarie;
    float orderIconRightBoundarie = 0.456f;
    float spaceBetweenIcons = 0.05f;
    List<int[]> orders; // holds all orders from the recipes array

    void Start()
    {
        orders = new List<int[]>();
        timer = Time.time;
        leftSpawnScreenPercentageBoundarie = 0.06f;
        rightSpawnScreenPercentageBoundarie = 0.44f;

        screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        spawnPosition = new Vector2(screenSize.x * UnityEngine.Random.Range(leftSpawnScreenPercentageBoundarie, rightSpawnScreenPercentageBoundarie), 10);
    }

    void Update()
    {
        SpawnRoot();
        addOrder();
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
                spawnTimer -= 0.05f;
            }
            timer = Time.time;
        }
    }

    void addOrder()
    {
        if (Time.time - timer2 > orderTimer)
        {
            if (orders.Count < 9)
            {
                orders.Add(recipes[UnityEngine.Random.Range(0, recipes.Length)]);
            } else
            {
                Debug.Log("You Lose!");
            }

            float temp = orderIconRightBoundarie;
            for (int i = 0; i < orders.Count; i++)
            {
                GameObject obj = (GameObject)Instantiate(topOrderIcon, new Vector2(screenSize.x * temp, -1.5f), Quaternion.identity);
                temp -= spaceBetweenIcons;
            }

            if (firstOrder)
            {
                orderTimer = 10.1f;
                firstOrder = false;
            }

            if (orderTimer > 1.0f)
            {
                orderTimer -= 0.2f;
            }
            timer2 = Time.time;
        }
    }

    void removeOrder()
    {

    }
}
