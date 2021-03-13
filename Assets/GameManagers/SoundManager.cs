using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sound {
    BuildingPlaced,
    BuildingDamaged,
    BuildingDestroyed,
    EnemyDie,
    EnemyHit,
    GameOver
}

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance { get; private set; }

    private AudioSource audioSource;

    private Dictionary<Sound, AudioClip> availableSounds;

    void Awake() {
        Instance = this;

        audioSource = GetComponent<AudioSource>();

        availableSounds = new Dictionary<Sound, AudioClip>();
        LoadSound(Sound.BuildingPlaced);
        LoadSound(Sound.BuildingDamaged);
        LoadSound(Sound.BuildingDestroyed);
        LoadSound(Sound.EnemyDie);
        LoadSound(Sound.EnemyHit);
        LoadSound(Sound.GameOver);
    }

    private void LoadSound(Sound sound) {
        availableSounds[sound] = Resources.Load<AudioClip>("Sounds/" + sound.ToString());
    }

    public void PlaySound(Sound sound) {
        audioSource.PlayOneShot(availableSounds[sound]);
    }

    public float IncreaseVolume(float v) {
        var volume = audioSource.volume + v;
        audioSource.volume = Mathf.Clamp01(volume);
        return audioSource.volume;
    }

    public float DecreaseVolume(float v) {
        var volume = audioSource.volume - v;
        audioSource.volume = Mathf.Clamp01(volume);
        return audioSource.volume;
    }
}
