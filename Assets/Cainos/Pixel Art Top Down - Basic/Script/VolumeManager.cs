using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public Slider musicSlider;
    public AudioSource audioSource;

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);

        if (musicSlider != null)
        {
            musicSlider.value = savedVolume;
            musicSlider.onValueChanged.AddListener(SetVolume);
        }

        if (audioSource != null)
        {
            audioSource.volume = savedVolume;
        }
    }

    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
            PlayerPrefs.SetFloat("MusicVolume", volume);
        }
    }
}
