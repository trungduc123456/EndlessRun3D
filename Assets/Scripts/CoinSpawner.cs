using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public int maxCoin;
    public float chanceToSpawner = 0.5f;
    public GameObject[] coins;
    public bool forceSpawnAll;

    void Awake()
    {
        
        
        coins = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            coins[i] = transform.GetChild(i).gameObject;
            
        }
        OnDisable();

    }
    void OnEnable()
    {
        if (Random.Range(0.0f, 1f) > chanceToSpawner)
            return;
        if(forceSpawnAll)
            for (int i = 0; i < maxCoin; i++)
            {
                coins[i].SetActive(true);
            }
        else
        {
            int r = Random.Range(0, maxCoin);
            for (int i = 0; i < r; i++)
            {
                coins[i].SetActive(true);
            }
        }
    }
    void OnDisable()
    {
        foreach (GameObject item in coins)
        {
            item.SetActive(false);
        }
    }
}
