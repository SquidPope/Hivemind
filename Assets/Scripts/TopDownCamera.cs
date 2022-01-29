using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    // Script to control camera movement
    Vector3 newPos;
    const float offset = -10f;

    void LateUpdate()
    {
        newPos = SwarmManager.Instance.transform.position;
        newPos.z += offset;
        transform.position = newPos;
    }
}
