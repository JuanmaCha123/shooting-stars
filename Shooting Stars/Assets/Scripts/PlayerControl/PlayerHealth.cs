using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;
    public bool IsShielded = false;
    public GameObject ShieldSign;

    public static PlayerHealth Instance;

    private List<IObserver> observers = new List<IObserver>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage()
    {
        health--;
        NotifyObservers();
        if (health <= 0)
        {
            GameOver();
        }
    }

    public void Heal()
    {
        health++;
        NotifyObservers();
    }

    public void ActivateShield()
    {
        StartCoroutine(ShieldCoroutine());
    }

    // Corrutina para manejar el temporizador del escudo
    private IEnumerator ShieldCoroutine()
    {
        IsShielded = true;
        ShieldSign.SetActive(true);
        yield return new WaitForSeconds(5);
        ShieldSign.SetActive(false);
        IsShielded = false;
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        GameManager.instance.GameOver();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            if (IsShielded == false)
            {
                TakeDamage();
            }
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (IsShielded == false)
            {
                TakeDamage();
            }
            Destroy(collision.gameObject);
        }
    }

    public void RegisterObserver(IObserver observer)
    {
        observers.Add(observer);
        observer.UpdateHealth(health);
    }

    public void UnregisterObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    private void NotifyObservers()
    {
        foreach (IObserver observer in observers)
        {
            observer.UpdateHealth(health);
        }
    }
}
