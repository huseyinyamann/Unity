using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public float rotateSpeed = 5f; // silahın dönme hızı
    private List<Transform> enemies = new List<Transform>();
    private Transform target;

    public bool look;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            enemies.Add(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            enemies.Remove(other.transform);
        }
    }

    private void Update()
    {
        // Hedef düşmanı belirle
        target = GetClosestEnemy();

        if (target != null && look)
        {
            // Düşmana doğru yönel ve dönme açısını düzelt
            transform.LookAt(target);
            transform.Rotate(0f, -90f, 0f);
        }
        else
        {
            // Hedef yoksa silahı varsayılan yönde tut
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    // En yakın düşmanı bul
    private Transform GetClosestEnemy()
    {
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.position);
            if (distance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }

        return closestEnemy;
    }
}
