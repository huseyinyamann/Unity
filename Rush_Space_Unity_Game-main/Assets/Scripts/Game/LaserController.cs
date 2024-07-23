using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class LaserController : MonoBehaviour
{
    public float defDistanceRay = 100f;
    public LineRenderer m_lineRenderer;
    public Transform laserFirpoint;
    public LayerMask enemy;
    public float laserDuration;
    private bool canFire = true;

    private bool isShooting = false;
    public float firedelayMain = .5f;
    public float damageclamp = 2;

    public int maxAmmo = 10;
    public int currentAmmo;

    public float reloadTime = 2f;
    private bool isReloading = false;

    public int randomGold;
    public Button reloadButton;
    public Button FireDealySkillButton;
    public bool akel47red, ak47white, ak47, m4a1, laser, gun1, gun2, gun3, artilery = false;
    public GameObject[] guns;
    public bool isSelected = false;
    public bool firedelaySkill = false;


    //Player Damage
    private float playerDamage;

    //REFERENCES
    public UIManager manager;
    private PlayerController playerController;
    public SoundManager soundManager;
    public FiringButton firingButton;
    private GunController gunController;

    private void Awake()
    {
        m_lineRenderer.enabled = false;
        playerController = GetComponentInParent<PlayerController>();
        currentAmmo = maxAmmo;
        gunController = GetComponentInParent<GunController>();

        Select();
    }

    private void GunSelect(int selectNum)
    {
        if (!isSelected)
        {
            for (int i = 0; i < guns.Length; i++)
            {
                if (i != selectNum)
                {
                    guns[i].SetActive(false);
                }
                else
                {
                    guns[i].SetActive(true);
                }
                isSelected = true;
            }
            currentAmmo = gunController.currentAmmo;
        }
    }

    private void Select()
    {
        if (laser)
        {
            gunController.Laser();
            GunSelect(0);
        }
        else if (gun1)
        {
            gunController.Pistol();
            GunSelect(1);
        }
        else if (gun2)
        {
            gunController.Pistol();
            GunSelect(2);
        }
        else if (gun3)
        {
            gunController.Pistol();
            GunSelect(3);
        }
        else if (ak47)
        {
            gunController.Ak47();
            GunSelect(4);
        }
        else if (akel47red)
        {
            gunController.Ak47();
            GunSelect(5);
        }
        else if (ak47white)
        {
            gunController.Ak47();
            GunSelect(6);
        }
        else if (m4a1)
        {
            gunController.M4A1();
            GunSelect(7);
        }
        else if (artilery)
        {
            gunController.Artillery();
            GunSelect(8);
        }
        if(!firedelaySkill)
        {
            firedelayMain = gunController.firedelayMain;
            maxAmmo = gunController.maxAmmo;
            damageclamp = gunController.damageclamp;
            reloadTime = gunController.reloadTime;
        }
        else
        {
            firedelayMain = 0.1f;
            maxAmmo = gunController.maxAmmo;
            damageclamp = gunController.damageclamp;
            reloadTime = 0.1f;
        }
        
    }

    private void Update()
    {
        laserFirpoint = GameObject.FindGameObjectWithTag("Firepoint").transform;

        Select();

        if (Input.GetMouseButton(0) && !isShooting && canFire && !isReloading && firingButton.fire)
        {
            if (currentAmmo > 0 && !isReloading)
            {
                canFire = false;
                StartCoroutine(FireDelay());
                StartCoroutine(ShootLaser());
                currentAmmo--;
            }
            else if (currentAmmo <= 0 && !isReloading)
            {
                StartCoroutine(Reload());
            }
        }

        if (currentAmmo == maxAmmo)
        {
            reloadButton.interactable = false;
        }
        else if (!isReloading)
        {
            reloadButton.interactable = true;
        }
    }

    public async void FireFire()
    {

        if (!isShooting && canFire && !isReloading)
        {
            if (currentAmmo > 0 && !isReloading)
            {
                canFire = false;
                StartCoroutine(FireDelay());
                StartCoroutine(ShootLaser());
                currentAmmo--;
            }
            else if (currentAmmo <= 0 && !isReloading)
            {
                StartCoroutine(Reload());
            }
        }

    }

    IEnumerator ShootLaser()
    {
        isShooting = true;
        m_lineRenderer.enabled = true;

        RaycastHit2D hitEnemy = Physics2D.Raycast(laserFirpoint.position, laserFirpoint.right, defDistanceRay, enemy);



        if (hitEnemy.collider != null)
        {
            if (hitEnemy.collider.gameObject.CompareTag("enemy"))
            {
                print("enemy vuruldu");
            }
            Draw2DRay(laserFirpoint.position, hitEnemy.point);
            ShutClip();



            // Get distance between player and enemy
            float distance = Vector3.Distance(transform.position, hitEnemy.collider.transform.position);

            // Calculate damage based on distance
            float damage = 100 / (distance + 1) * damageclamp; // Add 1 to avoid division by zero

            // Hasar verme, hasarı distance'e göre ayarla
            if (hitEnemy.collider.gameObject.GetComponent<Virus>() != null)
            {
                Virus virus = hitEnemy.collider.gameObject.GetComponent<Virus>();
                virus.TakeDamage(damage);
                if (virus.currentHealth <= 0)
                {
                    if (virus.gameObject.CompareTag("enemy"))
                    {
                        randomGold = Random.Range(10, 20);
                    }

                    playerController.goldCount += randomGold;
                    StartCoroutine(manager.GoldTextUpdater());
                }
            }

        }

        else
        {
            Draw2DRay(laserFirpoint.position, laserFirpoint.position + laserFirpoint.right * defDistanceRay);
        }



        yield return new WaitForSeconds(laserDuration);

        m_lineRenderer.enabled = false;
        isShooting = false;
    }

    IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(firedelayMain);
        canFire = true;
    }

    void Draw2DRay(Vector2 starPos, Vector2 endPos)
    {
        m_lineRenderer.SetPosition(0, starPos);
        m_lineRenderer.SetPosition(1, endPos);
        ShutClip();
    }

    private void ShutClip()
    {
        soundManager.PlaySound("shut");
    }
    IEnumerator Reload()
    {
        isReloading = true;
        reloadButton.interactable = false;
        soundManager.PlaySound("reloadLaser");
        yield return new WaitForSeconds(reloadTime);
        reloadButton.interactable = true;
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    public void ReloadButton()
    {
        StartCoroutine(Reload());
    }

    public async void FireDealySkill()
    {
        firedelaySkill = true;
        FireDealySkillButton.interactable = false;

        float firedelay = firedelayMain;
        float reload = reloadTime;

        reloadTime = 0.1f;
        firedelayMain = 0.1f;
        //GetComponentInParent<PlayerController>().moveSpeed = 13;

        await Task.Delay(3000);
        firedelaySkill = false;
        StartCoroutine(Reload());
        //GetComponentInParent<PlayerController>().moveSpeed = 8;

        firedelayMain = firedelay;
        reloadTime = reload;

        await Task.Delay(13000);
        if (FireDealySkillButton != null) FireDealySkillButton.interactable = true;
        
    }

}
