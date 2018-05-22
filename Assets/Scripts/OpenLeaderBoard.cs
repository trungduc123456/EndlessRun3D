using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OpenLeaderBoard : MonoBehaviour {

    void OnEnable()
    {
        int index = 0;
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            //var entry = LeaderBoard.instance.GetEntry(index);
            //Debug.Log(entry.name);
            transform.GetChild(0).GetChild(index).GetChild(1).GetComponent<Text>().text = LeaderBoard.instance.Entries[index].name;
            transform.GetChild(0).GetChild(index).GetChild(2).GetComponent<Text>().text = LeaderBoard.instance.Entries[index].score.ToString();
            index++;

        }
    }
}
