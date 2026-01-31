using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    public enum AudioName
    {
        Menu,
        MainGame      
    }


    [SerializeField] private List<AudioClip> _audioClipList = new List<AudioClip>();
    private AudioSource _audioSource;


    private Dictionary<AudioName, AudioClip> _enumNameAudioClipDictionary = new Dictionary<AudioName, AudioClip>();



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }

        _audioSource = GetComponent<AudioSource>();

        foreach (AudioName audioName in System.Enum.GetValues(typeof(AudioName)))
        {
            _enumNameAudioClipDictionary[audioName] = _audioClipList[(int)audioName];
        }

    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.buildIndex == 0)
        {
            PlayMusic(AudioName.Menu);

        }
        else if (arg0.buildIndex == 1)
        {
            PlayMusic(AudioName.MainGame);
        }
    }

    public void PlayMusic(AudioName name)
    {
        if (_audioSource.clip != _enumNameAudioClipDictionary[name] || !_audioSource.isPlaying)
        {
            _audioSource.clip = _enumNameAudioClipDictionary[name];
            _audioSource.Play();
        }
    }

}
