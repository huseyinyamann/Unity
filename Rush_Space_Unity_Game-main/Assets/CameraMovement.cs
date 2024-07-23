using UnityEngine;

public class CameraMovement: MonoBehaviour
{
    public float speed = 5f; // kamera hareket hızı

    // Kamera hareketi için girişlerin tanımlanması
    private float horizontalInput;
    private float verticalInput;

    void Update()
    {
        Time.timeScale = 1;

        // Girişlerin okunması
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Kamera pozisyonunu güncelleme
        transform.position += new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed;
    }
}
