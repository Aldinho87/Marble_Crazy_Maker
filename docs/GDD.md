# RaceMaker — Game Design Document

**Status:** Pre-production / MVP definition  
**Version:** 0.1

## 1. Vision

RaceMaker is a mobile arcade racing game inspired by the immediacy and chaos of miniature top-down racers, with the track editor as its defining feature.

The player should be able to create a track quickly, test it immediately, modify it and race it against opponents.

### Core loop

**Build → Test → Modify → Race → Share → Play others' tracks**

## 2. Camera and presentation

- Top-down / 2.5D presentation.
- Stylized toy/model-car aesthetic.
- Camera high enough for clear driving and track readability.
- Enough perspective to support bridges, ramps, loops, jumps and drops.
- Visual quality can evolve after the gameplay prototype is proven.

## 3. Cars

Initial visual roster may include:

- Race car
- Rally car
- Muscle car
- Buggy
- Sports car
- Pickup / off-road car

For the initial prototype, cars are visually different but have identical gameplay statistics: maximum speed, acceleration, braking, grip and weight.

Future customization should primarily be cosmetic. Any future competitive performance differences must not create pay-to-win gameplay.

## 4. Driving controls

Manual driving is required; there is no automatic acceleration.

### Left side

- Virtual analog joystick for steering.
- Small input = small steering angle.
- Full input = maximum steering.

### Right side

- Accelerate button.
- Brake / reverse button.
- Power-up button.

The driving model should feel arcade-like, responsive and forgiving rather than simulation-oriented.

## 5. Physics

Use simplified arcade physics with predictable results.

Loops and jumps are intentionally game-like:

- sufficient speed allows a loop to be completed;
- insufficient speed causes loss of momentum or reversal;
- collisions may cause an ejection, slowdown or fall depending on the element.

Do not over-engineer realistic vehicle dynamics during the MVP.

## 6. Track editor

The editor is the central feature of RaceMaker.

### MVP construction limit

Each track may contain:

- **20 total base/simple track pieces maximum**;
- **1 special element**;
- **1 obstacle**.

The resulting prototype therefore supports up to 22 placed components, subject to validation and connection rules.

### Base/simple pieces

- Straight
- Wide curve
- Tight curve
- 180° curve
- S / serpentine
- Chicane
- Intersection
- Fork / bifurcation
- Connector / raccordo
- Start
- Finish

### Special elements

The player selects one special element from the available list. Planned candidates include:

- Normal bridge
- Narrow bridge
- Suspension bridge
- Drawbridge
- Wooden bridge
- Partially destructible bridge
- Moving bridge
- Normal ditch / fossato
- Ditch with jump
- Ditch with platforms
- Ditch with traps
- Water ditch
- Ditch with moving objects
- Vertical loop
- Double loop
- Half loop
- Spiral
- Jump
- Ramp
- Jump over a gap
- Rollercoaster-like section

### Obstacles

The player selects one obstacle from the available list.

**Static:**

- Barriers
- Walls
- Rocks
- Tires
- Crates
- Logs

**Dynamic:**

- Hammers
- Rotating blades
- Doors
- Moving platforms
- Moving bridges
- Gates
- Swinging obstacles

**Destructible:**

- Destructible bridge / breakable pieces

Special elements and obstacles remain separate editor categories even when an implementation could technically overlap.

## 7. Track validation

The editor must prevent invalid tracks where possible.

Minimum validation requirements:

- exactly one start;
- exactly one finish;
- connected track path;
- compatible piece connections;
- race must be testable before publishing in the future.

The editor should have a prominent **Test** action that immediately starts a race on the current creation.

Future publishing rule: the creator must successfully complete their own track before publishing it.

## 8. Power-ups

Power-ups follow a Mario Kart-like design philosophy: easy to understand, impactful and chaotic without becoming unreadable.

The initial prototype uses a generic **random power-up generation pickup** placed on the track. Collecting it gives a randomly selected power-up.

Initial power-up categories:

### Offensive

Representative candidates:

- Homing missile
- Bomb
- EMP
- Magnet

### Defensive

Representative candidates:

- Shield
- Ghost

### Movement

Representative candidates:

- Turbo
- Glider / deltaplane
- Teleport

### Environmental

Representative candidates:

- Oil
- Ice

For the MVP, one representative power-up from each category is sufficient. The exact four selected effects should be finalized during implementation based on fun, readability and technical cost.

## 9. Race format and AI

MVP race format:

- 1 human player
- 3 AI opponents

AI requirements:

- follows the track;
- collects power-ups when practical;
- uses power-ups;
- reacts to obstacles;
- occasionally makes believable mistakes.

Perfect AI simulation is not required. The goal is to create entertaining opponents and validate the gameplay loop.

## 10. Saving and sharing

MVP:

- save tracks locally;
- load saved tracks;
- test saved tracks.

Post-MVP:

- publish tracks;
- discover tracks created by other players;
- share codes;
- likes / ratings;
- creator profiles;
- follow creators;
- daily track;
- categories such as popular, new, hardest, fastest and craziest.

## 11. Progression

A future credit system may reward:

- race participation;
- finishing position;
- daily missions;
- daily login bonus;
- events;
- community activity.

Credits can unlock additional creative elements and cosmetics.

Basic creative freedom should not be excessively restricted behind monetization.

## 12. Monetization principles

The project is initially focused on proving the game rather than maximizing monetization.

Potential future models:

- cosmetic purchases;
- optional premium pass;
- ad removal;
- non-intrusive advertising.

Competitive gameplay should avoid pay-to-win mechanics.

## 13. Multiplayer

Online multiplayer is a post-MVP feature.

The first prototype should be architected so that track data is compact and deterministic enough to support future sharing and online use, without implementing networking prematurely.

## 14. Technical direction

Target platforms:

- iOS
- Android

Engine:

- Unity

Track construction should use modular pieces rather than freeform geometry.

Each track piece should define at least:

- entry connection;
- exit connection;
- width;
- surface/gameplay type;
- collision data;
- optional special behavior.

Track data should be stored as lightweight piece identifiers and parameters, rather than as large scene files. This will make saving, validation and future online sharing easier.

## 15. MVP success criteria

The MVP is successful if a player can:

1. understand the controls without explanation;
2. drive a car comfortably on a phone;
3. build a simple track quickly;
4. press Test and immediately race it;
5. understand the power-up system;
6. enjoy racing against three AI opponents;
7. want to build another track.

The last criterion is particularly important: **the creation loop must generate replay value on its own.**

## 16. Open decisions

These remain intentionally unresolved until prototype testing:

- exact visual style and environment;
- final four MVP power-ups;
- exact track-piece dimensions/grid system;
- AI difficulty model;
- race lap/count rules;
- whether intersections/forks are allowed in competitive validation;
- final game name and trademark availability;
- progression pacing;
- monetization details.
