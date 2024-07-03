using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Health = 3;
    public float Speed = 1;
    public float FireRate = 1;
    public float SizeMultiplier = 1;

    public GameObject FirePosition;
    public GameObject Projectile;

    private float fireTimer;
    private IMovementStrategy movementStrategy;


    public int PointValue = 10;

    public void Start()
    {
        ChooseMovementStrategy();
        fireTimer = FireRate;
    }

    void Update()
    {
        movementStrategy.Move(this);
        HandleShooting();
    }

    private void ChooseMovementStrategy()
    {
        int strategyIndex = Random.Range(0, 3);
        switch (strategyIndex)
        {
            case 0:
                movementStrategy = new NormalMovement();
                break;
            case 1:
                movementStrategy = new ZigZagMovement();
                break;
            case 2:
                movementStrategy = new FighterMovement();
                break;
        }
    }

    private void HandleShooting()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0)
        {
            Shoot();
            fireTimer = FireRate;
        }
    }

    public virtual void Shoot()
    {
        GameObject projectileInstance = Instantiate(Projectile, FirePosition.transform.position, Quaternion.identity);
        projectileInstance.transform.localScale *= SizeMultiplier;
    }

    public void TakeDamage()
    {
        Health--;
        if (Health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        PlayerScore.Instance.AddScore(PointValue);
        Destroy(this.gameObject);
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            TakeDamage();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Die();
        }
    }
}
