using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int currLevel; // keeps track of current level

    private bool gamePaused;

    public int startingHearts;
    public int pHearts;

    public GameObject ingameUI;

    public UISpritesAnimation[] hearts;

    public Text coinText;
    private int coins;

    public static GameManager Instance; // A static reference to the GameManager instance, singleton pattern used

    void Awake()
    {
        if (Instance == null) // If there is no instance already
        {
            DontDestroyOnLoad(gameObject);
            ingameUI = GameObject.FindGameObjectWithTag("Canvas");
            DontDestroyOnLoad(ingameUI);
            hearts = FindObjectsOfType<UISpritesAnimation>();
            Instance = this;
        }
        else if (Instance != this) // If there is already an instance and it's not `this` instance
        {
            // restarting the OG game manager
            Instance.Start();
            Destroy(gameObject);
            Destroy(ingameUI);
        }
    }


    // this is called when reloading the game
    void Start()
    {
        pHearts = startingHearts;
        coins = 0;
        coinText = FindObjectOfType<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // pause feature
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public bool GetPaused()
    {
        return gamePaused;
    }

    public void PauseGame()
    {
        gamePaused = true;
    }

    public void UnpauseGame()
    {
        gamePaused = false;
    }

    public void NextLevel()
    {
        currLevel++;
        SceneManager.LoadScene("Level" + currLevel);
    }

    // TODO - pull up a 'you died' screen, make restart button
    public void GameOver()
    {
        Debug.Log("It's over!");
        SceneManager.LoadScene("TitleScreen");
    }

    public void DamageHeartUI()
    {
        // doing this because of 0 array indexing
        pHearts--;
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
            coinText.text = "" + coins;
        }
        else if (coins >= 10)
        {
            coinText.text = "0" + coins;
        }
        else
        {
            coinText.text = "00" + coins;
        }

    }

}
