using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
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
    [SerializeField] List<AudioClip> sfxClip = new List<AudioClip>();
    [SerializeField] List<AudioClip> playerClip = new List<AudioClip>();
    [SerializeField] List<AudioClip> bgmClip = new List<AudioClip>();

    // vars for checking which scene has been loaded at present moment
    private string lastScene;
    private string currentScene;

    // var for saving value
    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";

    // vars for checking player condition
    private bool isWalking;
    private bool isRunning;
    private bool isOpen;
    private bool notification;
    void LoadVolume()
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);
        mixer.SetFloat(VolumeSetting.MIXER_MUSIC, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(VolumeSetting.MIXER_SFX, Mathf.Log10(sfxVolume) * 20);
    }

    private void Awake()
    {
        // getting the scenename
        lastScene = SceneManager.GetActiveScene().name;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        LoadVolume();
    }
    private void Update()
    {
        currentScene = SceneManager.GetActiveScene().name;
        /**
        if(currentScene != lastScene)
        {
            lastScene = currentScene;
            ChangeBGM();
        }
        */
        if (!SFXSource.isPlaying)
        {
            SFXSource.Play();
        }
    }
    public void ButtonSFX()
    {
        AudioClip clip = sfxClip[1];
        ButtonSource.PlayOneShot(clip);
    }
    public void ShardSFX()
    {
         AudioClip clip = sfxClip[0];
         ShardSource.PlayOneShot(clip);
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
    public void ChangeBGM()
    {
        if (lastScene == "Menu")
        {
            BGMSource.loop = true;
            BGMSource.clip = bgmClip[0];
            BGMSource.Play();
        }
        else if (lastScene == "NaratorScreen")
        { 
            BGMSource.Stop();
            BGMSource.loop = true;
            BGMSource.clip = bgmClip[1];
            BGMSource.Play();
            DontDestroyOnLoad(gameObject);
        } 
        else if (lastScene == "InputName")
        {
            BGMSource.Pause();
            // BGMSource.loop = true;
            // BGMSource.clip = bgmClip[1];
            BGMSource.UnPause();
        }
        else if (lastScene == "Level-1")
        {
            BGMSource.Stop();
            BGMSource.loop = true;
            BGMSource.clip = bgmClip[2];
            BGMSource.Play();
        } 
        else BGMSource.Stop();
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
}