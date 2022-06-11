using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static SelectCharManager;

public class GameManager : MonoBehaviour
{

    public int currLevel; // keeps track of current level

    private bool gamePaused = false;

    public int startingHearts;
    public int pHearts;

    public int coins;
    public PlayerClass playerClass;

    [SerializeField]
    private GameObject[] classes;
    private GameObject player;

    public static GameManager Instance; // A static reference to the GameManager instance, singleton pattern used

    void Awake()
    {
        if (Instance == null) // If there is no instance already
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (Instance != this) // If there is already an instance and it's not `this` instance
        {
            // restarting the OG game manager
            Instance.Start();
            DestroyImmediate(gameObject);
        }
    }


    // this is called when reloading the game
    void Start()
    {
        pHearts = startingHearts;
        coins = 0;
        gamePaused = false;
        // this is done on Start(), which should only be when the scene loads
        ResetUI();

    }

    public bool GetPaused()
    {
        return gamePaused;
    }

    public void PauseGame()
    {
        gamePaused = true;
        // pull up pause menu
        UIManager.Instance.pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void UnpauseGame()
    {
        gamePaused = false;
        UIManager.Instance.pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void QuitToTitle()
    {
        gamePaused = false;
        Time.timeScale = 1.0f;
        UIManager.Instance.pauseMenu.SetActive(false);
        UIManager.Instance.gameObject.SetActive(false);
        SceneManager.LoadScene("TitleScreen");
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene("Level1");
    }

    // custom scene loader method, to instantiate the player object
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "CharacterSelect" || scene.name != "TitleScreen")
        {
            player = Instantiate(classes[(int)playerClass], new Vector3(0, 0, 0), Quaternion.identity);
        }
    }

    public void NextLevel()
    {
        currLevel++;
        SceneManager.LoadScene("Level" + currLevel);
    }

    // TODO - pull up a 'you died' screen, make restart button
    public void GameOver()
    {
        // TODO - add something with death here - maybe an ad to replay?
        QuitToTitle();
    }

    public void DamageHeartUI()
    {
        // doing this because of 0 array indexing
        pHearts--;
        UISpritesAnimation[] hearts = UIManager.Instance.hearts;
        // finds the heart in the array that needs to disappear
        foreach (UISpritesAnimation heart in hearts)
        {
            if (pHearts == heart.gameObject.transform.GetSiblingIndex())
            {
                // makes UI heart dissappear. activate means activate animation
                heart.Activate();
                return;
            }
        }


    }

    public void AddMoney(int value)
    {
        coins++;
        // update UI
        if (coins >= 100)
        {
            UIManager.Instance.coinText.text = "" + coins;
        }
        else if (coins >= 10)
        {
            UIManager.Instance.coinText.text = "0" + coins;
        }
        else
        {
            UIManager.Instance.coinText.text = "00" + coins;
        }

    }

    public void ResetUI()
    {
        if (UIManager.Instance == null)
        {
            return;
        }
        UISpritesAnimation[] hearts = UIManager.Instance.hearts;
        UIManager.Instance.coinText.text = "000";
        foreach (UISpritesAnimation heart in hearts)
        {
            heart.Deactivate();
        }
    }

}
