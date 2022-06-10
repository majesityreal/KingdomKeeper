using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectCharManager : MonoBehaviour
{

    // selected character
    public enum PlayerClass {
        Knight, Archer, Wizard
    }
    public PlayerClass selectedClass;
    public int numberOfClasses;

    public Sprite[] classSprites;
    public Image sprite;
    public Text spriteText;

    private void Start()
    {
        selectedClass = PlayerClass.Knight;
    }

    public void RightCharSelect()
    {
        selectedClass++;
        if ((int)selectedClass >= numberOfClasses)
        {
            selectedClass = 0;
        }
        SetSprite();
    }

    public void LeftCharSelect()
    {
        if (selectedClass == 0)
        {
            selectedClass = selectedClass + numberOfClasses - 1;
        }
        else
        {
            selectedClass--;
        }
        SetSprite();
    }

    // called in both left/right button methods
    private void SetSprite()
    {
        sprite.sprite = classSprites[(int)selectedClass];
        switch ((int)selectedClass)
        {
            case 0:
                spriteText.text = "The Knight";
                break;
            case 1:
                spriteText.text = "The Archer";
                break;
            case 2:
                spriteText.text = "The Wizard";
                break;
            default:
                spriteText.text = "ERROR";
                break;
        }

    }

    public void StartGame()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.gameObject.SetActive(true);
        }
        // set the character to the selected character!!
        GameManager.Instance.playerClass = selectedClass;
        SceneManager.LoadScene("Level1");
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene("TitleScreen");
    }

}
