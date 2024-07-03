using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour, IObserver
{
    public TMPro.TextMeshPro healthText;

    void Start()
    {
        PlayerHealth playerHealth = PlayerHealth.Instance;
        if (playerHealth != null)
        {
            playerHealth.RegisterObserver(this);
        }
    }

    void OnDestroy()
    {
        PlayerHealth playerHealth = PlayerHealth.Instance;
        if (playerHealth != null)
        {
            playerHealth.UnregisterObserver(this);
        }
    }

    public void UpdateHealth(int health)
    {
        healthText.text = health.ToString();
    }

}
