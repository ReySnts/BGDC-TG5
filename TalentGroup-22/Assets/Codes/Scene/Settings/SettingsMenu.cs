using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
public class SettingsMenu : MonoBehaviour
{
    public static SettingsMenu objInstance = null;
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    public const string MIXER_MUSIC = "MusicVolume";
    public const string MIXER_SFX = "SFXVolume";
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
        #region Setup
        musicSlider = GameObject.Find("Slider BGM").GetComponent<Slider>();
        sfxSlider = GameObject.Find("Slider SFX").GetComponent<Slider>();
        musicSlider.value = PlayerPrefs.GetFloat
        (
            AudioManager.MUSIC_KEY, 
            AudioManager.defaultValue
        );
        sfxSlider.value = PlayerPrefs.GetFloat
        (
            AudioManager.SFX_KEY, 
            AudioManager.defaultValue
        );
        #endregion
    }
    public void SetMusicVolume()
    {
        mixer.SetFloat
        (
            MIXER_MUSIC, 
            Mathf.Log10(musicSlider.value) * AudioManager.multiplier
        );
    }
    public void SetSFXVolume()
    {
        mixer.SetFloat
        (
            MIXER_SFX, 
            Mathf.Log10(sfxSlider.value) * AudioManager.multiplier
        );
    }
    void OnDisable()
    {
        PlayerPrefs.SetFloat
        (
            AudioManager.MUSIC_KEY, 
            musicSlider.value
        );
        PlayerPrefs.SetFloat
        (
            AudioManager.SFX_KEY, 
            sfxSlider.value
        );
    }
}
