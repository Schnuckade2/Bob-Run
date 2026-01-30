using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    [Header("Musik für diese Szene")]
    public AudioClip sceneTrack;
    public bool loop = true;

    private void Start()
    {
        if (AudioManager.Instance != null && sceneTrack != null)
        {
            AudioManager.Instance.PlayMusic(sceneTrack, loop);
        }
    }
}
