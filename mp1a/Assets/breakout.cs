using UnityEngine;
using UnityEngine.InputSystem;

public class BreakOut : MonoBehaviour
{
    public InputActionReference action;
    public Transform externalViewingPoint;

    private Vector3 roomPosition;
    private bool isOutside = false;

    void Start()
    {
        // The player starts inside the room.
        roomPosition = transform.position;

        action.action.Enable();
        action.action.performed += (ctx) =>
        {
            isOutside = !isOutside;

            if (isOutside)
            {
                transform.position = externalViewingPoint.position;
            }
            else
            {
                transform.position = roomPosition;
            }
        };
    }
}