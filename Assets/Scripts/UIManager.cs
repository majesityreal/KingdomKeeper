using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject pauseMenu;

    public UISpritesAnimation[] hearts;
    public Text coinText;

    public Button[] buttons;

    public static UIManager Instance;

    void Awake()
    {
        if (Instance == null) // If there is no instance already
        {
            hearts = FindObjectsOfType<UISpritesAnimation>();
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

    // Start is called before the first frame update
    void Start()
    {
        buttons = pauseMenu.GetComponentsInChildren<Button>();
        // resets each non persistent listener to prevent multiple calls
        foreach (Button b in buttons)
        {
            b.onClick.RemoveAllListeners();
        }
        // resume button
        buttons[0].onClick.AddListener(GameManager.Instance.UnpauseGame);
        // options button
        buttons[1].onClick.AddListener(GameManager.Instance.UnpauseGame);
        // quit button
        buttons[2].onClick.AddListener(GameManager.Instance.QuitToTitle);
    }

    // Update is called once per frame
    void Update()
    {
        // pause feature
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name != "TitleScreen")
            {
                GameManager.Instance.PauseGame();
            }
        }
    }



}
