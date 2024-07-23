using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller2 : MonoBehaviour
{
    public float moveSpeed = 5f; // Hareket hızı
    private Rigidbody2D rb;

    private Vector2 movement;

    //PLAYER HEALTH
    public float health = 100;

    // Joystick referansı
    public Joystick joystick;
    public bool isEmulator = false;

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
    }

    // FixedUpdate, fiziksel hesaplamaları yapmak için kullanılır
    void FixedUpdate()
    {
        // Rigidbody'yi hareket ettirin
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}