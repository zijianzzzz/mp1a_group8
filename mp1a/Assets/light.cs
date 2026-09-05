using UnityEngine;
using UnityEngine.InputSystem;

public class LightSwitch : MonoBehaviour
{
    public Light light;
    public InputActionReference action;

    private int colorIndex = 0;

    private Color[] colors =
    {
        Color.red,
        new Color(1f, 0.5f, 0f), // Orange
        Color.yellow,
        Color.green,
        Color.cyan,
        Color.blue,
        Color.magenta
    };

    void Start()
    {
        light = GetComponent<Light>();

        action.action.Enable();
        action.action.performed += (ctx) =>
        {
            light.color = colors[colorIndex];
            colorIndex = (colorIndex + 1) % colors.Length;
        };
    }
}
