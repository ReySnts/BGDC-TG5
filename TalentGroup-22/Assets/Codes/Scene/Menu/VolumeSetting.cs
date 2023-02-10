using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
public class VolumeSetting : MonoBehaviour
{
    public static VolumeSetting objInstance = null;
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    public const string MIXER_MUSIC = "MusicVolume";
    public const string MIXER_SFX = "SFXVolume";
    void Awake()
    {
        if (objInstance == null) objInstance = this;
        else if (objInstance != this) Destroy(gameObject);
    }
    void OnEnable()
    {
        musicSlider = GameObject.Find("Slider BGM").GetComponent<Slider>();
        sfxSlider = GameObject.Find("Slider SFX").GetComponent<Slider>();
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(AudioManager.SFX_KEY, 1f);
    }
    public void SetMusicVolume()
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(musicSlider.value) * 20f);
    }
    public void SetSFXVolume()
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(sfxSlider.value) * 20f);
    }
    void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, musicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.SFX_KEY, sfxSlider.value);
    }
}
