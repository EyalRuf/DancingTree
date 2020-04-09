using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTalk : MonoBehaviour
{
    public AudioClip[] audioClips;
    public int lastAudioClipIndex = -1;
    public AudioSource audioSource;

    bool clipBeingPlayed = false;
    const float waitTime = 4f;

    // Update is called once per frame
    void Update()
    {
        if (!clipBeingPlayed && !this.audioSource.isPlaying)
        {
            this.clipBeingPlayed = true;
            StartCoroutine(waitThenPlayRandomClip());
        }
    }

    IEnumerator waitThenPlayRandomClip ()
    {
        yield return new WaitForSeconds(waitTime);
        playRandomClip();
    }

    void playRandomClip ()
    {
        this.audioSource = GetComponent<AudioSource>();

        int randomAudioClipIndex = Random.Range(0, this.audioClips.Length);
        while (randomAudioClipIndex == lastAudioClipIndex)
        {
            randomAudioClipIndex = Random.Range(0, this.audioClips.Length);
        }

        this.lastAudioClipIndex = randomAudioClipIndex;
        this.audioSource.clip = this.audioClips[randomAudioClipIndex];
        this.audioSource.Play();
        this.clipBeingPlayed = false;
    }
}
