using System.Collections.Generic;
using UnityEngine;

public class MaskSoundManager : MonoBehaviour
{

    [SerializeField] private List<AudioClip> _audioClipList = new List<AudioClip>();
    private AudioSource _audioSource;


    private Dictionary<Mask, AudioClip> _enumNameAudioClipDictionary = new Dictionary<Mask, AudioClip>();

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        foreach (Mask mask in System.Enum.GetValues(typeof(Mask)))
        {
            _enumNameAudioClipDictionary[mask] = _audioClipList[(int)mask];
        }
    }

    private void Start()
    {
        GameStateManager.Instance.OnGameStateChanged += GameStateManager_OnGameStateChanged;
        PlayerMask.Instance.OnChangedMask += PlayerMask_OnChangedMask;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= GameStateManager_OnGameStateChanged;
        PlayerMask.Instance.OnChangedMask -= PlayerMask_OnChangedMask;
    }

    private void GameStateManager_OnGameStateChanged(object sender, GameStateManager.OnGameStateChangedEventArgs e)
    {
        if (e.NewGameState == GameState.Playing)
        {
            Mask currentMask = PlayerMask.Instance.GetPlayerMask();
            PlayMaskSound(currentMask);
        }
        else if (e.NewGameState == GameState.GameOver)
        {
            StopPlaying();
        }
    }

    private void PlayerMask_OnChangedMask(object sender, PlayerMask.OnChangedMaskEventArgs e)
    {
        if (GameStateManager.Instance != null && GameStateManager.Instance.GetCurrentGameState() == GameState.Playing)
        {
            PlayMaskSound(e.NewMask);
        }
    }

    public void PlayMaskSound(Mask mask)
    {
        if (_audioSource.clip != _enumNameAudioClipDictionary[mask] || !_audioSource.isPlaying)
        {
            _audioSource.clip = _enumNameAudioClipDictionary[mask];
            _audioSource.Play();
        }
    }

    private void StopPlaying()
    {
        _audioSource.Stop();
    }
}
