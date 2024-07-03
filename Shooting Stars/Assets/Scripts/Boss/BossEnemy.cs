using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    public GameObject projectilePrefab;
    public float rageHealthThreshold = 50f; 
    private bool isRaging = false;

    private Vector3 targetPosition;
    private bool isAtCenter = false;
    private bool shootDiagonals = false;

    public TMPro.TextMeshPro text;

    private void Start()
    {
        
        Health = 200;
        Speed = 2f;
        FireRate = 1f;
        SizeMultiplier = 2f;

        
        float cameraHeight = Camera.main.orthographicSize * 2;
        float cameraWidth = cameraHeight * Camera.main.aspect;
        float xPos = Camera.main.transform.position.x;
        float yPos = Camera.main.transform.position.y + cameraHeight / 2 + 1;
        transform.position = new Vector3(xPos, yPos, 0);

       
        targetPosition = Camera.main.transform.position;

        
        StartCoroutine(MoveToCenter());
    }

    private void Update()
    {
        if (Health <= rageHealthThreshold && !isRaging)
        {
            EnterRageMode();
        }
        text.text = Health.ToString();
    }

    private void EnterRageMode()
    {
        isRaging = true;
        FireRate /= 2;
        Speed *= 1.5f;
        
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(FireRate);
            ShootProjectiles();
            shootDiagonals = !shootDiagonals; 
        }
    }

    private void ShootProjectiles()
    {
        Vector2[] straightDirections = new Vector2[]
        {
            Vector2.down, 
            Vector2.up, 
            Vector2.left, 
            Vector2.right 
        };

        Vector2[] diagonalDirections = new Vector2[]
        {
            new Vector2(-1, -1).normalized, 
            new Vector2(1, -1).normalized, 
            new Vector2(-1, 1).normalized, 
            new Vector2(1, 1).normalized 
        };

        Vector2[] selectedDirections = shootDiagonals ? diagonalDirections : straightDirections;

        foreach (var direction in selectedDirections)
        {
            var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.transform.localScale *= SizeMultiplier;
            var directionalProjectile = projectile.GetComponent<DirectionalProjectile>();
            if (directionalProjectile != null)
            {
                directionalProjectile.Direction = direction;
            }
        }
    }

    private IEnumerator MoveToCenter()
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
            yield return null;
        }
        isAtCenter = true;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        EnemyInstanciator.Instance.IsFighting = false;
        this.gameObject.GetComponent<Collider2D>().enabled = true;
        EnemyInstanciator.Instance.IsFighting = true;
        StartCoroutine(MoveSideToSide());
        StartCoroutine(AttackRoutine()); 
    }

    private IEnumerator MoveSideToSide()
    {
        float cameraWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect;
        float leftBoundary = Camera.main.transform.position.x - cameraWidth / 2;
        float rightBoundary = Camera.main.transform.position.x + cameraWidth / 2;

        while (true)
        {
            while (transform.position.x < rightBoundary)
            {
                transform.position += Vector3.right * Speed * Time.deltaTime;
                yield return null;
            }
            while (transform.position.x > leftBoundary)
            {
                transform.position += Vector3.left * Speed * Time.deltaTime;
                yield return null;
            }
        }
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            TakeDamage();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            TakeDamage();
        }
    }

    public override void Die()
    {
        GameManager.instance.Victory();
        base.Die();
    }
}
