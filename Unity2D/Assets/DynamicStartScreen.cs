using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicStartScreen : MonoBehaviour
{
    public float parallaxEffectMultiplier = 0.05f;
    public float smoothness = 5f;
    public float distanceEffectRange = 50f;
    private Vector3 startPosition;
    private float screenWidth;
    private float screenHeight;

    void Start()
    {
        startPosition = transform.position;
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    void Update()
    {
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        float distance = Vector3.Distance(transform.position, startPosition);
        float adjustedMultiplier = Mathf.Lerp(parallaxEffectMultiplier, 0, distance / distanceEffectRange);

        float deltaX = (mouseX - screenWidth * 0.5f) * adjustedMultiplier;
        float deltaY = (mouseY - screenHeight * 0.5f) * adjustedMultiplier;

        Vector3 targetPosition = new Vector3(startPosition.x + deltaX, startPosition.y + deltaY, startPosition.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);
    }
}
