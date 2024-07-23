using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Envanter : MonoBehaviour
{
    public int envanterID;
    public Button buy_button;
    public Button select_button;
    public int price;
    public bool bulletdamage, item_skin = false;
    public string gun_name, skin_name;

    private void Start()
    {
        PlayerPrefs.SetString(skin_name, "");
        PlayerPrefs.SetString("skinname", "");
    }

    private void Update()
    {
        #region BulletDamage
        if (bulletdamage)
        {
            int prefsID = PlayerPrefs.GetInt("bulletenvanter", 0);
            if (prefsID < envanterID)
            {
                buy_button.interactable = false;
            }
            else if (prefsID <= envanterID)
            {
                buy_button.interactable = true;
            }
            else
            {
                buy_button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Purchased!";
                buy_button.interactable = false;
            }
        }
        #endregion

        #region ItemSkin

        if (item_skin)
        {
            if (PlayerPrefs.GetString(skin_name) == "true")
            {
                buy_button.gameObject.SetActive(false);
                select_button.gameObject.SetActive(true);
            }
            else
            {
                buy_button.gameObject.SetActive(true);
                select_button.gameObject.SetActive(false);
            }

            if (PlayerPrefs.GetString("skinname", "") == skin_name)
            {
                select_button.interactable = false;
            }
            else
            {
                select_button.interactable = true;
            }
        }

        #endregion
    }

    public void Buy()
    {
        int rush_coin = PlayerPrefs.GetInt("rush", 0);

        #region BulletDamage


        if (rush_coin >= price)
        {
            //satın alındı
            PlayerPrefs.SetInt("bulletenvanter", envanterID + 1);
        }
        else
        {
            Debug.Log("YETERSİZ BAKİYE!");
        }

        #endregion

        #region ItemSkin 

        if (item_skin)
        {
            if (gun_name != null)
            {
                if (rush_coin >= price)
                {
                    //satın alınma basarili
                    buy_button.gameObject.SetActive(false);
                    select_button.gameObject.SetActive(true);
                    PlayerPrefs.SetString(gun_name, "true");

                }
                else
                {
                    //basarisiz
                    Debug.Log("Yetersiz bakiye");
                }
            }
             if (skin_name != null)
            {
                Debug.Log("else if");
                if (rush_coin >= price)
                {
                    //satın alınma basarili
                    buy_button.gameObject.SetActive(false);
                    select_button.gameObject.SetActive(true);
                    PlayerPrefs.SetString(skin_name, "true");
                    Debug.Log("SATIN ALMA BASARILI");
                }
                else
                {
                    //basarisiz
                    Debug.Log("Yetersiz bakiye");
                }
            }
        }

        #endregion
    }

    public void Select()
    {
        #region ItemSkin

        Debug.Log("geldi");

        if (gun_name != null)
        {
            PlayerPrefs.SetString("gunname", gun_name);
        }
        if (skin_name != null)
        {
            PlayerPrefs.SetString("skinname", skin_name);
            Debug.Log("activeskin" + skin_name);
        }

        #endregion
    }
}
