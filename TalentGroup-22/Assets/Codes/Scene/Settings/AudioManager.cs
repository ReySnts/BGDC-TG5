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

    string audioResourcesPath = "Audio/";
    string sFXResourcesPath;
    string bGMResourcesPath;

    // var for saving value
    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";
    public const float defaultValue = 1f;
    public const float multiplier = 20f;

    // vars for checking player condition
    bool isOpen;
    bool notification;

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        #region Setup
        sFXResourcesPath = audioResourcesPath + "SFX/";
        bGMResourcesPath = audioResourcesPath + "BGM/";
        mixer = Resources.Load<AudioMixer>(audioResourcesPath + "mixer");
        #endregion
        #region Find Source
        SFXSource = GameObject.Find("SFXSource").GetComponent<AudioSource>();
        PlayerWalkSource = GameObject.Find("PlayerWalkSource").GetComponent<AudioSource>();
        PlayerRunSource = GameObject.Find("PlayerRunSource").GetComponent<AudioSource>();
        ButtonSource = GameObject.Find("ButtonSource").GetComponent<AudioSource>();
        ShardSource = GameObject.Find("ShardSource").GetComponent<AudioSource>();
        BGMSource = GameObject.Find("BGMSourceMusic").GetComponent<AudioSource>();
        DoorSource = GameObject.Find("DoorSource").GetComponent<AudioSource>();
        MainSource = GameObject.Find("MainSource").GetComponent<AudioSource>();
        #endregion
        #region List SFX
        sfxClip.Clear();
        sfxClip.Add
        (
            Resources.Load<AudioClip>(sFXResourcesPath + "Crystal Get (New)")
        );
        sfxClip.Add
        (
            Resources.Load<AudioClip>(sFXResourcesPath + "Button Select")
        );
        sfxClip.Add
        (
            Resources.Load<AudioClip>(sFXResourcesPath + "Game over")
        );
        sfxClip.Add
        (
            Resources.Load<AudioClip>(bGMResourcesPath + "Ending (good)")
        );
        sfxClip.Add
        (
            Resources.Load<AudioClip>(sFXResourcesPath + "Notif")
        );
        sfxClip.Add
        (
            Resources.Load<AudioClip>(sFXResourcesPath + "Door_Open")
        );
        sfxClip.Add
        (
            Resources.Load<AudioClip>(sFXResourcesPath + "Damaged")
        );
        sfxClip.Add
        (
            Resources.Load<AudioClip>(sFXResourcesPath + "Crystal")
        );
        #endregion
        #region List BGM
        bgmClip.Clear();
        bgmClip.Add
        (
            Resources.Load<AudioClip>(bGMResourcesPath + "Main Menu")
        );
        bgmClip.Add
        (
            Resources.Load<AudioClip>(bGMResourcesPath + "Your Name BGM")
        );
        bgmClip.Add
        (
            Resources.Load<AudioClip>(bGMResourcesPath + "Stage 1 (No Monster)")
        );
        bgmClip.Add
        (
            Resources.Load<AudioClip>(bGMResourcesPath + "HER_1")
        );
        bgmClip.Add
        (
            Resources.Load<AudioClip>(bGMResourcesPath + "Ending (good)")
        );
        bgmClip.Add
        (
            Resources.Load<AudioClip>(bGMResourcesPath + "HER_2")
        );
        #endregion
        #region Load Volume
        float musicVolume = PlayerPrefs.GetFloat
        (
            MUSIC_KEY, 
            defaultValue
        );
        float sfxVolume = PlayerPrefs.GetFloat
        (
            SFX_KEY, 
            defaultValue
        );
        mixer.SetFloat
        (
            SettingsMenu.MIXER_MUSIC, 
            Mathf.Log10(musicVolume) * multiplier
        );
        mixer.SetFloat
        (
            SettingsMenu.MIXER_SFX, 
            Mathf.Log10(sfxVolume) * multiplier
        );
        #endregion
    }
    void Start()
    {
        PlayerWalk(false);
        PlayerRun(false);
        #region Change BGM
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
                BGMSource.clip = bgmClip[3];
                BGMSource.Play();
                break;
            case "GoodEnding":
                BGMSource.Stop();
                BGMSource.loop = true;
                BGMSource.clip = bgmClip[4];
                BGMSource.Play();
                break;
            case "BadEnding":
                BGMSource.Stop();
                BGMSource.loop = false;
                BGMSource.clip = bgmClip[5];
                BGMSource.Play();
                break;
            default:
                BGMSource.Stop();
                break;
        }
        #endregion
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
    public void ShardSlotSFX()
    {
        SFXSource?.PlayOneShot(sfxClip[7]);
    }
    #endregion
}