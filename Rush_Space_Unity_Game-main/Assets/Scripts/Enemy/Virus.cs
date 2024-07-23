using System.Threading.Tasks;
using UnityEngine;

public class Virus : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float damagePerSecond = 10f;

    public GameObject particless;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        // Saniye başına verilen hasarı hesapla ve canı azalt
        float damage = damagePerSecond * Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        // Düşman ölünce yapılacaklar
        Instantiate(particless,transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
