using UnityEngine.Audio;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip shut;
    public AudioClip ak;
    public AudioClip dead;
    public AudioClip war;
    public AudioClip teleport;
    public AudioClip reloadLaser;
    public AudioClip buy;
    public AudioClip chaos;

    private AudioSource audioSource;

    public LaserController laserController;

    private ShutSound shutSound;

    void Awake()
    {
        shutSound = GetComponentInChildren<ShutSound>();

        if (instance == null)
        {
            instance = this;
        }

        audioSource=GetComponent<AudioSource>();
    }

    public void PlaySound(string soundName)
    {
        switch (soundName)
        {
            case "shut":
                if (laserController.ak47)
                {
                    shutSound.ShutAKEL();
                }
                else if(laserController.laser)
                {
                    shutSound.ShutLaser();
                }else if (laserController.m4a1)
                {
                    shutSound.ShutM4A1();
                }else if (laserController.artilery)
                {
                    shutSound.Articelly();
                }else if (laserController.gun1)
                {
                    shutSound.GUN1();
                }else if (laserController.gun2)
                {
                    shutSound.GUN2();
                }else if (laserController.gun3)
                {
                    shutSound.GUN3();
                }
               
                break;
            case "dead":
                audioSource.PlayOneShot(dead);
                break;
            case "chaos":
                audioSource.PlayOneShot(chaos);
                break;
            case "war":
                audioSource.PlayOneShot(war);
                break;
            case "teleport":
                audioSource.PlayOneShot(teleport);
                break;
            case "reloadLaser":

               
                 if (laserController.laser)
                {
                    shutSound.ShutLaser();
                }
                else if (laserController.gun1 || laserController.gun2 || laserController.gun3)
                {
                    shutSound.Reload2Min();
                }
                else
                {
                    shutSound.ArticellyReload();
                }
               

                break;
            case "buy":
                audioSource.PlayOneShot(buy);
                break;
            default:
                Debug.Log("Invalid sound name");
                break;
        }
    }
}
