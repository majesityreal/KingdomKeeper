using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseCanvasBrain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PurchaseHeart(int cost)
    {
        if (GameManager.Instance.coins > cost)
        {
            // TODO - play purchase sound
            GameManager.Instance.AddMoney(-cost);
            FindObjectOfType<PlayerDamageable>().Heal(1);
        }
        else
        {
            // play NULL sound, cannot buy sound
        }
    }

    public void PurchaseSpeed(int cost)
    {
        if (GameManager.Instance.coins > cost)
        {
            // TODO - play purchase sound
            GameManager.Instance.AddMoney(-cost);
            FindObjectOfType<PlayerController>().movementSpeed += 0.75f;
        }
        else
        {
            // play NULL sound, cannot buy sound
        }
    }
}
