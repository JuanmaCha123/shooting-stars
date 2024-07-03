using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject projectilePrefab;  // Prefab del proyectil
    public Transform firePoint;          // Punto de origen del proyectil
    public float fireRate = 3f;          // Intervalo de tiempo entre disparos
    public int health = 2;               // Salud de la torreta

    private float fireTimer = 0f;

    public bool IsLeft = false;
    public bool IsRight = false;
    public GameObject player;

    void Start()
    {
        fireTimer = fireRate;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            Fire();
            fireTimer = fireRate;
        }

        if (player != null)
        {
            Vector3 playerPosition = player.transform.position;
            if (IsLeft)
            {
                transform.position = new Vector3(playerPosition.x - 2, playerPosition.y, transform.position.z);
            }
            else if (IsRight)
            {
                transform.position = new Vector3(playerPosition.x + 2, playerPosition.y, transform.position.z);
            }
        }

    }

    void Fire()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            projectile.transform.localScale *= 0.5f;  // Redimensionar el proyectil
        }
    }

    public void Upgrade()
    {
        fireRate = Mathf.Max(0.1f, fireRate * 0.5f);  // Reducir el FireRate, no permitiendo que sea menor a 0.5
        health += 1;
    }

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (IsLeft == true)
        {
            Store.Instance.LeftTurretActive = false;
        }
        if (IsRight == true)
        {
            Store.Instance.RightTurretActive = false;
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            TakeDamage();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }
}
