﻿using System.Collections;
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
            ResetGame();
        }
        else if (Instance != this) // If there is already an instance and it's not `this` instance
        {
            DestroyImmediate(gameObject);
        }
    }

    void Update()
    {
        // GET RID OF THIS BEFORE LAUNCHING
        if (Input.GetKeyDown(KeyCode.P))
        {
            NextLevel();
        }
    }

    public void ResetGame()
    {
        pHearts = startingHearts;
        coins = 0;
        gamePaused = false;
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
        currLevel = 0;
        Time.timeScale = 1.0f;
        UIManager.Instance.pauseMenu.SetActive(false);
        UIManager.Instance.gameObject.SetActive(false);
        SceneManager.LoadScene("TitleScreen");
    }

    public void LoadFirstLevel()
    {
        currLevel = 1;
        SceneManager.LoadScene("Level1");
        ResetGame();
    }

    // custom scene loader method, to instantiate the player object
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "CharacterSelect" && scene.name != "TitleScreen")
        {
            // classes[int] stores GO's for each player class
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
        currLevel = 0;
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

    public void AddHeartUI()
    {
        pHearts++;
        // adds new heart to the canvas if we go over
        if (pHearts > UIManager.Instance.hearts.Length)
        {
            pHearts--;
            UIManager.Instance.AddHeart();
            pHearts++;
        }
        else
        {
            UISpritesAnimation[] hearts = UIManager.Instance.hearts;
            // finds the heart in the array that needs to disappear
            foreach (UISpritesAnimation heart in hearts)
            {
                // we subtract 1 because we previously added 1 to pHearts
                if (pHearts - 1 == heart.gameObject.transform.GetSiblingIndex())
                {
                    heart.Deactivate();
                    // gotta do this after bc of the shifting index
                    return;
                }
            }
        }
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }



    public void AddMoney(int value)
    {
        coins += value;
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
