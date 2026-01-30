using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.InputSystem;


public class LogicScript : MonoBehaviour
{
    [Header("Textes")]
    public Text text;
    public Text scoreText;
    public Text gameOverScoretext;

    [Header("Logic")]
    public int lives = 10;
    public int count = 0;

    public PlayerScript player;
    public SpecialAbility ability;
    public GameObject gameOverScreen;
    public Image abilityReady;

    [Header("Pause Menu")]
    public InputAction pause;
    public GameObject pauseMenu;
    public bool isPaused = false;



    private void OnEnable()
    {
        pause = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/escape");
        pause.Enable();
        
    }





    void Start()
    {
        StartCoroutine(IncreaseScore());
    }



    void Update()
    {
        HandleHealth();
        HandleScore();
        AvailableAbility();
        PauseGame();

        if (player.isAlive == false)
        {
            gameOverScreen.SetActive(true);
            gameOverScoretext.text = "Your Score: " + count.ToString();
            NoteNewHighScore();
        }


    }

    void RemoveHealth(int damage)
    {
        lives -= damage;
    }

    void AddHealth(int heal)
    {
        lives += heal + 1;
    }

    void PauseGame()
    {
        if (pause.triggered)
        {
            if (Time.timeScale == 1f)
            {
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
                isPaused = true;
            }
            else
            {
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
                isPaused = false;
            }
        }
           
        
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        isPaused = false;
    }


    void HandleScore()
    {
        scoreText.text = count.ToString();

        if (count > 9)
        {
            // Default 160 Font
            // Default Position : Vector 2 (30, -17.3486)
            scoreText.fontSize = 100;
            scoreText.rectTransform.anchoredPosition = new Vector2(10, -47);
        }
        if (count > 99)
        {
            scoreText.rectTransform.anchoredPosition = new Vector2(-11, -48);
        }
    }


    void HandleHealth()
    {
        if (player.isTouchingExplosion)
        {
            RemoveHealth(2);
            text.text = lives.ToString();
            player.isTouchingExplosion = false;
        }
        else if (player.isTouchingSpike)
        {
            RemoveHealth(1);
            text.text = lives.ToString();
            player.isTouchingSpike = false;
        }
        if (lives <= 0)
        {
            text.text = "0";
            gameOverScreen.SetActive(true);
            player.isAlive = false;
        }
        if (lives < 10)
        {
            text.rectTransform.anchoredPosition = new Vector2(-228, 140);
        }
        else if (lives > 9)
        {
            text.rectTransform.anchoredPosition = new Vector2(-277, 140);
        }


        if (player.icRegene)
        {
            AddHealth(1);
            text.text = lives.ToString();
            player.icRegene = false;
        }
        
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        UnpauseGame();
        SceneManager.LoadScene("MenuScene");
    }


    IEnumerator IncreaseScore()
    {
        while (player.isAlive)
        {
            count++;
            Debug.Log("Score: " + count);
            yield return new WaitForSeconds(1); // warte 1 Sekunde
        }
    }

    void AvailableAbility()
    {
        abilityReady.enabled = !ability.isOnCooldown;
    }

    void NoteNewHighScore()
    {
        if (count > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", count);
            PlayerPrefs.Save(); // Speichert explizit – optional, wird auch automatisch gemacht
        }
    }


    public void QuitGame()
    {
        UnpauseGame();
        Debug.Log("Spiel wird beendet");
        Application.Quit();
    }







}
