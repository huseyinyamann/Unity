using UnityEngine;

public class StarController : MonoBehaviour
{
    public float speed = 2f; // Yıldızın hareket hızı
    private Vector3 direction; // Yıldızın hareket yönü
    private float minX, maxX, minY, maxY; // Ekrandaki sınırlar

    void Start()
    {
        // Ekrandaki sınırları belirleyin
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        minX = bottomLeft.x;
        maxX = topRight.x;
        minY = bottomLeft.y;
        maxY = topRight.y;

        // Rastgele bir yön belirleyin
        direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;
    }

    void Update()
    {
        // Yıldızın pozisyonunu güncelleyin
        transform.position += direction * speed * Time.deltaTime;

        // Ekrandaki sınırların dışına çıktıysa, yönünü tersine çevirin
        if (transform.position.x < minX || transform.position.x > maxX)
        {
            direction = new Vector3(-direction.x, direction.y, 0f);
        }
        if (transform.position.y < minY || transform.position.y > maxY)
        {
            direction = new Vector3(direction.x, -direction.y, 0f);
        }
    }
}
