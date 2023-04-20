using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float elapsedTime = 0f;
    private bool isTiming = false;

    // Number of bullets for each type of bullet
    private int bullet1Count = 0;
    private int bullet2Count = 0;
    private int bullet3Count = 0;

    private int[] bulletLevel1 = { 0, 0, 0 };
    private int[] bulletLevel2 = { 0, 0, 0 };
    private int[] bulletLevel3 = { 0, 0, 0 };
    private int[] bulletLevel4 = { 0, 0, 0 };


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
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
            StartTimer();
        }

        else if (scene.name == "Final Score Screen")
        {
            //StopTimer();
        }
    }

    private void Update()
    {       
        if (isTiming)
        {
            elapsedTime += Time.deltaTime;
        }
    }

    public void StartTimer()
    {
        isTiming = true;
    }

    public void StopTimer()
    {
        isTiming = false;
    }

    public float GetTime()
    {
        return elapsedTime;
    }

    // Bullet count getters and setters
    public int GetBullet1Count()
    {
        return bullet1Count;
    }

    public void SetBullet1Count(int count)
    {
        bullet1Count = count;
    }

    public int GetBullet2Count()
    {
        return bullet2Count;
    }

    public void SetBullet2Count(int count)
    {
        bullet2Count = count;
    }

    public int GetBullet3Count()
    {
        return bullet3Count;
    }

    public void SetBullet3Count(int count)
    {
        bullet3Count = count;
    }

    public int[] GetBulletLevel1Count()
    {
        return bulletLevel1;
    }

    public void SetBulletLevel1Count(int[] count)
    {
        bulletLevel1 = count;
    }

    public int[] GetBulletLevel2Count()
    {
        return bulletLevel2;
    }

    public void SetBulletLevel2Count(int[] count)
    {
        bulletLevel2 = count;
    }

    public int[] GetBulletLevel3Count()
    {
        return bulletLevel3;
    }

    public void SetBulletLevel3Count(int[] count)
    {
        bulletLevel3 = count;
    }

    public int[] GetBulletLevel4Count()
    {
        return bulletLevel4;
    }

    public void SetBulletLevel4Count(int[] count)
    {
        bulletLevel4 = count;
    }
}
