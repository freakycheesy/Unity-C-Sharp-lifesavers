using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingsScript : MonoBehaviour
{
    // Audio
    public Slider master_volumeSlider;
    public Slider music_volumeSlider;
    public Slider sfx_volumeSlider;
    public AudioMixer audioMixer;

    // Controls
    public Slider sensSlider;

    // Graphics
    public TMP_Dropdown graphicsQuality;

    void OnEnable() {
        // Audio
        master_volumeSlider.value = PlayerPrefs.GetFloat("master_volume");
        music_volumeSlider.value = PlayerPrefs.GetFloat("music_volume");
        sfx_volumeSlider.value = PlayerPrefs.GetFloat("sfx_volume");

        // Controls
        sensSlider.value = PlayerPrefs.GetFloat("sens");

        // Graphics
        graphicsQuality.value = PlayerPrefs.GetInt("quality");
    }
    void Update() {
        // Audio
        PlayerPrefs.SetFloat("master_volume", master_volumeSlider.value);
        audioMixer.SetFloat("master_volume", PlayerPrefs.GetFloat("master_volume"));
        PlayerPrefs.SetFloat("music_volume", music_volumeSlider.value);
        audioMixer.SetFloat("music_volume", PlayerPrefs.GetFloat("music_volume"));
        PlayerPrefs.SetFloat("sfx_volume", sfx_volumeSlider.value);
        audioMixer.SetFloat("sfx_volume", PlayerPrefs.GetFloat("sfx_volume"));
        // Controls
        PlayerPrefs.SetFloat("sens", sensSlider.value);
        // Graphics
        PlayerPrefs.SetInt("quality", graphicsQuality.value);
    }
}
