/*
 * AudioManager.cs
 * Author: William Dibble
 * Date: 24-04-2023
 *
 * This script is responsible for managing the audio in the game.
 * 
 * It contains audio source and audio clip variables for sound effects and background music, 
 * as well as methods for playing each sound effect and playing music for the current scene.
 */

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    // Declare Audio Source and Audio Clip variables for the sound effects
    public AudioSource audioSource;
    public AudioClip blowSound;
    public AudioClip cinematicSound;
    public AudioClip cinematicSound2;
    public AudioClip coin1Sound;
    public AudioClip coin2Sound;
    public AudioClip collectSound;
    public AudioClip curiousSound;
    public AudioClip dotSound;
    public AudioClip explosionSound;
    public AudioClip fallImpactSound;
    public AudioClip hit1Sound;
    public AudioClip hit2Sound;
    public AudioClip hit3Sound;
    public AudioClip jump1Sound;
    public AudioClip jump2Sound;
    public AudioClip bigJumpSound;
    public AudioClip levelUpSound;
    public AudioClip machineSound;
    public AudioClip questionSound;
    public AudioClip select1Sound;
    public AudioClip select2Sound;
    public AudioClip select3Sound;
    public AudioClip select4Sound;
    public AudioClip select5Sound;
    public AudioClip slideSound;
    public AudioClip textSound;
    public AudioClip invisible1Sound;
    public AudioClip invisible2Sound;
    public AudioClip fire1;

    // Declare Audio Source and Audio Clip variables for the background music
    public AudioSource musicSource;
    public AudioClip startSceneMusic;
    public AudioClip level1Music;
    public AudioClip level2Music;
    public AudioClip level3Music;
    public AudioClip level4Music;
    public AudioClip finalScoreScreenMusic;

    public float musicFadeTime = 1f;

    private Coroutine fadeCoroutine;

    private void Awake()
    {
        // Add audio sources if they are not assigned
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
        }

        // Register the OnSceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Unregister the OnSceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Method called when a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlaySceneMusic();
    }

    private void Start()
    {
        // Play the music for the current scene
        PlaySceneMusic();
    }

    // Methods to play each sound effect
    public void PlayBlowSound() { PlaySound(blowSound); }
    public void PlayCinematicSound() { PlaySound(cinematicSound); }
    public void PlayCinematicSound2() { PlaySound(cinematicSound2); }
    public void PlayCoin1Sound() { PlaySound(coin1Sound); }
    public void PlayCoin2Sound() { PlaySound(coin2Sound); }
    public void PlayCollectSound() { PlaySound(collectSound); }
    public void PlayCuriousSound() { PlaySound(curiousSound); }
    public void PlayDotSound() { PlaySound(dotSound); }
    public void PlayExplosionSound() { PlaySound(explosionSound); }
    public void PlayFallImpactSound() { PlaySound(fallImpactSound); }
    public void PlayHit1Sound() { PlaySound(hit1Sound); }
    public void PlayHit2Sound() { PlaySound(hit2Sound); }
    public void PlayHit3Sound() { PlaySound(hit3Sound); }
    public void PlayJump1Sound() { PlaySound(jump1Sound); }
    public void PlayJump2Sound() { PlaySound(jump2Sound); }
    public void PlayBigJumpSound() { PlaySound(bigJumpSound); }
    public void PlayLevelUpSound() { PlaySound(levelUpSound); }
    public void PlayMachineSound() { PlaySound(machineSound); }
    public void PlayQuestionSound() { PlaySound(questionSound); }
    public void PlaySelect1Sound() { PlaySound(select1Sound); }
    public void PlaySelect2Sound() { PlaySound(select2Sound); }
    public void PlaySelect3Sound() { PlaySound(select3Sound); }
    public void PlaySelect4Sound() { PlaySound(select4Sound); }
    public void PlaySelect5Sound() { PlaySound(select5Sound); }
    public void PlaySlideSound() { PlaySound(slideSound); }
    public void PlayTextSound() { PlaySound(textSound); }
    public void PlayInvisible1() { PlaySound(invisible1Sound); }
    public void PlayInvisible2() { PlaySound(invisible2Sound); }
    public void PlayFire1() { PlaySound(fire1); }

    // Method to play a specific sound effect
    private void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    // Method to play music for the current scene
    public void PlaySceneMusic()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        AudioClip targetClip = null;

        // Select appropriate music based on scene name
        if (sceneName == "Start Scene")
        {
            targetClip = startSceneMusic;
        }
        else if (sceneName == "Level 1")
        {
            targetClip = level1Music;
        }
        else if (sceneName == "Level 2")
        {
            targetClip = level2Music;
        }
        else if (sceneName == "Level 3")
        {
            targetClip = level3Music;
        }
        else if (sceneName == "Level 4")
        {
            targetClip = level4Music;
        }
        else if (sceneName == "Final Score Screen")
        {
            targetClip = finalScoreScreenMusic;
        }

        // Fade out old music and play new music if target clip is different
        if (targetClip != null && musicSource.clip != targetClip)
        {
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
            }
            fadeCoroutine = StartCoroutine(FadeAndPlayMusic(targetClip));
        }
    }

    // Coroutine to fade out current music and fade in new music
    private IEnumerator FadeAndPlayMusic(AudioClip clip)
    {
        // Fade out current music
        float startTime = Time.time;
        float startVolume = musicSource.volume;
        while (Time.time < startTime + musicFadeTime)
        {
            musicSource.volume = Mathf.Lerp(startVolume, 0, (Time.time - startTime) / musicFadeTime);
            yield return null;
        }
        musicSource.volume = 0;

        // Set new clip and start playing
        musicSource.clip = clip;
        musicSource.Play();

        // Fade in new music
        startTime = Time.time;
        while (Time.time < startTime + musicFadeTime)
        {
            musicSource.volume = Mathf.Lerp(0, startVolume, (Time.time - startTime) / musicFadeTime);
            yield return null;
        }
        musicSource.volume = startVolume;
    }
}
