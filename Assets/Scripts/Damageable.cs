﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{

    public float maxHealth = 100f;
    public float currHealth;

    public float contactDamage;

    // invincibility frames after being damaged
    public float iframes;
    protected float lastDamageTime; // keeps track of the last time was damaged

    public bool isDying; // used to check for dying, so logic can be handlded appropriately (no contact damage)
    public float timeUntilDeath; // the time this entity remains until death (destruction)

    public Animator animator;

    [SerializeField]
    private GameObject coinPrefab;

    // Start is called before the first frame update
    protected void Start()
    {
        currHealth = maxHealth;
        lastDamageTime = Time.time;
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                animator = GetComponentInChildren<Animator>();
            }
        }
    }

    public float GetHealth()
    {
        return currHealth;
    }

    public void SetHealth(float amount)
    {
        currHealth = amount;
    }

    public void SetMaxHealth(float amount)
    {
        maxHealth = amount;
    }

    // damages this GO by float amount, kills if necessary
    public void Damage(float amount)
    {
        if (lastDamageTime + iframes > Time.time)
        {
            return;
        }
        currHealth -= amount;
        if (currHealth <= 0)
        {
            StartCoroutine(Die());
        }
        else
        {
            lastDamageTime = Time.time;
            Animate_Hurt();
        }
    }

    // handles the death of this game object
    IEnumerator Die()
    {
        if (animator != null)
        {
            animator.SetTrigger("Die");
            animator.SetBool("isDying", true);

        }
        isDying = true;
        AudioManager.Instance.Play("EnemyDie");
        yield return new WaitForSeconds(timeUntilDeath);
        // drop coin
        if (coinPrefab != null)
        {
            GameObject coin = Instantiate(coinPrefab, gameObject.transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
        yield return null;
    }

    // Animations:

    protected void Animate_Hurt()
    {
        if (animator != null)
        {
            animator.SetTrigger("Hurt");
        }
    }

}
