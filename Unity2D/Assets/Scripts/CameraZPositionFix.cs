using UnityEngine;

public class CameraZPositionFix : MonoBehaviour
{
    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.z = Mathf.Abs(pos.z) * -1f;
        transform.position = pos;
    }
}