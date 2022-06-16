using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageable : Damageable
{

    public BoxCollider2D playerCollider;

    public SpriteRenderer playerSprite;

    public Color playerDamageColor;
    public Color defaultColor;


    new void Start()
    {
        lastDamageTime = Time.time;
        currHealth = GameManager.Instance.pHearts;
        if (playerCollider == null)
        {
            playerCollider = GetComponent<BoxCollider2D>();
        }
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                animator = GetComponentInChildren<Animator>();
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Heal(1);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Damage(2);
        }
    }

    // this handles when the player is hit by other objects
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damageable enemyHealth = collision.gameObject.GetComponent<Damageable>();
        if (enemyHealth != null)
        {
            // ensures that cannot damage player while playing death animation
            if (!enemyHealth.isDying)
            {
                Damage(enemyHealth.contactDamage);
            }
        }
    }

    new public void Damage(float amount)
    {
        if (lastDamageTime + iframes > Time.time)
        {
            return;
        }
        AudioManager.Instance.Play("PlayerHurt");
        for (int i = 0; i < amount; i++)
        {
            GameManager.Instance.DamageHeartUI();
        }
        currHealth -= amount;
        if (currHealth <= 0)
        {
            GameManager.Instance.GameOver();
            Destroy(gameObject);
        }
        else
        {
            lastDamageTime = Time.time;
            animator.SetTrigger("Hurt");
            StartCoroutine(PlayerDamageColor());
            playerSprite.color = playerDamageColor;
        }

    }

    public void Heal(float amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameManager.Instance.AddHeartUI();
        }
        currHealth += amount;
    }

    IEnumerator PlayerDamageColor()
    {
        yield return new WaitForSeconds(0.75f);
        playerSprite.color = defaultColor;

    }

}
