using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BuySkillButtons : MonoBehaviour
{
    public int buyGold;
    public PlayerController playerController;
    public GameObject skillObj;

    public SoundManager soundManager;
    
    public void ClickBuy()
    {
      
        if (buyGold <= playerController.goldCount)
        {
            Debug.Log("Skill Açıldı");
            soundManager.PlaySound("buy");
            playerController.goldCount -= buyGold;
            skillObj.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
