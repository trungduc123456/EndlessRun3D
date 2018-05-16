using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class Inventory : MonoBehaviour
{
   
    [MenuItem("Akki/Create/Scriptable Object List")]
    public static void MakeScriptableObjectList()
    {
        ScriptableObjectUtility.CreateAsset<SegmentList>();
    }
}