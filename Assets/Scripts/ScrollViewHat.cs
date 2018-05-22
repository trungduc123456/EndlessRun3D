using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScrollViewHat : MonoBehaviour {

	void OnEnable()
    {
        ShopManager.instance.GetInfoHat();
    }
    void Start()
    {
        
    }
    void Update()
    {
        for (int i = 0; i < ShopManager.instance.listItemHatInShop.Length; i++)
        {
            if(PlayerPrefs.GetInt("hat"+i) == 1)
            {
                transform.GetChild(0).GetChild(0).GetChild(i).GetChild(1).GetChild(0).GetComponent<Text>().text = "Select";
               
            }
            
        }
        SelectHat();
    }
    void SelectHat()
    {
        int value = GameSettings.Hat_Select;
        for (int i = 0; i < ShopManager.instance.listItemHatInShop.Length; i++)
        {
            if(i == value)
            {
                transform.GetChild(0).GetChild(0).GetChild(i).GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(0).GetChild(0).GetChild(i).GetChild(2).gameObject.SetActive(false);
            }
        }
    }
}
