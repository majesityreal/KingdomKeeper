using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private bool gamePaused;

    public int startingHearts;
    public int pHearts;

    public UISpritesAnimation[] hearts;

    public Text coinText;
    private int coins;

    public static GameManager Instance; // A static reference to the GameManager instance, singleton pattern used

    void Awake()
    {
        if (Instance == null) // If there is no instance already
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this) // If there is already an instance and it's not `this` instance
        {
            // restarting the OG game manager
            Instance.Start();
            Destroy(gameObject);
        }
    }


    // this is called when reloading the game
    void Start()
    {
        pHearts = startingHearts;
        coins = 0;
        hearts = FindObjectsOfType<UISpritesAnimation>();
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
            Debug.Log(pHearts);
            if (pHearts == heart.gameObject.transform.GetSiblingIndex())
            {
                Debug.Log("Thjis happened");
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
