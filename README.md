# MP1A Group 8

Unity VR project for CS 417.

## Controls

| Action | XR controller | Keyboard | Result |
| --- | --- | --- | --- |
| Quit | Right-hand primary button | `1` | Stops Play mode in the Unity Editor or quits a standalone build. |
| Change light color | Right-hand secondary button | `2` | Advances the point light to the next color in the cycle. |

## Input setup

- Quit uses `ControllerSampleActions/RightHand/PrimaryButton`.
- Light color cycling uses `ControllerSampleActions/RightHand/SecondaryButton`.
- For keyboard testing, `1` is bound to `PrimaryButton` and `2` is bound to `SecondaryButton`.
