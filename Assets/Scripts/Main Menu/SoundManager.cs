using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource musicAudioSource;
    public AudioSource vfxAudioSource;

    public AudioClip musicClip;
    public AudioClip bossAudioClip;
    public AudioClip musicSFX;
    public AudioClip beingHitClip;
    public AudioClip shootSFX;
    public AudioClip laserSFX;
    public AudioClip explosionSFX;
    public AudioClip warningSFX;

    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Start()
    {

        musicAudioSource.clip = musicClip;
        musicAudioSource.loop = true;
        musicAudioSource.Play();

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
        }
    }
    public void changeToBossAudioClip()
    {
        musicAudioSource.clip = bossAudioClip;
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }
    public void changeToNormalAudioClip()
    {
        musicAudioSource.clip = musicClip;
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }
    public void playSFX()
    {
        vfxAudioSource.PlayOneShot(musicSFX);
    }
    public void playBeingHitSound()
    {
        vfxAudioSource.PlayOneShot(beingHitClip);
    }
    public void playShootSound()
    {
        vfxAudioSource.PlayOneShot(shootSFX);
    }
    public void playLaserSound()
    {
        vfxAudioSource.PlayOneShot(laserSFX);
    }
    public void playExplosionSound()
    {
        vfxAudioSource.PlayOneShot(explosionSFX);
    }
    public void playWarningSound()
    {
        vfxAudioSource.PlayOneShot(warningSFX);
    }

    // Update is called once per frame
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    
    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        myMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    private void LoadVolume()
    {
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0.5f);
        SetMusicVolume();
        SetSFXVolume();
    }
}
