using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScrollSkill : MonoBehaviour {

    void OnEnable()
    {
        ShopManager.instance.GetInfoSkill();
    }
    void Update()
    {
        for (int i = 0; i < ShopManager.instance.listItemSkillInShop.Length; i++)
        {
            if(ShopManager.instance.listItemSkillInShop[i].id == 0)
            {
                transform.GetChild(0).GetChild(0).GetChild(i).GetChild(2).GetComponent<Text>().text = GameSettings.CountItemMagnet.ToString();
            }
            else
            {
                transform.GetChild(0).GetChild(0).GetChild(i).GetChild(2).GetComponent<Text>().text = GameSettings.CountItemFly.ToString();
            }
        }
    }
}
