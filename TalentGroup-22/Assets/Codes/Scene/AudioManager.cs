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
    [SerializeField] AudioSource PlayerWalkSource;
    [SerializeField] AudioSource PlayerRunSource;
    [SerializeField] AudioSource ButtonSource;
    [SerializeField] AudioSource ShardSource;
    [SerializeField] AudioSource BGMSource;
    [SerializeField] AudioSource DoorSource;
    [SerializeField] AudioSource MainSource;

    [SerializeField] List<AudioClip> sfxClip = new List<AudioClip>();
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
            float musicVolume = PlayerPrefs.GetFloat
            (
                MUSIC_KEY, 
                1f
            );
            float sfxVolume = PlayerPrefs.GetFloat
            (
                SFX_KEY, 
                1f
            );
            mixer.SetFloat
            (
                SettingsMenu.MIXER_MUSIC, 
                Mathf.Log10(musicVolume) * 20f
            );
            mixer.SetFloat
            (
                SettingsMenu.MIXER_SFX, 
                Mathf.Log10(sfxVolume) * 20f
            );
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
        PlayerWalk(false);
        PlayerRun(false);
    }
    void Update()
    {
        if (!SFXSource.isPlaying) SFXSource?.Play();
    }
    public void PlayerWalk(bool isEnabled)
    {
        if (PlayerWalkSource != null) PlayerWalkSource.enabled = isEnabled;
    }
    public void PlayerRun(bool isEnabled)
    {
        if (PlayerRunSource != null) PlayerRunSource.enabled = isEnabled;
    }
    #region SFX
    public void ShardCollectSFX()
    {
        ShardSource?.PlayOneShot(sfxClip[0]);
    }
    public void ButtonSFX()
    {
        ButtonSource?.PlayOneShot(sfxClip[1]);
    }
    public void GameOver()
    {
        SFXSource?.Stop();
        PlayerWalk(false);
        PlayerRun(false);
        SFXSource?.PlayOneShot(sfxClip[2]);
    }
    public void GoodEndingEffect()
    {
        SFXSource?.PlayOneShot(sfxClip[3]);
    }
    public void NotifDoor()
    {
        DoorSource?.PlayOneShot(sfxClip[4]);
    }
    public void DoorOpen()
    {
        DoorSource?.PlayOneShot(sfxClip[5]);
    }
    public void DamagePlayer()
    {
        SFXSource?.PlayOneShot(sfxClip[6]);
    }
    #endregion
    public void DoorSFX()
    {
        if 
        (
            PlayerPrefs.GetInt("SavedScore") == 7
        )
        {
            isOpen = false;
            notification = true;
            if 
            (
                Input.GetKeyDown(KeyCode.F)
            )
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
            DoorSource?.Stop();
            DoorOpen();
        }
    }
}