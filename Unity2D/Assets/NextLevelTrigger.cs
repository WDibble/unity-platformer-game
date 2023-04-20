using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
    [SerializeField] private string nextLevelName;
    private LevelManager levelManager;

    // Reference to the Timer script
    private Timer timer;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        
        // Find the Timer object and get its Timer script component
        timer = FindObjectOfType<Timer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {              
            if (SceneManager.GetActiveScene().name == "Level 1")
            {
                int[] bulletCounts = { timer.GetBullet1Count(), timer.GetBullet2Count(), timer.GetBullet3Count() };
                timer.SetBulletLevel2Count(bulletCounts);
            }

            else if (SceneManager.GetActiveScene().name == "Level 2")
            {
                int[] bulletCounts = { timer.GetBullet1Count(), timer.GetBullet2Count(), timer.GetBullet3Count() };
                timer.SetBulletLevel3Count(bulletCounts);
            }

            else if (SceneManager.GetActiveScene().name == "Level 3")
            {
                int[] bulletCounts = { timer.GetBullet1Count(), timer.GetBullet2Count(), timer.GetBullet3Count() };
                timer.SetBulletLevel4Count(bulletCounts);
            }

            levelManager.LoadLevel(nextLevelName);
        }
    }
}
