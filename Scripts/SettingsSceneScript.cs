using UnityEngine;
using UnityEngine.SceneManagement;  
public class SettingsSceneScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Back2Menu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
