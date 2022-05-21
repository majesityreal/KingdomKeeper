using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBrain : MonoBehaviour
{
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void Dissappear()
    {
        // alpha to 0
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
    }

    public void Reappear()
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
    }

}
