using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [Header("Clips")]
    [SerializeField] private List<AudioClip> _musicClips;
    [SerializeField] private List<AudioClip> _sfxClips;

    private Dictionary<string, AudioClip> _musicDict;
    private Dictionary<string, AudioClip> _sfxDict;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _musicDict = new Dictionary<string, AudioClip>();
        foreach (var clip in _musicClips)
            _musicDict[clip.name] = clip;

        _sfxDict = new Dictionary<string, AudioClip>();
        foreach (var clip in _sfxClips)
            _sfxDict[clip.name] = clip;
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "MainMenu":
                PlayMusic("Drifting Memories");
                break;

            case "Level1":
                PlayMusic("Strange Worlds");
                break;
        }
    }

    public void PlayMusic(string name)
    {
        if (_musicDict.ContainsKey(name))
        {
            _musicSource.clip = _musicDict[name];
            _musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Music not found: " + name);
        }
    }

    public void PlaySFX(string name)
    {
        if (_sfxDict.ContainsKey(name))
        {
            _sfxSource.PlayOneShot(_sfxDict[name]);
        }
        else
        {
            Debug.LogWarning("SFX not found: " + name);
        }
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }
}

