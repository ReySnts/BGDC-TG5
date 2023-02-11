using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    // variable for audio
    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource PlayerSource;
    [SerializeField] AudioSource ButtonSource;
    [SerializeField] AudioSource ShardSource;
    [SerializeField] AudioSource BGMSource;
    [SerializeField] AudioSource DoorSource;
    [SerializeField] AudioSource MainSource;
    [SerializeField] List<AudioClip> sfxClip = new List<AudioClip>();
    [SerializeField] List<AudioClip> playerClip = new List<AudioClip>();
    [SerializeField] List<AudioClip> bgmClip = new List<AudioClip>();

    // var for saving value
    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";

    // vars for checking player condition
    bool isWalking;
    bool isRunning;
    bool isOpen;
    bool notification;

    void Awake()
    {
        instance ??= this;
        if (instance != this) Destroy(gameObject);
        else
        {
            float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
            float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);
            mixer.SetFloat(SettingsMenu.MIXER_MUSIC, Mathf.Log10(musicVolume) * 20);
            mixer.SetFloat(SettingsMenu.MIXER_SFX, Mathf.Log10(sfxVolume) * 20);
        }
    }
    public void ChangeSceneBGM()
    {
        switch
        (
            SceneManager.GetActiveScene().name
        )
        {
            case "Menu":
                BGMSource.loop = true;
                BGMSource.clip = bgmClip[0];
                BGMSource.Play();
                break;
            case "InputName":
                BGMSource.Stop();
                BGMSource.loop = true;
                BGMSource.clip = bgmClip[1];
                BGMSource.Play();
                break;
            case "Level-1":
                BGMSource.Stop();
                MainSource.Stop();
                MainSource.loop = true;
                MainSource.clip = bgmClip[2];
                MainSource.Play();
                break;
            case "DialogueBeforeBoss":
                MainSource.Stop();
                BGMSource.Stop();
                BGMSource.loop = true;
                BGMSource.clip = bgmClip[1];
                BGMSource.Play();
                break;
            case "GoodEnding":
                BGMSource.Stop();
                BGMSource.loop = true;
                BGMSource.clip = bgmClip[5];
                BGMSource.Play();
                break;
            case "BadEnding":
                BGMSource.Stop();
                BGMSource.loop = false;
                BGMSource.clip = bgmClip[4];
                BGMSource.Play();
                break;
            default:
                BGMSource.Stop();
                break;
        }
    }
    void Start()
    {
        ChangeSceneBGM();
    }
    void Update()
    {
        if (!SFXSource.isPlaying) SFXSource.Play();
    }
    public void ShardSFX()
    {
        AudioClip clip = sfxClip[0];
        ShardSource.PlayOneShot(clip);
    }
    public void ButtonSFX()
    {
        AudioClip clip = sfxClip[1];
        ButtonSource.PlayOneShot(clip);
    }
    public void GameOver()
    {
        AudioClip clip = sfxClip[2];
        SFXSource.PlayOneShot(clip);
    }
    public void GoodEndingEffect()
    {
        AudioClip clip = sfxClip[3];
        SFXSource.PlayOneShot(clip);
    }
    public void NotifDoor()
    {
        AudioClip clip = sfxClip[4];
        DoorSource.PlayOneShot(clip);
    }
    public void DoorOpen()
    {
        AudioClip clip = sfxClip[5];
        DoorSource.PlayOneShot(clip);
    }
    public void DoorSFX()
    {
        if (PlayerPrefs.GetInt("SavedScore") == 7)
        {
            isOpen = false;
            notification = true;
            if (Input.GetKeyDown(KeyCode.F))
            {
                notification = false;
                isOpen = true;
            }
        }
        else
        {
            notification = false;
            isOpen = false;
        }
        if (!isOpen && notification && !DoorSource.isPlaying)
        {
            NotifDoor();
        }
        else if (isOpen && !notification && !DoorSource.isPlaying)
        {
            DoorSource.Stop();
            DoorOpen();
        }
    }
}