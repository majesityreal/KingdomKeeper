using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UISpritesAnimation : MonoBehaviour
{

    private bool activate = false;

    public float duration;

    [SerializeField] private Sprite[] sprites;

    private Image image;
    private int index = 0;
    private float timer = 0;

    void Start()
    {
        image = GetComponent<Image>();
        activate = false;
    }
    private void Update()
    {
        if (activate)
        {
            if ((timer += Time.deltaTime) >= (duration / sprites.Length))
            {
                timer = 0;
                image.sprite = sprites[index];
                // when animation ends
                if (++index >= sprites.Length)
                {
                    activate = false;
                    index = 0;
                    image.enabled = false;
                }
            }
        }

    }

    public void Activate ()
    {
        activate = true;
        timer = 0;
    }

    public void Deactivate()
    {
        image.enabled = true;
        activate = false;
        index = 0;
        image.sprite = sprites[index];
    }
}