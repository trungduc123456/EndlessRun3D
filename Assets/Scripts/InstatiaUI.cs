using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstatiaUI : MonoBehaviour
{
    public GameObject txt;
    //public GameObject obj;
	void Start()
    {
        GameObject _txt = (GameObject)Instantiate(txt);
        _txt.transform.SetParent(GameObject.Find("Canvas").transform);
        _txt.GetComponent<MoveUI>().uiObject = GameObject.Find("txtTo");
        _txt.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        _txt.GetComponent<MoveUI>().check = true;

    }
}
