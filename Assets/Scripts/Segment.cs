using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{

    public int SegId { get; set; }
    public bool transition;

    public int length;
    public int beginY1;
    public int beginY2;
    public int beginY3;
    public int endY1;
    public int endY2;
    public int endY3;

    public PieceSpawner[] pieces;

    void Awake()
    {
        pieces = gameObject.GetComponentsInChildren<PieceSpawner>();
        for (int i = 0; i < pieces.Length; i++)
        {
            foreach (MeshRenderer item in pieces[i].GetComponentsInChildren<MeshRenderer>())
            {
                item.enabled = LevelManager.instance.SHOW_COLLIDER;
            }
        }
    }
    
    public void Spawn()
    {
        gameObject.SetActive(true);
        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i].Spawn();
        }
    }
    public void DeSpawn()
    {
        gameObject.SetActive(false);
        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i].DeSpawn();
        }
    }
}
