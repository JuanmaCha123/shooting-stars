using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Store : MonoBehaviour
{
    public bool LeftTurretActive = false;
    public bool RightTurretActive = false;
    public TMPro.TextMeshProUGUI points;
    public GameObject Turret;

    public static Store Instance;

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

    private void Update()
    {
        points.text = PlayerScore.Instance.Points.ToString();
    }

    public void AddTurret()
    {
        if (PlayerScore.Instance.Points >= 500)
        {
            if (LeftTurretActive == false)
            {
                GameObject NewTurret = Instantiate(Turret);
                NewTurret.GetComponent<Turret>().IsLeft = true;
                LeftTurretActive = true;
            }
            else if (LeftTurretActive == true)
            {
                if (RightTurretActive == false)
                {
                    GameObject NewTurret = Instantiate(Turret);
                    NewTurret.GetComponent<Turret>().IsRight = true;
                    NewTurret.GetComponent <Turret>().IsLeft = false;
                    RightTurretActive = true;
                }
                else if (RightTurretActive == true && LeftTurretActive == true)
                {
                    GameObject[] Turrets = GameObject.FindGameObjectsWithTag("Turret");
                    foreach (GameObject Turret in Turrets)
                    {
                        Turret.GetComponent<Turret>().Upgrade();
                    }
                }
            }
            PlayerScore.Instance.Points = PlayerScore.Instance.Points - 500;
        }
    }

    public void BuyShields()
    {
        if (PlayerScore.Instance.Points >= 200)
        {
            PlayerHealth.Instance.ActivateShield();
            PlayerScore.Instance.Points -= 200;
        }
    }

    public void BuyHealth()
    {
        if (PlayerScore.Instance.Points >= 100)
        {
            PlayerHealth.Instance.Heal();
            PlayerScore.Instance.Points -= 100;
        }
    }

}
