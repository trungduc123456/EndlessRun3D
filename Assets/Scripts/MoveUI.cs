using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUI : MonoBehaviour
{
    public bool check;
    public bool checkAddCoin;
    public GameObject uiObject;
    void Start()
    {
       
    }
    void Update()
    {
       
        if (check)
        {
            MoveTo();
            if(GetDistance(this.GetComponent<RectTransform>().anchoredPosition, uiObject.GetComponent<RectTransform>().anchoredPosition) <= 0.5f)
            {
                Debug.Log(GetDistance(this.GetComponent<RectTransform>().anchoredPosition, uiObject.GetComponent<RectTransform>().anchoredPosition));
                ObjectPoolerSimple.instance.DestroyPooledObject(this.gameObject);
              
                GameManager.instance.GetCoin();
                Debug.Log("den roi");
                check = false;


            }


        }

    }
    public void MoveTo()
    {
        this.GetComponent<RectTransform>().localPosition = Vector3.MoveTowards(this.GetComponent<RectTransform>().localPosition, uiObject.GetComponent<RectTransform>().localPosition, Time.deltaTime * 1000f);
    }
    float GetDistance(Vector3 a, Vector3 b)
    {
        return Vector3.Distance(a, b);
    }
}
