using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatPointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] points;
    private int currentPointIndex = 0;

    [SerializeField] private float speed = 2f;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.enabled = true;
        Vector2 previousPosition = transform.position;
        if (Vector2.Distance(points[currentPointIndex].transform.position, transform.position) < .1f)
        {
            currentPointIndex++;
            if (currentPointIndex >= points.Length)
            {
                currentPointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, points[currentPointIndex].transform.position, Time.deltaTime * speed);
        UpdateObjectDirection(previousPosition);
    }

    // Update the object's direction based on the moving direction
    private void UpdateObjectDirection(Vector2 previousPosition)
    {
        float direction = transform.position.x - previousPosition.x;

        if (direction > 0.01f) // Moving right
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (direction < -0.01f) // Moving left
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
