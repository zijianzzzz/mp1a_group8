# MP1A project context

- Unity 6000.5.6f1; project root is `mp1a_group8/mp1a`.
- URP 17.5, Input System 1.20, OpenXR 1.17.1, XR Interaction Toolkit 3.5.1.
- The enabled build scene is `Assets/Scenes/SampleScene.unity`.
- Scene composition is primarily serialized GameObjects and small root-level MonoBehaviours. Imported XR samples have their own assemblies; first-party scripts use the default assembly.
- The room spans x/z -7.5 to 7.5, floor y=0, ceiling y=15. The planet is at (0,7.5,0); the XR rig starts at the origin. Existing car decoration is at (5.91,0.258,-4.97).
- `planetOrbit`, `CometOrbit`, `ShootingBall`, `ShootingBallSpawner`, `breakout`, `light`, and `quit` own the existing interactions. Preserve their scene wiring.
- Unity Test Framework is installed; no first-party test assemblies were found. No Unity MCP tools or project bridge were detected. Editor log is `Logs/Editor.log`; live scene/play-mode validation is unavailable through MCP.
- Parking decorations are static geometry in `Assets/Decorations/ParkingLot.prefab`, instantiated once in SampleScene. They use shared URP materials and built-in meshes, with no new scripts, packages, lights, physics, or input behavior. The original floor still owns collision.
- Blood moon feedback is controlled by `BloodMoonEffects` on Point Light. Three cloud ParticleSystems are serialized as children of planet, moon, and comet in SampleScene; seven burst ParticleSystems are serialized under the four trees and three people in ParkingLot.prefab. All module settings, materials, and emission counts live in those assets. The script only starts/stops assigned ParticleSystem arrays. No runtime particle creation or tree lights.

- Additional hierarchy feedback: two lamp spark children and two cottage window mist children join the blood moon burst array. Cottage Doorway Fog is assigned to BreakOut.viewChangeParticle and plays on view switching. Total input-triggered ParticleSystems: 16, including the existing shooter.
- Spatial audio: 16 saved 3D AudioSources (6 celestial-body cues, 3 gasps in ParkingLot, 2 car horns, 2 teleport cues, welcome, farewell, existing shot). SpatialSoundFeedback on Point Light is called by LightSwitch, ShootingBallSpawner, BreakOut and Quit. Imported audio is under Assets/sound_effect; used clips are mono/preloaded. Quit waits for the farewell duration. Isolated Unity tests verified native clip imports, 3D settings, playback routing, welcome once, teleport placement, farewell duration and repaired shooting clip; perceptual mixing in the main scene is still a manual check.
