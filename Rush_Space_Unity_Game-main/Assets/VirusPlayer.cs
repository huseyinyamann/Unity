using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class VirusPlayer : MonoBehaviour
{
    public GameObject child;
    public GameObject healthBar;
    public GameObject particless;
    public GameObject virusPlayer;
    private bool isspawn;

    private float second = 3;
    private float active = 5;
    private bool isactive = false;

    private SoundManager soundManager;

    private void Start()
    {
        SpawnerChild();
        GetComponent<Animator>().enabled = false;
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();

    }

    private void Update()
    {
        if (active >= 0)
        {
            active -= Time.deltaTime;
        }
        else
        {
            isactive = true;
            GetComponent<Animator>().enabled = true;
        }

        if (isactive)
        {
            healthBar.transform.localScale -= new Vector3(0.05f * Time.deltaTime, 0, 0);
            if (healthBar.transform.localScale.x <= 0)
            {
                Instantiate(particless, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    private async Task SpawnerChild()
    {
        while (true)
        {
            for (int i = 0; i < 4; i++)
            {
                Instantiate(child, transform.position, Quaternion.identity);
            }

            await Task.Delay(5000);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isactive)
        {
            if (collision.CompareTag("Player"))
            {
                if (second == 3 && !isspawn)
                {
                    soundManager.PlaySound("chaos");
                    Instantiate(virusPlayer, transform.position + new Vector3(+Random.Range(1, 2), 0, 0), Quaternion.identity);
                    isspawn = true;
                }
                if (second > 0)
                {
                    second -= Time.deltaTime;
                }
                else
                {
                    second = 3;
                    isspawn = false;
                }

                print("PLAYER GELDI");
            }
        }

    }

    private async Task spawn()
    {

        await Task.Delay(5000);
    }
}
