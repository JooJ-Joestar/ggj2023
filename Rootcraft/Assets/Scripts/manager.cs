using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        new int[] { 0, 2, 1, 2 }
    };

    public List<GameObject> roots;
    public List<GameObject> rootsImages;
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
    List<GameObject> ordersIcons;

    void Start()
    {
        orders = new List<int[]>();
        ordersIcons = new List<GameObject>();
        timer = Time.time;
        leftSpawnScreenPercentageBoundarie = 0.06f;
        rightSpawnScreenPercentageBoundarie = 0.44f;

        screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        spawnPosition = new Vector2(screenSize.x * UnityEngine.Random.Range(leftSpawnScreenPercentageBoundarie, rightSpawnScreenPercentageBoundarie), 10);
    }

    public void OrderToRemove(List<string> rootNameList)
    {
        bool isOrderRemoved = false;

        foreach (int[] order in orders)
        {
            List<int> orderAux = order.ToList();

            List<int> rootNameIndexes = new List<int>();

            foreach(string rootName in rootNameList)
            {
                string rootNameAux = rootName.Split("(Clone)")[0];

                switch (rootNameAux)
                {
                    case "Beat":
                        rootNameIndexes.Add(0);
                        break;
                    case "Carrot":
                        rootNameIndexes.Add(1);
                        break;
                    case "Potato":
                        rootNameIndexes.Add(2);
                        break;
                    case "Cassava":
                        rootNameIndexes.Add(3);
                        break;
                    case "Yam":
                        rootNameIndexes.Add(4);
                        break;
                    default:
                        break;
                }
            }

            if (orderAux.Count == rootNameIndexes.Count && orderAux.All(rootNameIndexes.Contains))
            {
                isOrderRemoved = true;
                orders.Remove(order);
                Destroy(ordersIcons[0]);
                ordersIcons.RemoveAt(0);

                for (int i = 0; i < ordersIcons.Count; i++)
                {
                    Destroy(ordersIcons[i]);
                }

                break;
            }
        }

        if (isOrderRemoved)
        {
            float temp = orderIconRightBoundarie;

            for (int i = 0; i < orders.Count; i++)
            {
                GameObject obj = (GameObject)Instantiate(topOrderIcon, new Vector2(screenSize.x * temp, -1.5f), Quaternion.identity);
                ordersIcons.Add(obj);
                int numberOfIngredients = orders[i].Length;
                float offset = 0.3f;

                if (numberOfIngredients == 2)
                {
                    GameObject rootImage1 = (GameObject)Instantiate(rootsImages[orders[i][0]], new Vector2(obj.transform.position.x - offset, obj.transform.position.y), Quaternion.identity);
                    GameObject rootImage2 = (GameObject)Instantiate(rootsImages[orders[i][1]], new Vector2(obj.transform.position.x + offset, obj.transform.position.y), Quaternion.identity);

                    rootImage1.transform.parent = obj.transform;
                    rootImage2.transform.parent = obj.transform;

                }
                else if (numberOfIngredients == 3)
                {
                    GameObject rootImage1 = (GameObject)Instantiate(rootsImages[orders[i][0]], new Vector2(obj.transform.position.x, obj.transform.position.y - offset), Quaternion.identity);
                    GameObject rootImage2 = (GameObject)Instantiate(rootsImages[orders[i][1]], new Vector2(obj.transform.position.x - offset, obj.transform.position.y + offset), Quaternion.identity);
                    GameObject rootImage3 = (GameObject)Instantiate(rootsImages[orders[i][2]], new Vector2(obj.transform.position.x + offset, obj.transform.position.y + offset), Quaternion.identity);

                    rootImage1.transform.parent = obj.transform;
                    rootImage2.transform.parent = obj.transform;
                    rootImage3.transform.parent = obj.transform;


                }
                else if (numberOfIngredients == 4)
                {
                    GameObject rootImage1 = (GameObject)Instantiate(rootsImages[orders[i][0]], new Vector2(obj.transform.position.x - offset, obj.transform.position.y - offset), Quaternion.identity);
                    GameObject rootImage2 = (GameObject)Instantiate(rootsImages[orders[i][1]], new Vector2(obj.transform.position.x + offset, obj.transform.position.y - offset), Quaternion.identity);
                    GameObject rootImage3 = (GameObject)Instantiate(rootsImages[orders[i][2]], new Vector2(obj.transform.position.x - offset, obj.transform.position.y + offset), Quaternion.identity);
                    GameObject rootImage4 = (GameObject)Instantiate(rootsImages[orders[i][3]], new Vector2(obj.transform.position.x + offset, obj.transform.position.y + offset), Quaternion.identity);

                    rootImage1.transform.parent = obj.transform;
                    rootImage2.transform.parent = obj.transform;
                    rootImage3.transform.parent = obj.transform;
                    rootImage4.transform.parent = obj.transform;
                }

                temp -= spaceBetweenIcons;
            }
        }
    }

    void Update()
    {
        SpawnRoot();
        addOrder();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            removeOrder();
        }
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
            int recipeIndex = UnityEngine.Random.Range(0, recipes.Length);

            if (orders.Count < 9)
            {
                orders.Add(recipes[recipeIndex]);
            } else
            {
                Debug.Log("You Lose!");
            }

            float temp = orderIconRightBoundarie;

            for (int i = 0; i < orders.Count; i++)
            {
                GameObject obj = (GameObject)Instantiate(topOrderIcon, new Vector2(screenSize.x * temp, -1.5f), Quaternion.identity);
                ordersIcons.Add(obj);
                int numberOfIngredients = orders[i].Length;
                float offset = 0.3f;

                if (numberOfIngredients == 2)
                {
                    GameObject rootImage1 = (GameObject)Instantiate(rootsImages[orders[i][0]], new Vector2(obj.transform.position.x - offset, obj.transform.position.y), Quaternion.identity);
                    GameObject rootImage2 = (GameObject)Instantiate(rootsImages[orders[i][1]], new Vector2(obj.transform.position.x + offset, obj.transform.position.y), Quaternion.identity);

                    rootImage1.transform.parent = obj.transform;
                    rootImage2.transform.parent = obj.transform;

                } else if (numberOfIngredients == 3)
                {
                    GameObject rootImage1 = (GameObject)Instantiate(rootsImages[orders[i][0]], new Vector2(obj.transform.position.x, obj.transform.position.y - offset), Quaternion.identity);
                    GameObject rootImage2 = (GameObject)Instantiate(rootsImages[orders[i][1]], new Vector2(obj.transform.position.x - offset, obj.transform.position.y + offset), Quaternion.identity);
                    GameObject rootImage3 = (GameObject)Instantiate(rootsImages[orders[i][2]], new Vector2(obj.transform.position.x + offset, obj.transform.position.y + offset), Quaternion.identity);

                    rootImage1.transform.parent = obj.transform;
                    rootImage2.transform.parent = obj.transform;
                    rootImage3.transform.parent = obj.transform;

                    
                } else if (numberOfIngredients == 4)
                {
                    GameObject rootImage1 = (GameObject)Instantiate(rootsImages[orders[i][0]], new Vector2(obj.transform.position.x - offset, obj.transform.position.y - offset), Quaternion.identity);
                    GameObject rootImage2 = (GameObject)Instantiate(rootsImages[orders[i][1]], new Vector2(obj.transform.position.x + offset, obj.transform.position.y - offset), Quaternion.identity);
                    GameObject rootImage3 = (GameObject)Instantiate(rootsImages[orders[i][2]], new Vector2(obj.transform.position.x - offset, obj.transform.position.y + offset), Quaternion.identity);
                    GameObject rootImage4 = (GameObject)Instantiate(rootsImages[orders[i][3]], new Vector2(obj.transform.position.x + offset, obj.transform.position.y + offset), Quaternion.identity);

                    rootImage1.transform.parent = obj.transform;
                    rootImage2.transform.parent = obj.transform;
                    rootImage3.transform.parent = obj.transform;
                    rootImage4.transform.parent = obj.transform;
                }

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
        if (orders.Count > 0)
        {
            float temp = orderIconRightBoundarie;
            orders.RemoveAt(0);
            Destroy(ordersIcons[0]);
            ordersIcons.RemoveAt(0);

            for (int i = 0; i < ordersIcons.Count; i++)
            {
                Destroy(ordersIcons[i]);
            }

            for (int i = 0; i < orders.Count; i++)
            {
                GameObject obj = (GameObject)Instantiate(topOrderIcon, new Vector2(screenSize.x * temp, -1.5f), Quaternion.identity);
                ordersIcons.Add(obj);
                int numberOfIngredients = orders[i].Length;
                float offset = 0.3f;

                if (numberOfIngredients == 2)
                {
                    GameObject rootImage1 = (GameObject)Instantiate(rootsImages[orders[i][0]], new Vector2(obj.transform.position.x - offset, obj.transform.position.y), Quaternion.identity);
                    GameObject rootImage2 = (GameObject)Instantiate(rootsImages[orders[i][1]], new Vector2(obj.transform.position.x + offset, obj.transform.position.y), Quaternion.identity);

                    rootImage1.transform.parent = obj.transform;
                    rootImage2.transform.parent = obj.transform;

                }
                else if (numberOfIngredients == 3)
                {
                    GameObject rootImage1 = (GameObject)Instantiate(rootsImages[orders[i][0]], new Vector2(obj.transform.position.x, obj.transform.position.y - offset), Quaternion.identity);
                    GameObject rootImage2 = (GameObject)Instantiate(rootsImages[orders[i][1]], new Vector2(obj.transform.position.x - offset, obj.transform.position.y + offset), Quaternion.identity);
                    GameObject rootImage3 = (GameObject)Instantiate(rootsImages[orders[i][2]], new Vector2(obj.transform.position.x + offset, obj.transform.position.y + offset), Quaternion.identity);

                    rootImage1.transform.parent = obj.transform;
                    rootImage2.transform.parent = obj.transform;
                    rootImage3.transform.parent = obj.transform;


                }
                else if (numberOfIngredients == 4)
                {
                    GameObject rootImage1 = (GameObject)Instantiate(rootsImages[orders[i][0]], new Vector2(obj.transform.position.x - offset, obj.transform.position.y - offset), Quaternion.identity);
                    GameObject rootImage2 = (GameObject)Instantiate(rootsImages[orders[i][1]], new Vector2(obj.transform.position.x + offset, obj.transform.position.y - offset), Quaternion.identity);
                    GameObject rootImage3 = (GameObject)Instantiate(rootsImages[orders[i][2]], new Vector2(obj.transform.position.x - offset, obj.transform.position.y + offset), Quaternion.identity);
                    GameObject rootImage4 = (GameObject)Instantiate(rootsImages[orders[i][3]], new Vector2(obj.transform.position.x + offset, obj.transform.position.y + offset), Quaternion.identity);

                    rootImage1.transform.parent = obj.transform;
                    rootImage2.transform.parent = obj.transform;
                    rootImage3.transform.parent = obj.transform;
                    rootImage4.transform.parent = obj.transform;
                }

                temp -= spaceBetweenIcons;
            }
        }
    }
}
