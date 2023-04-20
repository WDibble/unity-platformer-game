using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Timer timer;
    public TransitionImageController transitionImageController;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level 1")
        {
            timer.StartTimer();
        }

        // Trigger the fade out when the scene loads
        transitionImageController.FadeOut();
    }

    public void LoadLevel(string levelName)
    {
        // Trigger the fade in and wait for it to complete before loading a new scene
        StartCoroutine(LoadLevelWithTransition(levelName));
    }

    private IEnumerator LoadLevelWithTransition(string levelName)
    {
        transitionImageController.FadeIn();
        yield return new WaitForSeconds(transitionImageController.transitionTime);
        SceneManager.LoadScene(levelName);
    }

    public void StopTimer()
    {
        timer.StopTimer();
    }
}
