using UnityEngine;
using UnityEngine.UI;

public class ShowHighScore : MonoBehaviour
{

    public Text highScoreText; 
    public int highScore;
   



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        Debug.Log("Highscore: " + highScore);
        highScoreText.text = "Highest: " + highScore.ToString();

    }

    // Update is called once per frame
    void Update()
    {

    }




}
