using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public PlayerController p_controller;

    public SpriteRenderer weaponSprite;

    public float damage;
    public bool doesCollisionDamage;

    // offset var from player, for sword like weapons
    public float offSet;

    public enum Weapon
    {
        SWORD = 0,
        BOW = 1,
        MAGIC = 2
    }

    public Weapon currWeapon;

    public GameObject arrow;

    private void Awake()
    {
        weaponSprite = GetComponent<SpriteRenderer>();
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
        }
        else if (currWeapon == Weapon.BOW || currWeapon == Weapon.MAGIC)
        {
            p_controller.attacking = true;
            GameObject shotArrow = Instantiate(arrow, gameObject.transform.position, Quaternion.identity);
            if (p_controller.playerSprite.flipX == true)
            {
                weaponSprite.flipX = true;
                shotArrow.GetComponent<ProjectileBrain>().SetDirection(-1);
                gameObject.transform.localPosition = new Vector2(-offSet, gameObject.transform.localPosition.y);
            }
            else
            {
                weaponSprite.flipX = false;
                shotArrow.GetComponent<ProjectileBrain>().SetDirection(1);
                gameObject.transform.localPosition = new Vector2(offSet, gameObject.transform.localPosition.y);
            }
        }
    }

    private void OnDisable()
    {
        p_controller.attacking = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damageable enemyHealth = collision.gameObject.GetComponent<Damageable>();
        if (enemyHealth != null && doesCollisionDamage)
        {
            if (enemyHealth.gameObject.tag == "Player")
            {
                return;
            }
            enemyHealth.Damage(damage);
        }
    }

}
