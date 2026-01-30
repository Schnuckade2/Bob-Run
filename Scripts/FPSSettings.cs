using UnityEngine;
using UnityEngine.UI;
using TMPro; // nur nötig, wenn du TMP-InputField benutzt

public class SettingsMenu : MonoBehaviour
{
    [Header("FPS Einstellung (Text-Eingabe)")]
    public TMP_InputField fpsInput; // oder InputField, je nach UI
    public Toggle vsyncToggle;

    [Header("Musiklautstärke")]
    public Slider musicVolumeSlider;
    public TextMeshProUGUI volumeText;

    [Header("Audioquelle")]
    public AudioSource musicSource;

    [Header("FPS Limits")]
    public int minFPS = 30;
    public int maxFPS = 240;

    private void Start()
    {
        // --- FPS laden ---
        int savedFPS = PlayerPrefs.GetInt("TargetFPS", 60);
        bool savedVSync = PlayerPrefs.GetInt("VSync", 0) == 1;

        if (fpsInput != null)
        {
            fpsInput.text = savedFPS.ToString();
            fpsInput.onEndEdit.AddListener(OnFPSTextChanged);
        }

        if (vsyncToggle != null)
        {
            vsyncToggle.isOn = savedVSync;
            vsyncToggle.onValueChanged.AddListener(OnVSyncToggleChanged);
        }

        ApplyFPS(savedFPS);
        ApplyVSync(savedVSync);

        // --- Musiklautstärke laden ---
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        if (musicSource != null)
        {
            musicSource.volume = savedVolume;
        }

        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = savedVolume;
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        }

        UpdateVolumeText(savedVolume);
    }

    // ---- FPS Eingabe ----
    private void OnFPSTextChanged(string input)
    {
        if (int.TryParse(input, out int fps))
        {
            fps = Mathf.Clamp(fps, minFPS, maxFPS);
            ApplyFPS(fps);
            PlayerPrefs.SetInt("TargetFPS", fps);
            PlayerPrefs.Save();
        }
        else
        {
            // Ungültige Eingabe zurücksetzen
            fpsInput.text = PlayerPrefs.GetInt("TargetFPS", 60).ToString();
        }
    }

    private void ApplyFPS(int fps)
    {
        if (QualitySettings.vSyncCount == 0)
        {
            Application.targetFrameRate = fps;
        }
    }

    private void OnVSyncToggleChanged(bool isOn)
    {
        ApplyVSync(isOn);
        PlayerPrefs.SetInt("VSync", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void ApplyVSync(bool isOn)
    {
        QualitySettings.vSyncCount = isOn ? 1 : 0;
        if (!isOn)
        {
            int target = PlayerPrefs.GetInt("TargetFPS", 60);
            Application.targetFrameRate = target;
        }
        else
        {
            Application.targetFrameRate = -1;
        }
    }

    // ---- Musiklautstärke ----
    private void OnMusicVolumeChanged(float value)
    {
        if (musicSource != null)
        {
            musicSource.volume = value;
        }

        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();

        UpdateVolumeText(value);
    }

    private void UpdateVolumeText(float value)
    {
        if (volumeText != null)
        {
            volumeText.text = Mathf.RoundToInt(value * 100f) + "%";
        }
    }
}
