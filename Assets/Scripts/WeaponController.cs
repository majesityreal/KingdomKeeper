using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public PlayerController p_controller;

    public SpriteRenderer weaponSprite;

    public float damage;

    // offset var from player, for sword like weapons
    public float offSet;

    public enum Weapon
    {
        SWORD = 0,
        BOW = 1,
        MAGIC = 2
    }

    Weapon currWeapon;

    private void Awake()
    {
        weaponSprite = GetComponent<SpriteRenderer>();

        // temporary controlling the start
        currWeapon = Weapon.SWORD;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        // initiate slashing if a sword
        if (currWeapon == Weapon.SWORD)
        {
            // TODO:
            // prevent player from turning
            p_controller.attacking = true;
            // place slash according to player direction - true = left, false = right
            if (p_controller.playerSprite.flipX == true)
            {
                weaponSprite.flipX = true;
                gameObject.transform.localPosition = new Vector2(- offSet, gameObject.transform.localPosition.y);
            }
            else
            {
                weaponSprite.flipX = false;
                gameObject.transform.localPosition = new Vector2(offSet, gameObject.transform.localPosition.y);
            }
            // load animation for slash - temporarily not doing this

            // chech enemies for collision, and then damage them
        }
    }

    private void OnDisable()
    {
        p_controller.attacking = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damageable enemyHealth = collision.gameObject.GetComponent<Damageable>();
        if (enemyHealth != null)
        {
            if (enemyHealth.gameObject.tag == "Player")
            {
                return;
            }
            enemyHealth.Damage(damage);
        }
    }

}
