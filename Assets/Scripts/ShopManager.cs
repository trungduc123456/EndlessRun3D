using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public struct ItemInShop
{
    public int id;
    public string name;
    public int cost;
}

public class ShopManager : MonoBehaviour
{
    
    public ItemInShop[] listItemInShop;
    public GameObject scrollViewShop;
    public GameObject itemPrefabs;
    public int countItemFly;
    public int countItemMagnet;
    public int allCoin;
    public Text countItemFlyInShopText;
    public Text countItemMagnetInShopText;
    public Text allCoinText;
    
  
    // Use this for initialization
    void OnEnable()
    {
        GetInfoShop();
    }
    void GetInfoShop()
    {
        countItemFly = GameSettings.CountItemFly;
        countItemMagnet = GameSettings.CountItemMagnet;
        allCoin = GameSettings.Coin;
        countItemFlyInShopText.text = countItemFly.ToString();
        countItemMagnetInShopText.text = countItemMagnet.ToString();
        allCoinText.text = allCoin.ToString();
        for (int i = 0; i < listItemInShop.Length; i++)
        {
            GameObject item = Instantiate(itemPrefabs);
            item.transform.SetParent(scrollViewShop.transform.GetChild(0).GetChild(0).transform);
            item.transform.localScale = Vector3.one;
            item.transform.GetChild(0).GetComponent<Text>().text = listItemInShop[i].name;
            item.transform.GetChild(1).GetComponent<Text>().text = listItemInShop[i].cost.ToString();
            int _id = listItemInShop[i].id;
            int _cost = listItemInShop[i].cost;
            item.GetComponent<Button>().onClick.AddListener(() => BuyItem(_id, _cost));

        }
    }
    void BuyItem(int id, int cost)
    {
        if (allCoin < cost)
            return;

        switch (id)
        {
            case 0:
                {
                    countItemFly++;
                    GameSettings.CountItemFly = countItemFly;
                    break;
                }
            case 1:
                {
                    countItemMagnet++;
                    GameSettings.CountItemMagnet = countItemMagnet;
                    break;
                }
            default:
                {
                    break;
                }
        }
        allCoin -= cost;
        GameSettings.Coin = allCoin;


    }
    void Update()
    {
        countItemFlyInShopText.text = countItemFly.ToString();
        countItemMagnetInShopText.text = countItemMagnet.ToString();
        allCoinText.text = allCoin.ToString();
    }

}
