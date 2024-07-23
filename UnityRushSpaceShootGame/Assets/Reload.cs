using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
    public void Reloading()
    {
        GameObject.FindGameObjectWithTag("lasercontroller").GetComponent<LaserController>().
        ReloadButton();
    }
}
