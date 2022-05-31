using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private bool gamePaused;

    public int startingHearts;
    public int pHearts;

    public HeartBrain[] hearts;

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
            Destroy(gameObject);
        }
    }


    // this is called when reloading the game
    void Start()
    {
        pHearts = startingHearts;
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
    }

    public void DamageHeartUI()
    {
        pHearts--;

        // makes UI heart dissappear
        hearts[pHearts].Dissappear();

    }

}
