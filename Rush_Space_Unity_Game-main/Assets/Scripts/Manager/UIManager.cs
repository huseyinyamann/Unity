using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    #region TIME_TEXT

    [SerializeField] private TextMeshProUGUI timeText;

    #endregion

    #region Gold_Text
    public TextMeshProUGUI goldText;
    #endregion

    public Canvas gameplayCanvas, controllerCanvas, pauseCanvas, gameoverCanvas, adsCanvas;

    public GameObject three, two, one, zero, noTouchPanel;

    public Animator coundownAnim;

    public TextMeshProUGUI bestTime, time, adsTimer;


    //REFERENCES
    public PlayerController playerController;

    void Update()
    {
        TimeTextUpdater();
        GoldTextUpdater();
        GameoverScene();

        if (Time.timeScale == 0)
        {
            controllerCanvas.enabled = false;
        }
        else
        {
            controllerCanvas.enabled = true;
        }
    }

    private void Start()
    {
        gameplayCanvas.enabled = controllerCanvas.enabled = true;
        Time.timeScale = 1f;
    }

    private void TimeTextUpdater()
    {

        timeText.text = PlayerPrefs.GetFloat("time", 0).ToString("0.00");
    }

    private void GameoverScene()
    {
        float times = PlayerPrefs.GetFloat("time", 0);
        float bestTimes = PlayerPrefs.GetFloat("besttime", 0);

        time.text = times.ToString();
        if (times > bestTimes)
        {
            PlayerPrefs.SetFloat("besttime", times);
            bestTimes = times;
        }

        bestTime.text = bestTimes.ToString();
    }

    public IEnumerator GoldTextUpdater()
    {
        int targetGoldCount = playerController.goldCount;
        int currentGoldCount = int.Parse(goldText.text);

        while (currentGoldCount < targetGoldCount)
        {
            currentGoldCount++;
            goldText.text = currentGoldCount.ToString();
            yield return new WaitForSeconds(0.05f);
            yield return null;
        }
    }
}
