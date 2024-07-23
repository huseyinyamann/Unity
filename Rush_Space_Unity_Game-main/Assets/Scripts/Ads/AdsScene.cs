using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AdsScene : MonoBehaviour
{
    private float timer = 5;
    public bool timertime = false;

    public UIManager manager;

    private void Update()
    {
        if (timertime)
        {
            if (timer >= 0) timer -= Time.deltaTime;
        }

        if (manager.adsTimer != null) manager.adsTimer.text = timer.ToString("0");
    }
}
