# Vampire keys and locks

The earlier camera HUD and name-based inventory have been removed. This feature uses physical props, direct hand grabbing, and three matching socket locks.

## Three different pairs

| Key | Matching lock | Unlock event |
| --- | --- | --- |
| Ruby blood vial with gold collars | Gold chalice at the Blood Tithe altar | Reliquary lid opens to reveal a heart |
| Pointed silver stake with a wrapped handle | Stake receptacle beside the bone-colored coffin | Coffin lid swings open |
| Purple-and-gold winged bat medallion | Bat-shaped crest on the crypt | Iron gate rises |

Each pairing differs in silhouette, theme, color, label, and moving mechanism. Drop a prop anywhere over its matching lock table, then release your grip or left mouse button in simulator Point and Click mode. Each lock has a 2 m wide, 1.72 m deep box trigger from local Y=0.8 to 1.8, covering the tabletop with a small edge margin. The matching prop snaps into its original socket seat when accepted. A matching offer changes the indicator to gold and displays **RELEASE TO OFFER**. Once accepted, the indicator turns green, the sign reads **UNLOCKED**, and the mechanism opens. Wrong props cannot hover or select that socket. Each unlock fires once; installed props remain latched.

## Scene and controls

- Open `Assets/Scenes/SampleScene.unity` and enter Play Mode.
- The **Vampire Relics** group contains separate **Keys** and **Locks** groups. Keys stand on their own pedestals at world Z=1.2; the lock stations are farther back at Z=3.6.
- Both XR controllers have direct-grab children with 12 cm trigger volumes. They use the existing XRI left/right **Select** grip actions and join the existing hand interaction groups with priority over distant grabbing.
- Aim the **right controller's ray** at a key. Its accent turns **gold** to show it can be grabbed. Hold the **grip button** to pull it to your hand; its accent turns **cyan** while held. Carry it to the matching lock and release grip to offer it. Aiming alone never collects a key.
- In the XR Interaction Simulator, focus the Game view, select the right controller with `]`, aim at a key, and hold `G` for grip. Release `G` to drop or place it. These are the existing simulator bindings.
- The existing floor retains its MeshCollider. All three props have gravity-enabled, non-kinematic Rigidbodies, colliders, and XR Grab Interactable behavior. Three loose crypt stones also fall and tumble.

## Editing

`Assets/VampireProps/VampireRelics.prefab` arranges the separate key displays and lock stations. `BloodVialKey.prefab`, `SilverStakeKey.prefab`, and `BatMedallionKey.prefab` are independent movable props. `ChaliceLock.prefab`, `CoffinLock.prefab`, and `CryptLock.prefab` contain only the accepting mechanism, with no key inside. Move or reuse each independently. Shapes and materials are saved Unity assets; Blender is not required.

`VampireKey` derives from XRGrabInteractable. Its **Relic** enum defines the identity. The saved **Far Attach Mode = Near** setting brings a ray-selected key to the hand. Hover/select events drive the targeting and held colors. `VampireLock` derives from XRSocketInteractor; **Required Relic** chooses its match, and **On Unlocked** can trigger another scene event later. **Moving Part**, **Opened Local Position**, and **Opened Local Euler Angles** configure the visible opening.

`LeftDirectGrab.prefab` and `RightDirectGrab.prefab` are attached to their matching controller in SampleScene. `VampireDirectInteractor` joins its parent hand's XRInteractionGroup at startup to avoid competing with that hand's ray interactor. Existing input assets and project settings are unchanged.

Unlock and victory state reset when the scene reloads. There is no inventory UI or persistent save system.

## Escape goal and signifiers

`VampireEscape` on the arrangement prefab updates the original scene object named **Canvas**, on the back wall. It contains the escape heading, goal, progress, grab/offer instructions, and the original controls in five **TextMeshProUGUI** labels on an opaque background. The separate floating Canvas has been removed. Key names, raised lock titles, and lock status use opaque **TextMeshPro** 3D labels, with no legacy TextMesh components. General grabbing and offering explanations appear only on the Canvas. Original controls remain: Right Primary — Quit; Right Secondary — Change Light Color; Left Primary — Switch View Location. Edit the table prefabs or the existing scene Canvas children in the Inspector. The scene VampireRelics instance overrides its heading/progress references to point to that Canvas. It teaches that gold highlights mark grabbable objects, green marks solved locks, and all three named key/lock pairs are required. In simulator Point and Click mode, holding left mouse on a key simulates grip; releasing it offers the key to the table.

