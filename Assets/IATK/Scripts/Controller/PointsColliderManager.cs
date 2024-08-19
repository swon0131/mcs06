using UnityEngine;
using UnityEngine.InputSystem;

public class PointsColliderManager : MonoBehaviour
{
    public InputActionReference rightTriggerPressAction; // Reference to the Right Trigger Press action

    private void Awake()
    {
        // Find all PointCollider objects and attach input handling
        foreach (var pointCollider in FindObjectsOfType<PointsInteraction>())
        {
            pointCollider.AssignInputAction(rightTriggerPressAction);
        }
    }
}
