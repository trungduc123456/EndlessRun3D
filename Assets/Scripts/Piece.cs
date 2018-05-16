using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PieceTypes
{
    none = 0, 
    ramp = 1,
    longblock = 2,
    jump = 4,
    slide = 8

}
public class Piece : MonoBehaviour
{
    public PieceTypes type;
    public int visualIndex;

}
