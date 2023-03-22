using UnityEngine;

public class Bullet3ImpactDestroy : MonoBehaviour
{
    [SerializeField] private float delay = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delay);
    }
}