using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float speed = 10f; // Dönüş hızı

    void Update()
    {
        transform.Rotate(0f, 0f, speed * Time.deltaTime); // Z ekseninde döndürme işlemi
    }
}
