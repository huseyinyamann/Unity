using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float attackDelay = 2f;
    public float attackDistance = 3f;
    public float enemySpeed = 5f;

    private Transform playerTransform;
    private bool canAttack = true;

    void Start()
    {
        // Find the player object using the "Player" tag
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Rotate towards the player
        Vector3 direction = playerTransform.position - transform.position;

        // Check if the player is facing the opposite direction
        if (playerTransform.localScale.x < 0)
        {
            direction.x *= -1;
        }

        // Raycast to detect walls
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, 1f, LayerMask.GetMask("Wall"));
        if (hit.collider != null)
        {
            // Wall detected, change direction
            direction *= -1;
        }

        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;

        // Move towards the player while keeping a minimum distance
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer > attackDistance)
        {
            transform.position += transform.forward * Time.deltaTime * enemySpeed;
        }
        else if (distanceToPlayer <= attackDistance && canAttack)
        {
            // Attack the player if within attack distance and can attack
            StartCoroutine(AttackDelay());
            Debug.Log("Enemy attacked player!");
        }
    }


    IEnumerator AttackDelay()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }
}
