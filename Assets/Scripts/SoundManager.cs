using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource musicAudioSource;
    public AudioSource vfxAudioSource;
    public AudioClip musicClip;
    public AudioClip musicSFX;

    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;
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

    public void playSFX()
    {
        vfxAudioSource.PlayOneShot(musicSFX);
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
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetMusicVolume();
        SetSFXVolume();
    }
}
