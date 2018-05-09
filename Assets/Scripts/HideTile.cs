using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideTile : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(Hide());
        }
    }
    IEnumerator Hide()
    {
        yield return new WaitForSeconds(5f);
        transform.parent.gameObject.SetActive(false);
        TileManager.instance.stkObj.Push(transform.parent.gameObject);
    }
	
}
