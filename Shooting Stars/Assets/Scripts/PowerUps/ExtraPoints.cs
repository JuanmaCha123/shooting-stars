using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraPoints : PowerUp
{
    public int Amount;
    public TMPro.TextMeshPro text;

    public override void Start()
    {
        base.Start();
        AssignRandomAmount();
        SetAmountOnScreen(Amount);
    }

    void AssignRandomAmount()
    {
        int[] possibleAmounts = { 30, 50, 100, 200 };
        int randomIndex = Random.Range(0, possibleAmounts.Length);
        Amount = possibleAmounts[randomIndex];
    }

    void SetAmountOnScreen(int amount)
    {
        text.text = "+" + amount.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerScore>().AddScore(Amount);
            Destroy(this.gameObject);
        }
    }
}
