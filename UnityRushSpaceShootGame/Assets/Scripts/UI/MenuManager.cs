using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public enum MenuType
    {
        GamePlay,
        Home,
        Pause,
        Resume,
        Gameover,
        Ads
    }

    public MenuType currentMenu;

    private MenuController menuController;

    void Start()
    {
        menuController = GetComponent<MenuController>();
        currentMenu = MenuType.GamePlay;
        menuController.Restart();
    }

    private void Update()
    {

    }

    public void MenuOpen()
    {
        switch (currentMenu)
        {
            case MenuType.GamePlay:
                Debug.Log("GamePlay menu açıldı");

                menuController.OpenGamePlay();
                menuController.CloseGameOver();
                break;
            case MenuType.Home:

                Debug.Log("Home menu açıldı");

                menuController.CloseGamePlay();
                menuController.OpenHome();
                menuController.ClosePause();
                break;
            case MenuType.Pause:

                Debug.Log("Pause menu açıldı");
                menuController.Pause();
                menuController.CloseGamePlay();
                break;
            case MenuType.Resume:

                Debug.Log("Resume");
                menuController.Resume();
                menuController.OpenGamePlay();
                break;

            case MenuType.Ads:

                Debug.Log("Ads");
                Ads();
                menuController.CloseController();
                break;

            case MenuType.Gameover:

                Debug.Log("GameOver");
                menuController.CloseGamePlay();
                menuController.OpenGamever();
                break;
        }
    }

    public void GamePlay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        currentMenu = MenuType.GamePlay;
        MenuOpen();
        Time.timeScale = 1;
    }

    public void ClickAds()
    {
        menuController.CloseAds();// can fulleme timescale işlemleri

    }

    public async void Ads()
    {
        currentMenu = MenuType.Ads;
       // Time.timeScale = 0;
        menuController.OpenAds();

        await Task.Delay(5000);
        GameOver();
    }

    public void GameOver()
    {
        currentMenu = MenuType.Gameover;
        MenuOpen();
        Time.timeScale = 0;
    }

    public void Home()
    {
        currentMenu = MenuType.Home;
        MenuOpen();
    }

    public void Quit()
    {
        Application.Quit();
        MenuOpen();
    }

    public void Pause()
    {
        currentMenu = MenuType.Pause;
        MenuOpen();
    }

    public void Resume()
    {
        currentMenu = MenuType.Resume;
        MenuOpen();
    }
}
