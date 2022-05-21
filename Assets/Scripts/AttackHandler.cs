using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    // used to reset cooldown
    public float attackCooldown;

    // used to count down until next attack
    public float cooldownCounter;

    public bool attacking;

    public GameObject weapon;

    // Start is called before the first frame update
    void Start()
    {
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Attack
        if (Input.GetKeyDown(KeyCode.J) && cooldownCounter == 0f && !attacking)
        {
            cooldownCounter = attackCooldown;
            weapon.SetActive(true);
            attacking = true;
            // attack
        }
        if (cooldownCounter > 0f && attacking)
        {
            cooldownCounter -= Time.deltaTime;
        }
        else
        {
            cooldownCounter = 0f;
            attacking = false;
            weapon.SetActive(false);
        }
    }
}
