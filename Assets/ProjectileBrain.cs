using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBrain : MonoBehaviour
{
    // set X component to + or - to control the direction. Can also be used to control speed as well, not advised to do so however
    public Vector2 movementDirection;

    public float damage;
    public float speed;
    public int pierce;

    public bool hasGravity;
    public float gravityScale;

    // Start is called before the first frame update
    void Start()
    {
        if (hasGravity)
        {
            movementDirection.y = 0.25f;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (hasGravity)
        {
            movementDirection.y += gravityScale * Time.deltaTime;
        }

        gameObject.transform.Translate(new Vector2(movementDirection.x * speed, movementDirection.y) * Time.deltaTime);

        if (gameObject.transform.position.y <= -2.5f)
        {
            Destroy(gameObject);
        }
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
            // pierce handling, goes through X amount of enemies before getting destroyed
            pierce -= 1;
            if (pierce  <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetDirection()
    {

    }

}
