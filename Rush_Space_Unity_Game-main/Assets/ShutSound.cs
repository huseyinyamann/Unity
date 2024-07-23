using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ShutSound : MonoBehaviour
{
    private SoundManager _soundManager;
    private AudioSource _source;

    public AudioClip shutlaser, shutAK, gun1, gun2, gun3, artillery, m4a1;
    public AudioClip bulletchange_laser, bulletchange_auto_gun, bulletchange_pistol, bulletchange_artillery;

    private void Start()
    {
        _soundManager = GetComponentInParent<SoundManager>();   
        _source=GetComponent<AudioSource>();
    }
    public void ShutLaser()
    {
        _source.PlayOneShot(shutlaser);
    }
    public void ShutAKEL()
    {
        _source.PlayOneShot(shutAK);
    }

    public void ShutM4A1()
    {
        _source.PlayOneShot(m4a1);
    }
    public void GUN1()
    {
        _source.PlayOneShot(gun1);
    }
    public void GUN2()
    {
        _source.PlayOneShot(gun2);
    }
    public void GUN3()
    {
        _source.PlayOneShot(gun3);
    }
    public void Articelly()
    {
        _source.PlayOneShot(artillery);
    }

    public async void Reload2Min()
    {
        _source.PlayOneShot(bulletchange_pistol);
        await Task.Delay(1000);
        _source.PlayOneShot(bulletchange_pistol);
    }

    public void ArticellyReload()
    {
        _source.PlayOneShot(bulletchange_artillery);
    }
}
