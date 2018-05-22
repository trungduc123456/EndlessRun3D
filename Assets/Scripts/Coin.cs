using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float speed;
  
    void Start()
    {
        speed = 10f;
       
    }

   
    public void MoveToPlayer(GameObject player)
    {
        transform.parent.position = Vector3.MoveTowards(transform.parent.position, player.transform.position, Time.deltaTime * speed * 3);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            this.gameObject.transform.parent.gameObject.SetActive(false);
            GameObject _txt = ObjectPoolerSimple.instance.GetPooledObject();
            _txt.gameObject.SetActive(true);
          
            _txt.GetComponent<MoveUI>().uiObject = GameObject.Find("txtCoinScore");
            _txt.transform.position = Camera.main.WorldToScreenPoint(other.transform.position);
            _txt.GetComponent<MoveUI>().check = true;
        }
    }

}
