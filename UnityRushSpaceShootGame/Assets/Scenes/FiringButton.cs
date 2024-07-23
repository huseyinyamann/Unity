using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringButton : MonoBehaviour
{
    public bool fire = false;

    public LaserController controller;

    public Look gun;

    private void Update()
    {
        controller = GameObject.FindGameObjectWithTag("lasercontroller").GetComponent<LaserController>();
        gun = GameObject.FindGameObjectWithTag("lasercontroller").GetComponentInChildren<Look>();
        if (fire)
        {
            controller.FireFire();
        }
        else
        {

        }

        print("fire: " + fire);
    }

    public void FireDown()
    {
        fire = true;
        gun.look = true;
    }
    public void FireUp()
    {
        fire = false;
        gun.look = false;
    }
}
