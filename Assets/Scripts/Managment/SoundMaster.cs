using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaster : MonoBehaviour
{  
    public enum Sounds 
    { 
        Spawn, 
        GoblinDie,
        Damage,
        MonsterDie,
        Freeze, 
        KillAll,
        GameOver
    }

    public static SoundMaster instance;

    private AudioSource _audioSource;

    [SerializeField] private AudioClip[] _musicTracks;
    [SerializeField] private AudioClip _spawn;
    [SerializeField] private AudioClip _goblinDie;
    [SerializeField] private AudioClip _damage;
    [SerializeField] private AudioClip _monsterDie;
    [SerializeField] private AudioClip _freeze;
    [SerializeField] private AudioClip _killAll;
    [SerializeField] private AudioClip _gameOver;

    private Dictionary<string, AudioClip> _sounds;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        _sounds = new Dictionary<string, AudioClip>();

        _sounds["Spawn"] = _spawn;
        _sounds["Damage"] = _damage;
        _sounds["GoblinDie"] = _goblinDie;
        _sounds["MonsterDie"] = _monsterDie;
        _sounds["Freeze"] = _freeze;
        _sounds["KillAll"] = _killAll;
        _sounds["GameOver"] = _gameOver;

        PlayRandomMusic();

        instance = this; 
    }

    private void PlayRandomMusic()
    {
        _audioSource.clip = _musicTracks[Random.Range(0, _musicTracks.Length)];
        _audioSource.Play();
    }
    public void PlaySoundEffect(Sounds sound, float volume) => 
        _audioSource.PlayOneShot(_sounds[sound.ToString()], volume);           
}
