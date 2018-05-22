using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public struct ItemSkillInShop
{
    public int id;
    public string name;
    public Sprite spr;
    public int cost;
}
[System.Serializable]
public struct ItemHatInShop
{
    public int id;
    public int idUnlock;
    public string name;
    public Sprite spr;

    public int cost;
}
public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public ItemSkillInShop[] listItemSkillInShop;
    public ItemHatInShop[] listItemHatInShop;
    public GameObject scrollViewSkill;
    public GameObject scrollViewHat;
    public GameObject itemSkillPrefabs;
    public GameObject itemHatPrefabs;
    public GameObject panelShop;
    public int countItemFly;
    public int countItemMagnet;
    public int allCoin;
    public Text countItemFlyInShopText;
    public Text countItemMagnetInShopText;
    public Text allCoinText;
    public List<GameObject> lstSkill;
    public List<GameObject> lstHat;
    public GameObject btnSound;
    public Sprite[] sprSound;
    void OnEnable()
    {
        //_img = GetComponent<Image>();

        if (AudioControl.enbaleSound)
        {
            btnSound.GetComponent<Image>().sprite = sprSound[0];
        }
        else
        {
            btnSound.GetComponent<Image>().sprite = sprSound[1];
        }
    }

    void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }
    void Start()
    {
        Hat_Unlock();
        allCoin = GameSettings.Coin;
        allCoinText.text = allCoin.ToString();
        AudioControl.Instance.OpenSound("El D'Vir - Ala ad-Din");
    }
    // Use this for initialization
    //void OnEnable()
    //{
    //    GetInfoSkill();
    //}
    public void GetInfoSkill()
    {
        ClearList(lstSkill);
        countItemFly = GameSettings.CountItemFly;
        countItemMagnet = GameSettings.CountItemMagnet;
        allCoin = GameSettings.Coin;
        //countItemFlyInShopText.text = countItemFly.ToString();
        //countItemMagnetInShopText.text = countItemMagnet.ToString();
        allCoinText.text = allCoin.ToString();
        for (int i = 0; i < listItemSkillInShop.Length; i++)
        {
            GameObject item = Instantiate(itemSkillPrefabs);
            item.transform.SetParent(scrollViewSkill.transform.GetChild(0).GetChild(0).transform);
            item.transform.localScale = Vector3.one;
            item.transform.GetChild(0).GetComponent<Image>().sprite = listItemSkillInShop[i].spr;
            item.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = listItemSkillInShop[i].cost.ToString();

            int _id = listItemSkillInShop[i].id;
            int _cost = listItemSkillInShop[i].cost;
            item.transform.GetChild(2).GetComponent<Text>().text = GetCountSkill(_id).ToString();
            item.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => BuySkill(_id, _cost));
            lstSkill.Add(item);

        }
    }
    public int GetCountSkill(int id)
    {
        if (id == 0)
        {
            return GameSettings.CountItemMagnet;
        }
        return GameSettings.CountItemFly;
    }
    // 0 : khoa
    // 1: mo khoa
    public void GetInfoHat()
    {
        ClearList(lstHat);
        for (int i = 0; i < listItemHatInShop.Length; i++)
        {
            listItemHatInShop[i].idUnlock = PlayerPrefs.GetInt("hat" + i);
            GameObject item = Instantiate(itemHatPrefabs);
            item.transform.SetParent(scrollViewHat.transform.GetChild(0).GetChild(0).transform);
            item.transform.localScale = Vector3.one;
            item.transform.GetChild(0).GetComponent<Image>().sprite = listItemHatInShop[i].spr;
            int _id = listItemHatInShop[i].id;
            //Debug.Log(_id);
            Debug.Log(listItemHatInShop[i].idUnlock);
            int _cost = listItemHatInShop[i].cost;
            if (listItemHatInShop[i].idUnlock == 1)
            {
                Debug.Log(_id);
                item.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Select";
                item.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => SelectHat(_id));
            }
            else
            {
                item.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = listItemHatInShop[i].cost.ToString();
                item.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => BuyHat(_id, _cost));
            }
            lstHat.Add(item);
        }
    }
    void BuyHat(int id, int cost)
    {
        if (allCoin < cost)
            return;
        PlayerPrefs.SetInt("hat" + id, 1);
        allCoin -= cost;
        GameSettings.Coin = allCoin;
        scrollViewHat.transform.GetChild(0).GetChild(0).GetChild(id).GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        scrollViewHat.transform.GetChild(0).GetChild(0).GetChild(id).GetChild(1).GetComponent<Button>().onClick.AddListener(() => SelectHat(id));
    }
    public void SelectHat(int id)
    {

        GameSettings.Hat_Select = id;
        Debug.Log(GameSettings.Hat_Select);


    }
    void BuySkill(int id, int cost)
    {
        if (allCoin < cost)
            return;

        switch (id)
        {
            case 0:
                {
                    countItemMagnet++;
                    GameSettings.CountItemMagnet = countItemMagnet;

                    break;
                }
            case 1:
                {
                    countItemFly++;
                    GameSettings.CountItemFly = countItemFly;

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

        allCoinText.text = allCoin.ToString();
    }
    public void OpenSkill()
    {
        if (!scrollViewSkill.activeInHierarchy)
        {
            scrollViewSkill.SetActive(true);
        }

        scrollViewHat.SetActive(false);
    }
    public void OpenHat()
    {
        scrollViewSkill.SetActive(false);
        scrollViewHat.SetActive(true);
    }
    public void Hat_Unlock()
    {
        if (!PlayerPrefs.HasKey("hat0"))
        {
            PlayerPrefs.SetInt("hat0", 1);
        }
        if (!PlayerPrefs.HasKey("hat1"))
        {
            PlayerPrefs.SetInt("hat1", 0);
        }
        if (!PlayerPrefs.HasKey("hat2"))
        {
            PlayerPrefs.SetInt("hat2", 0);
        }
    }
    public void OpenShop()
    {
        panelShop.SetActive(true);
        scrollViewSkill.SetActive(true);
        if (scrollViewHat.activeInHierarchy)
        {
            scrollViewHat.SetActive(false);
        }
    }
    public void ClearList(List<GameObject> lst)
    {
        if (lst.Count > 0)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                Destroy(lst[i].gameObject);
            }
            lst.Clear();
        }
    }
    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void CloseShop()
    {
        panelShop.SetActive(false);
    }
    public void ClickSound()
    {
        AudioControl.enbaleSound = !AudioControl.enbaleSound;
        if (AudioControl.enbaleSound)
        {
            btnSound.GetComponent<Image>().sprite = sprSound[0];
            //_img.sprite = sprOn;
            AudioControl.Instance.OpenSound("El D'Vir - Ala ad-Din");
            Debug.Log("theem dong mo am thanh bg vao");
        }
        else
        {
            btnSound.GetComponent<Image>().sprite = sprSound[1];
            //_img.sprite = sprOff;
            AudioControl.Instance.StopAllSound();
        }
    }
}
