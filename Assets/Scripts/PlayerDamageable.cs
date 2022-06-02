using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageable : Damageable
{

    public BoxCollider2D playerCollider;

    private GameManager gameManager;

    public SpriteRenderer playerSprite;

    public Color playerDamageColor;
    public Color defaultColor;


    new void Start()
    {
        base.Start();
        if (playerCollider == null)
        {
            playerCollider = GetComponent<BoxCollider2D>();
        }
        gameManager = GameManager.Instance;
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

    public void Damage(float amount)
    {
        if (lastDamageTime + iframes > Time.time)
        {
            return;
        }
        gameManager.DamageHeartUI();
        currHealth -= amount;
        if (currHealth <= 0)
        {
            gameManager.GameOver();
            Destroy(gameObject);
        }
        else
        {
            lastDamageTime = Time.time;
            StartCoroutine(PlayerDamageColor());
            playerSprite.color = playerDamageColor;
        }

    }

    IEnumerator PlayerDamageColor()
    {
        yield return new WaitForSeconds(0.75f);
        playerSprite.color = defaultColor;

    }

}
