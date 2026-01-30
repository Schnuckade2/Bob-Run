using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Setup")]
    public AudioSource musicSource;
    public string sliderTag = "MusicSlider"; // Gib deinem Slider diesen Tag oder lass es auf Standard

    private void Awake()
    {
        // Singleton Logik
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Event abonnieren: Wird aufgerufen, wenn eine neue Szene geladen wird
        SceneManager.sceneLoaded += OnSceneLoaded;

        LoadSettings();
    }

    private void OnDestroy()
    {
        // Wichtig: Event wieder abmelden, um Fehler zu vermeiden
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Diese Methode läuft jedes Mal, wenn eine Szene neu geladen wird
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindAndSetupSlider();
    }

    private void FindAndSetupSlider()
    {
        // Wir nutzen jetzt die modernere Methode, um die Warnung zu entfernen
        Slider foundSlider = GameObject.FindFirstObjectByType<Slider>();

        if (foundSlider != null)
        {
            // Aktuellen Wert setzen
            foundSlider.value = GetVolume();

            // Alten Listener entfernen und neuen hinzufügen
            foundSlider.onValueChanged.RemoveAllListeners();
            foundSlider.onValueChanged.AddListener(delegate { SetVolume(foundSlider.value); });

            Debug.Log("AudioManager: Slider gefunden und verbunden.");
        }
    }

    public void PlayMusic(AudioClip newTrack, bool loop = true)
    {
        if (musicSource == null) return;
        if (musicSource.clip == newTrack) return;

        musicSource.clip = newTrack;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void SetVolume(float value)
    {
        if (musicSource != null)
            musicSource.volume = value;

        PlayerPrefs.SetFloat("MusicVolume", value);
        // Wir speichern nicht bei jeder kleinen Bewegung (Performance), 
        // Unity speichert automatisch beim Beenden oder via PlayerPrefs.Save()
    }

    public float GetVolume()
    {
        return PlayerPrefs.GetFloat("MusicVolume", 1f);
    }

    private void LoadSettings()
    {
        float savedVolume = GetVolume();
        if (musicSource != null)
            musicSource.volume = savedVolume;
    }
}