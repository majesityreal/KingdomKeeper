using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class TitleScreenManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        if (!AudioManager.Instance.isPlaying("TitleTheme"))
        {
            AudioManager.Instance.Play("TitleTheme");
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene("CharacterSelect");

    }

    public void StartGame()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.gameObject.SetActive(true);
        }
    }

    public void Quit()
    {
        // anything else needed before quit goes here
        Application.Quit();
    }



}
