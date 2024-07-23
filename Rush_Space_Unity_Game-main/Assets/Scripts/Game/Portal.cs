using System.Threading.Tasks;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject checkpoint1;
    public GameObject checkpoint2;
    private bool inCheckpoint = false;
    private GameObject player;

    [SerializeField] private SpriteRenderer playerSprite, gunSprite;

    //REFERENCES
    private SoundManager soundManager;
    [SerializeField] private VolumeManager volumeManager;

    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "CheckPoint1")
        {
            Teleport(checkpoint2);
        }
        if (other.gameObject.tag == "CheckPoint2")
        {
            Teleport(checkpoint1);
        }
    }

    async void Teleport(GameObject checkPoint)
    {
        GetComponent<PlayerController>().enabled = false;
        GetComponentInChildren<LaserController>().enabled = false;
        soundManager.PlaySound("teleport");
        volumeManager.viggnetteEfect = true;
        playerSprite.enabled = false;
        gunSprite.enabled = false;
        await Task.Delay(1000);
        playerSprite.enabled = true;
        gunSprite.enabled = true;
        volumeManager.viggnetteEfect = false;

        if (checkPoint == checkpoint1) gameObject.transform.position = checkPoint.transform.position + new Vector3(-4, 0, 0);
        if (checkPoint == checkpoint2) gameObject.transform.position = checkPoint.transform.position + new Vector3(4, 0, 0);

        await Task.Delay(2000);

        GetComponent<PlayerController>().enabled = true;
        GetComponentInChildren<LaserController>().enabled = true;
    }
}
