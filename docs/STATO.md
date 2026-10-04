# STATO — Marble Crazy Maker

Aggiornare a fine di ogni sessione di lavoro. Ultimo aggiornamento: 2026-10-04.

> Ricostruito da appunti riassuntivi, non dal progetto Unity: verifica nel progetto prima di fidarti.

## Fase
Pre-produzione, concept in evoluzione.

## Funziona (testato)
- Snap tra due pezzi rettilinei (aggancio South–North corretto, senza seam nella texture)
- `YLevelSelector` dinamico (pulsanti generati a runtime, in basso a destra nella UI)
- Prefab dei pezzi base: rettilineo, curva 90° (+mirrored), rampa dritta, rampa curva 90° (+mirrored)
- Prefab `Marble` (sfera scala 0.5, Rigidbody, Sphere Collider, materiale rosso provvisorio)
- Lancio di test con `MarbleTestLauncher` (impulso fisso, Spazio/R), `MarbleRollingResistance` e `MarbleAutoStop`: dopo gli ultimi file completi applicati (con `rb.WakeUp()` in `Launch()` e contatore di `MarbleAutoStop` azzerato al lancio) **confermato che funziona tutto**

## Problemi aperti
1. **Cucitura tra Mesh Collider**: se la biglia attraversa o parte esattamente da un punto di giunzione tra due pezzi (es. Z multiplo di 4), esce di pista in modo anomalo o non parte. Causa probabile: Mesh Collider statici separati (fenomeno noto di PhysX).
   - Non risolto con: Default Contact Offset 0.03; Solver Iterations 12 (peggiora)
   - Soluzione individuata, **rimandata**: unire i collider dei pezzi collegati in un unico collider combinato (`Mesh.CombineMeshes`) nella fase TEST/VERIFY
2. **Dubbio sulle curve/rampe**: potrebbero essere troppo strette o brusche per la biglia. Da verificare con un test pratico reale prima di modificare (es. curve su più celle con raggio maggiore).

## Script Unity del progetto
`TrackPiece`, `ConnectionPoint`, `SurfaceData`, `SurfacePhysics`, `GridManager`, `GridSnap`, `GridMoveTool`, `GridSnapEditor`, `TrackPieceGenerator`, `MarbleTestLauncher`, `MarbleRollingResistance`, `MarbleAutoStop`

Il codice vive in `Assets/Scripts/`, **non** in questi file. Gli script originali erano stati scritti con ChatGPT e sono in revisione.

## Prossimi passi
1. Editor di piazzamento per il giocatore finale
2. Fisica del lancio: script di lancio touch con New Input System, pista di test con i pezzi esistenti
3. In base ai risultati dei test, decidere se modificare curve e rampe
4. Poi: risolvere la cucitura dei collider (problema 1)

## Per ripartire in una nuova chat
Allega `DECISIONI.md`, `STATO.md` e i file `.cs` coinvolti nel lavoro del momento (completi, non snippet).