Progress advances from 0/3 to 3/3. Victory requires all three distinct locks to finish opening AND retain their matching installed props. Only then does the sign show **YOU ESCAPED!**, fade to green, and invoke **On Escaped** once. The completed scene stays visible for the walkthrough; locomotion is not frozen and the application does not close.

Each lock's indicator blends to green over 1.1 seconds with SmoothStep easing, alongside the existing eased lid or gate motion. Matching props remain in their original visible socket seats. A new round requires reloading the scene.

The pairs share a taught offering rule but differ in shape, theme, and result (heart revealed, coffin opened, crypt gate lifted). Their input mechanic is deliberately the same broad table drop; they do not yet provide increasing puzzle complexity.

### Walkthrough verification

1. Show the goal sign and the three separate key props.
2. Target a key to show its gold highlight, then hold left mouse to grab it (cyan).
3. Drop a wrong prop on a table to demonstrate rejection; retrieve it.
4. Drop each matching prop near a table edge. Show its eased indicator and mechanism, and the progress counter.
5. Confirm one or two solved locks do not win. Frame all three installed keys as the final opening completes and the victory message appears.
## Boarded wall door and hidden Night Crest room

The wall under the existing Canvas now has a 2.4 m wide, 2.5 m tall doorway at world Z=7.5. Four nailed wooden planks seal an opaque oak door. The door and its collision barrier remain closed; entry uses the teleport interaction.

Solve Blood Tithe and Hunter's Seal, in either order. Once both lock animations finish, the boards slide away over 1.1 seconds. The door stays closed. Aim the left controller at the door and press **Left Secondary** to teleport inside. In the simulator use **Shift + 2**. A matching return door on the inside wall uses the same left-controller aim and button to teleport back to the main room.

Night Crest and the bat-medallion pedestal are inside the enclosed crypt beyond the wall. The room has a floor, walls, ceiling, and warm sconces. Its key and socket interactions stay disabled until the boards have cleared, preventing a ray grab or solve through the sealed wall. Night Crest still supplies the third lock required for victory.

Edit **VampireRelics > Boarded wall doorway and hidden crypt**. `BoardedCryptDoor` has explicit references to the two required locks, boards, door hinge, passage collider, status label, and gated interactions. `CryptDoorTeleport` handles both doors, using the existing LeftHand/SecondaryButton input action. Scene overrides assign the XR Origin and left controller. A clear pointing ray within 10 m is required; other surfaces block the ray. Each press teleports once, with a short cooldown, preserving head height and facing the destination. The original `front_plane` is disabled in SampleScene; three solid wall sections in the relic arrangement replace it around the opening. Keep the arrangement's scene position at (0,0,3) so those sections align with the room boundary.

## Five gate layout (current)

Aim with the left controller and press Left Secondary (simulator Shift + 2). There are five destination pairs; return doors/portals are the reverse route, not additional destinations.

| Entrance | Destination and contents |
| --- | --- |
| Existing house door | Enclosed Blood Tithe chamber, blood vial pedestal and chalice lock |
| Blue car rear side door | Simulated back-seat cabin, coffin on the rear bench |
| Green car rear side door | Separate ordinary back-seat cabin, no key or lock |
| Portal in an east parking bay | Landing terrace on top of the existing planet, silver stake pedestal |
| Boarded wall crypt | Existing Night Crest and bat medallion, available after the first two seals |

The two car doors swing when activated, then teleport the player. Every destination has a return route. The planet's solid landing cap and low perimeter rim are stationary while the original planet continues rotating. Carry the silver stake through the return portal and into the blue car, then release it at the coffin's accepting area. Blood Tithe remains independent and can be solved before or after Hunter's Seal. No extra keys or locks were duplicated.

The new geometry is saved in VampireRelics.prefab: house/cabins are offset beyond the main room, and the green car is a nested instance of the same native PrimitiveCar prefab with green body material overrides. The original blue car and cottage remain in their existing scene locations. Thin gate surfaces align with their visible doors. Keep VampireRelics at (0,0,3); moving the original cottage/car requires moving its corresponding gate and outside-arrival marker too.

CryptDoorTeleport owns all gate input to avoid multiple handlers teleporting on one press. Its Additional Gates list stores the four added entrance/return pairs, destination markers, hints and optional opening hinges. Existing scene XR references are retained. VampireKey moves a held key with the rig during direct gate travel and briefly excludes the jump from its throw velocity.

The original Canvas includes the route instructions and all original controller controls. Table labels remain solid 3D TMP text. For the walkthrough, show the installed vial and stake at their separate destinations before entering the final crypt and installing the bat medallion.
