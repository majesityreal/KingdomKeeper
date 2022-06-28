using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject pauseMenu;

    public GameObject heartPrefab;
    public UISpritesAnimation[] hearts;
    public Text coinText;

    public Button[] buttons;

    public static UIManager Instance;

    public Slider volumeSlider;

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
        // quit button
        buttons[1].onClick.AddListener(GameManager.Instance.QuitToTitle);

        AudioManager.Instance.audioMixer.GetFloat("Volume", out float val);
        volumeSlider.value = val;

        for (int i = 3; i < hearts.Length; i++)
        {
            if (GameManager.Instance.pHearts <= i)
            {
                hearts[i].Activate();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // pause feature
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name != "TitleScreen" && SceneManager.GetActiveScene().name != "CharacterSelect")
            {
                GameManager.Instance.PauseGame();
            }
        }
    }

    public void AddHeart()
    {
        GameObject heart = Instantiate(heartPrefab, gameObject.transform.GetChild(1));
        heart.transform.localPosition = new Vector2(-126 + (GameManager.Instance.pHearts * 62), 11f);
        UISpritesAnimation[] temp = new UISpritesAnimation[hearts.Length + 1];
        // copying old stuff into new array
        for (int i = 0; i < hearts.Length; i++)
        {
            temp[i] = hearts[i];
        }
        // adding final heart
        temp[hearts.Length] = heart.GetComponent<UISpritesAnimation>();
        hearts = temp;
    }

}
