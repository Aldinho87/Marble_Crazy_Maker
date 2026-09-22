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


## D010 — Griglia di costruzione 4 × 4 Unity

La Cella di costruzione è fissata a 4 × 4 Unity sul piano X/Z. La cella è distinta dal TrackPiece e lascia spazio interno per ostacoli, trappole ed elementi ambientali.

**Status:** Accepted

## D011 — Livelli Y discreti

L'editor usa inizialmente cinque livelli di costruzione: −2, −1, 0, +1, +2. Un livello corrisponde a 2 Unity verticali.

**Status:** Accepted

## D012 — Regola di connessione verticale

Due ConnectionPoint possono rimanere collegati solo con stesso tipo di connessione, allineamento X/Z e differenza massima di un livello Y. ΔY = 0 è piano; ΔY = ±1 è un dislivello valido; ΔY = ±2 è invalido.

**Status:** Accepted

## D013 — Selettore Y nell'editor

L'editor dispone di cinque pulsanti UI per selezionare il livello attivo. Il cambio del livello attivo modifica la griglia di costruzione visualizzata ma non sposta automaticamente i TrackPiece già presenti.

**Status:** Accepted

## D014 — UI separata dal mondo 3D

Il selettore Y usa una UI Screen Space - Overlay. La futura camera dell'editor può quindi effettuare zoom, pan e orbit sulla scena 3D senza modificare posizione e dimensioni della UI.

**Status:** Accepted

## D015 — Controllo dello spostamento verticale

Se un TrackPiece già collegato viene spostato, lo spostamento verticale viene bloccato quando porterebbe una connessione oltre ΔY = 1. Se viene perso l'allineamento X/Z, la connessione viene rimossa automaticamente.

**Status:** Accepted
