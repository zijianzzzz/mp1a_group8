# MP1A Group 8

Unity VR project for CS 417.

## Controls

| Action | XR controller | Keyboard | Result |
| --- | --- | --- | --- |
| Quit | Right-hand primary button | `1` | Stops Play mode in the Unity Editor or quits a standalone build. |
| Change light color | Right-hand secondary button | `2` | Advances the point light to the next color in the cycle. |
| Switch view location | Left-hand primary button | `3` | Alternates between the room and the external viewing point. |
| Spawn car | Left-hand secondary button | `4` | Spawns a car at a random position (`x/z`: -7 to 7, `y`: 2 to 10), plays spatial sound and a particle burst there, and drops it onto the floor. |

## Input setup

- Quit uses `ControllerSampleActions/RightHand/PrimaryButton`.
- Light color cycling uses `ControllerSampleActions/RightHand/SecondaryButton`.
- View switching uses `ControllerSampleActions/LeftHand/PrimaryButton`.
- Car spawning uses `ControllerSampleActions/LeftHand/SecondaryButton`.
- For keyboard testing, the matching controls are `1`, `2`, `3`, and `4`.

## Core Requirements

All Core Requirements are satisfied:

- [x] Particle Bursts
- [x] Spatial Sound
- [x] Object Space
- [x] World Space
- [x] Materials
- [x] Highlight Outline
- [x] XR Tracked Camera
- [x] Euler Steady
- [x] Kinematic Double Integrators
- [x] XR Controller Inputs
- [x] Quit Key
- [x] Object Spawning
- [x] Camera Teleport
