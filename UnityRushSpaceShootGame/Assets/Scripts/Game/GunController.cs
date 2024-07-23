using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float firedelayMain;
    public int maxAmmo;
    public int currentAmmo;
    public float damageclamp;
    public float reloadTime = 2f;

    private LaserController controller;

    private void Awake()
    {
        controller = GetComponent<LaserController>();
    }

    public void Ak47()
    {
        firedelayMain = 0.1f;
        maxAmmo = 25;
        currentAmmo = 25;
        damageclamp = 4;
        reloadTime = 4f;
    }

    public void M4A1()
    {
        firedelayMain = 0.1f;
        maxAmmo = 25;
        currentAmmo = 25;
        damageclamp = 4;
        reloadTime = 4f;
    }

    public void Laser()
    {
        firedelayMain = 0.4f;
        maxAmmo = 10;
        currentAmmo = 10;
        damageclamp = 1.5f;
        reloadTime = 2f;
    }
    public void Artillery()
    {
        firedelayMain = 0.1f;
        maxAmmo = 80;
        currentAmmo = 80;
        damageclamp = 6;
        reloadTime = 9f;
    }
    public void Pistol()
    {
        firedelayMain = 0.5f;
        maxAmmo = 15;
        currentAmmo = 15;
        damageclamp = 3;
        reloadTime = 3.5f;
    }
}
