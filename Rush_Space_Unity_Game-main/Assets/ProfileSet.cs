using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileSet : MonoBehaviour
{
    public Image targetOBJ;
    
    public void PressProfileIcon()
    {
        if (!targetOBJ.enabled)
        {
            targetOBJ.enabled = true;
            targetOBJ.gameObject.SetActive(true);
            print("debuf");
        }
        else
        {
            targetOBJ.enabled = false;
            targetOBJ.gameObject.SetActive(false);
        }
    }
}
