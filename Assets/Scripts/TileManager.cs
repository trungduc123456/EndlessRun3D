using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager instance;
    public GameObject[] tilePrefabs;
    private Transform character;
    private const float tileLength = 6f;
    private int countOfTileOnScreen;
    private float spawnZ;
    public Stack<GameObject> stkObj;
    public Vector3 firstVector;
    // Use this for initialization
    void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }
    void Start()
    {
        firstVector = new Vector3(0f, -3.88f, 2f);
        stkObj = new Stack<GameObject>();
        countOfTileOnScreen = 7;
        spawnZ = 0f;
        character = GameObject.FindGameObjectWithTag("Player").transform;
        Init(countOfTileOnScreen);
        TilePool(50);
    }

    // Update is called once per frame
    void Update()
    {
        if (character.position.z > (spawnZ - countOfTileOnScreen * tileLength))
        {
            for (int i = 0; i < countOfTileOnScreen; i++)
            {
                if (i < 2)
                {
                    GetTileFromPool(0);
                }
                else
                {
                    GetTileFromPool(-1);
                }
            }

        }
    }
    void Init(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnTile(0);
        }
    }
    void TilePool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject go = Instantiate(tilePrefabs[Random.Range(0, tilePrefabs.Length)]);
            go.SetActive(false);
            stkObj.Push(go);
        }
    }
    GameObject GetTileFromPool(int index)
    {
        GameObject go = null;
        int temp;
        temp = Random.Range(0, tilePrefabs.Length);

        if (stkObj.Count > 0)
        {
            go = stkObj.Pop();
            go.SetActive(true);

        }

        else
        {
            //Debug.Log("go");
            go = Instantiate(tilePrefabs[temp]);
            Debug.Log("first" + go.name);
            stkObj.Push(go);
        }
        //Debug.Log("first" + spawnZ);
        go.transform.position = new Vector3(firstVector.x, firstVector.y, firstVector.z + spawnZ);
        //Debug.Log(go.name);
        Debug.Log(go.transform.position);
        spawnZ += tileLength;
        // Debug.Log("second" + spawnZ);
        return go;
    }
    void SpawnTile(int index)
    {
        GameObject go;
        go = Instantiate(tilePrefabs[index]);

        go.transform.position = new Vector3(firstVector.x, firstVector.y, firstVector.z + spawnZ);

        spawnZ += tileLength;

    }
}
