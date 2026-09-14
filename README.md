# RaceMaker

**A mobile arcade racing game focused on track creation, customization, power-ups and user-generated content.**

## Concept

RaceMaker is a top-down / 2.5D arcade racing game built around:

**Build → Test → Modify → Race → Share → Play others' tracks**

The track editor is the heart of the game: players create compact, chaotic and replayable circuits, then immediately race them against AI opponents.

## MVP

The first playable prototype focuses on proving three things:

1. Driving feels fun and responsive on mobile.
2. Building a track is fast and intuitive.
3. Racing a track created by the player is genuinely fun.

### MVP scope

- 1 playable car
- 1 environment / visual theme
- 1 player + 3 AI opponents
- Manual mobile controls
- Modular track editor
- Maximum 20 base track pieces
- Exactly 1 special track element per track
- Exactly 1 obstacle per track
- Random power-up generation pickup
- 1 representative power-up per initial category
- Track validation
- Save/load custom tracks
- Test/race created tracks

## Design pillars

- **Arcade first:** simple, readable and forgiving physics.
- **Creation first:** the editor must be easy enough to use on a phone.
- **Immediate feedback:** build a track, test it, modify it and race again without friction.
- **Chaos with control:** obstacles and power-ups create memorable races without making the game unreadable.
- **No pay-to-win:** future progression and monetization should not undermine competitive play.

## Planned features

- More cars and cosmetic customization
- More track pieces, special elements and obstacles
- Expanded power-up roster
- Track publishing and discovery
- Community ratings, likes and creator profiles
- Daily tracks and challenges
- Credits/progression system
- Events
- Online multiplayer (post-MVP)

## Technical direction

The current technical direction is **Unity** for iOS and Android, using modular track pieces rather than a freeform track/physics construction system.

Track data should remain lightweight and deterministic: pieces have defined connection points and gameplay/collision metadata, allowing tracks to be saved, validated and eventually shared online.

## Project status

**Pre-production / MVP definition**

The repository contains the initial design foundation. The next major milestone is the first Unity prototype: car movement, camera and a minimal modular track system.

## Documentation

- [Game Design Document](docs/GDD.md)
- [Roadmap](docs/ROADMAP.md)
