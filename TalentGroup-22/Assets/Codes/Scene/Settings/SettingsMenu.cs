using UnityEngine;
using UnityEngine.UI;
public class SettingsMenu : MonoBehaviour
{
    public static SettingsMenu objInstance = null;
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
        musicSlider.minValue = sfxSlider.minValue = -80f;
        musicSlider.maxValue = sfxSlider.maxValue = 20f;
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
        if (AudioManager.instance != null) 
        {
            AudioManager.instance.mixer.SetFloat
            (
                MIXER_MUSIC, 
                musicSlider.value
            );
        }
    }
    public void SetSFXVolume()
    {
        if (AudioManager.instance != null) 
        {
            AudioManager.instance.mixer.SetFloat
            (
                MIXER_SFX, 
                sfxSlider.value
            );
        }
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
