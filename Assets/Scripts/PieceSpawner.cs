using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour {

    public PieceTypes type;
    public Piece currentPiece;


    public void Spawn()
    {
        int amtObj = 0;
        switch(type)
        {
            case PieceTypes.jump:
                {
                    amtObj = LevelManager.instance.jumps.Count;
                    break;
                }
            case PieceTypes.slide:
                {
                    amtObj = LevelManager.instance.slides.Count;
                    break;
                }
            case PieceTypes.ramp:
                {
                    amtObj = LevelManager.instance.ramps.Count;
                    break;
                }
            case PieceTypes.longblock:
                {
                    amtObj = LevelManager.instance.longblocks.Count;
                    break;
                }
        }
        // get Piece from Pool;
        currentPiece = LevelManager.instance.GetPiece(type, Random.Range(0, amtObj));
        currentPiece.gameObject.SetActive(true);
        currentPiece.transform.SetParent(transform, false);
    }
    public void DeSpawn()
    {
        currentPiece.gameObject.SetActive(false);
    }
}
