using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomHat : MonoBehaviour
{
    public GameObject[] hats;
    
	void Start()
    {
        if(GameSettings.Hat_Select == 0)
        {
            hats[0].SetActive(true);
            hats[1].SetActive(false);
            hats[2].SetActive(false);
        }
        else if(GameSettings.Hat_Select == 1)
        {
            hats[0].SetActive(false);
            hats[1].SetActive(true);
            hats[2].SetActive(false);
        }
        else
        {
            hats[0].SetActive(false);
            hats[1].SetActive(false);
            hats[2].SetActive(true);
        }
    }
}
