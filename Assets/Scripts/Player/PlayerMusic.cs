using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMusic : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private List<AudioSource> audioSources;
    [SerializeField]
    private AudioSource heartBeat;
    [SerializeField]
    private float transitionOutDuration;
    [SerializeField]
    private float fadeOutVolume;
    [SerializeField]
    private float transitionInDuration;
    [SerializeField]
    private float fadeInVolume;

    private bool isDead;
    private bool tierFiveReached;

    private void Start()
    {
        // Start off with all mixer groups at their fadeOutVolume to allow for a better transition when they get turned on
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "exposedParam2", 1f, fadeOutVolume));
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "exposedParam3", 1f, fadeOutVolume));
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "exposedParam4", 1f, fadeOutVolume));
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "exposedParam5", 1f, fadeOutVolume));
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "exposedParam6", 1f, fadeOutVolume));
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "exposedParam7", 1f, fadeOutVolume));

    }

    public void BeginNewMusic(int previousTier, int newTier)
    {
        if (isDead)
            return;

        switch (previousTier) {
            case 1:
                    StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "exposedParam1", transitionOutDuration, fadeOutVolume));
                    StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "exposedParam6", transitionOutDuration, fadeOutVolume));
                break;
            case 2:
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "exposedParam2", transitionOutDuration, fadeOutVolume));
                break;
            case 3:
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "exposedParam3", transitionOutDuration, fadeOutVolume));
                break;
            case 4:
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "exposedParam4", transitionOutDuration, fadeOutVolume));
                break;
            case 5:
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "exposedParam5", transitionOutDuration, fadeOutVolume));
                break;
            default: break;
        }

        audioSources[newTier - 1].Play();
        switch (newTier) {
            case 1:
                if (!tierFiveReached)
                    StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "exposedParam1", transitionInDuration, fadeInVolume));
                else { 
                    StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "exposedParam6", transitionInDuration, fadeInVolume));
                    audioSources[5].Play();
                }
                break;
            case 2:
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "exposedParam2", transitionInDuration, fadeInVolume));
                break;
            case 3:
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "exposedParam3", transitionInDuration, fadeInVolume));
                break;
            case 4:
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "exposedParam4", transitionInDuration, fadeInVolume));
                break;
            case 5:
                tierFiveReached = true;
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "exposedParam5", transitionInDuration, fadeInVolume));
                break;
            case 7:
                isDead = true;
                StartCoroutine(FadeMixerGroup.StartFade(audioMixer, "exposedParam7", transitionInDuration, fadeInVolume));
                break;
            default: break;
        }
    }

    public void PlayHeartbeat() {
        heartBeat.Play();
    }

    public void StartMusic() {
        audioSources[0].Play();
    }
}
