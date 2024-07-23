using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Takip edilecek objenin transformu
    public float smoothSpeed = 0.125f; // Hareket süresi

    public Vector3 offset; // Kamera ile hedef arasındaki fark

    void Start()
    {
        offset = new Vector3(0,0,(transform.position.z - target.position.z)); // Hedef ile kamera arasındaki farkı hesapla
    }

    void LateUpdate()
    {
        if(target.gameObject != null)
        {
            Vector3 desiredPosition = target.position + offset; // Hedefin yeni pozisyonunu hesapla
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Hareket etmek için yeni bir pozisyon hesapla
            transform.position = smoothedPosition; // Kamerayı hareket ettir
        }
    }
}
