using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Timer timer;

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
    }
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void StopTimer()
    {
        timer.StopTimer();
    }
}
