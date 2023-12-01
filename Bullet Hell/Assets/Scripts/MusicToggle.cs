using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    // Reference to the Audio Source component
    public AudioSource musicAudioSource;

    // Reference to the Toggle UI component
    public Toggle musicToggle;

    // Start is called before the first frame update
    void Start()
    {
        // Check if the PlayerPrefs has a stored value for the music state
        if (PlayerPrefs.HasKey("MusicOn"))
        {
            bool isMusicOn = PlayerPrefs.GetInt("MusicOn") == 1;
            musicToggle.isOn = isMusicOn;
            UpdateMusicState(isMusicOn);
        }
        else
        {
            // If no value is stored, default to music being on
            PlayerPrefs.SetInt("MusicOn", 1);
            PlayerPrefs.Save();
        }

        // Add listener to the toggle to respond to changes
        musicToggle.onValueChanged.AddListener(OnMusicToggleValueChanged);
    }

    // Function to handle toggle value changes
    void OnMusicToggleValueChanged(bool isOn)
    {
        UpdateMusicState(isOn);

        // Save the current music state in PlayerPrefs
        PlayerPrefs.SetInt("MusicOn", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    // Function to update the music state based on the toggle value
    void UpdateMusicState(bool isOn)
    {
        // If the toggle is on, play the music; otherwise, stop it
        if (isOn)
        {
            musicAudioSource.Play();
        }
        else
        {
            musicAudioSource.Stop();
        }
    }
}
