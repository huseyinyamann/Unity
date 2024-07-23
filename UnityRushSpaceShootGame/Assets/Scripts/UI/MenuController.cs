using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private UIManager _manager;
    [SerializeField] private PlayerController _playerController;

    private void Awake()
    {
        _manager = GetComponent<UIManager>();
    }

    public void OpenGamePlay()
    {
        _manager.gameplayCanvas.enabled = true;
        _manager.controllerCanvas.enabled = true;
    }

    public void CloseAds()
    {
        _manager.adsCanvas.enabled = false;
    }

    public void CloseController()
    {
        _manager.controllerCanvas.enabled = false;
    }

    public void Restart()
    {
        PlayerPrefs.DeleteKey("time");
        Time.timeScale = 1;
        _playerController.goldCount = 0;
    }

    public void OpenGamever()
    {
        _manager.gameoverCanvas.enabled = true;
    }

    public void CloseGameOver()
    {
        _manager.gameoverCanvas.enabled = false;
    }

    public void OpenHome()
    {
        SceneManager.LoadScene("Home");
    }

    public void CloseGamePlay()
    {
        if(_manager.gameplayCanvas != null)_manager.gameplayCanvas.enabled = false;
       if(_manager.controllerCanvas !=null) _manager.controllerCanvas.enabled = false;
    }

    public void Pause()
    {
        _manager.pauseCanvas.enabled = true;
        Time.timeScale = 0;
    }

    public void ClosePause()
    {
        _manager.pauseCanvas.enabled= false;
    }

    public async void Resume()
    {
        Time.timeScale = 0;

        _manager.noTouchPanel.SetActive(true);

        _manager.pauseCanvas.enabled = false;

        _manager.coundownAnim.SetTrigger("Countdown");

        await Task.Delay(5000);

        Time.timeScale = 1;

        _manager.noTouchPanel.SetActive(false);
    }

    private async void NoTouch(int times)
    {
        _manager.noTouchPanel.SetActive(true);

        await Task.Delay(times);

        _manager.noTouchPanel.SetActive(true);
    }

    public async void OpenAds()
    {
        _manager.adsCanvas.enabled = true;
    }
}
