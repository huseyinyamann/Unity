using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Hareket hızı
    private Rigidbody2D rb;

    private Vector2 movement;

    //PLAYER HEALTH
    public float health = 100;

    public TextMeshProUGUI damageText;
    public Animator damageanim;

    // Joystick referansı
    public Joystick joystick;
    public bool isEmulator = false;

    public float thrust = 10f;  // Player nesnesine uygulanacak güç miktarı
    public float timeElapsed = 0;
    public int goldCount;

    public GameObject[] heart;
    public GameObject heartDieParticles;

    float dm1 = 1.2f;

    //REFERENCES
    public MenuManager menuManager;
    public AdsScene adsScene;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Joystick ile hareket girişlerini okuyun
        if (!isEmulator)
        {
            movement.x = joystick.Horizontal;
            movement.y = joystick.Vertical;
        }
        else
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }


        if (health > 0)
        {
            timeElapsed += Time.deltaTime;
            PlayerPrefs.SetFloat("time", timeElapsed);

        }
        else if (health <= 0)
        {
            PlayerPrefs.SetFloat("time", timeElapsed);
            Die();
        }
    }

    private async void DamagePlugin(float damage)
    {
        int dm = Convert.ToInt32(damage);
        damageText.text=dm.ToString();

        damageText.enabled = true;

        damageanim.SetTrigger("DamageAnim");

        await Task.Delay(400);

       if(damageText !=null) damageText.enabled=false;
    }

    public void HeartController()
    {
        if (health <= 80)
        {
            if (heart[4] != null) StartCoroutine(Shaker(heart[4]));
        }
        if (health <= 60)
        {
            if (heart[3] != null) StartCoroutine(Shaker(heart[3]));
        }
        if (health <= 40)
        {
            if (heart[2] != null) StartCoroutine(Shaker(heart[2]));
        }
        if (health <= 20)
        {
            if (heart[1] != null) StartCoroutine(Shaker(heart[1]));
        }
        if (health <= 0)
        {
            if (heart[0] != null) StartCoroutine(Shaker(heart[0]));
        }
    }

    IEnumerator Shaker(GameObject obj)
    {
        DotweenShake dotweenShake = obj.GetComponent<DotweenShake>();

        dotweenShake.isShake = true;

        yield return new WaitForSeconds(dotweenShake.shakeDuration);

        Destroy(obj);
    }

    // FixedUpdate, fiziksel hesaplamaları yapmak için kullanılır
    void FixedUpdate()
    {
        // Rigidbody'yi hareket ettirin
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            int dama = UnityEngine.Random.Range(10, 25);
                health -= dama;
                DamagePlugin(-dama);           
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            dm1 -= Time.deltaTime;
            if (dm1 <= 0)
            {
                int dama = UnityEngine.Random.Range(10, 25);
                health -= dama;
                DamagePlugin(-dama);
                dm1 = 1.2f;
            } 
        }   
    }  

    public void Die()
    {
        menuManager.GameOver();
        //Destroy(gameObject);
    }

    public void OpenAds()
    {
        menuManager.Ads();
    }
}