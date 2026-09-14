# RaceMaker — Decision Log

This file records important design decisions so that later changes can be evaluated against the original intent rather than silently replacing it.

## D001 — Editor-first concept

The track editor is the defining feature of the game. Racing exists both as the core gameplay experience and as immediate feedback for creations.

**Status:** Accepted

## D002 — Modular track construction

Tracks use predefined modular pieces rather than freeform geometry.

**Status:** Accepted

## D003 — MVP track limits

An MVP track can contain a maximum of 20 base pieces, plus exactly 1 special element and exactly 1 obstacle.

**Status:** Accepted

## D004 — Manual driving

The player manually accelerates, brakes/reverses and steers using mobile controls. Automatic acceleration is not part of the intended control scheme.

**Status:** Accepted

## D005 — Identical initial car performance

Initial cars differ visually but not statistically.

**Status:** Accepted

## D006 — MVP race format

The first prototype uses one human player and three AI opponents.

**Status:** Accepted

## D007 — Random power-up pickup

The initial power-up system uses a generic pickup that generates a random power-up. The MVP aims for one representative effect from each initial category.

**Status:** Accepted

## D008 — Multiplayer deferred

Online multiplayer is intentionally postponed until the creation-and-racing loop is proven.

**Status:** Accepted

## D009 — No pay-to-win direction

Future monetization should favor cosmetics and convenience and should not compromise competitive fairness.

**Status:** Guiding principle
