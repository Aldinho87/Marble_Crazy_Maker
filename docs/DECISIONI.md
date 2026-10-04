# DECISIONI — Marble Crazy Maker

Documento delle decisioni prese. Se una decisione cambia, **sostituisci la riga** e annota la data: non accumulare versioni contrarie.

> Nome tecnico storico del repo: `RaceMaker`. Titolo del gioco: **Marble Crazy Maker**.
> Ricostruito da appunti riassuntivi: controlla i valori contro il progetto Unity prima di fidarti ciecamente.

## Ambiente
- Unity **6.3 LTS**, target **mobile**
- Active Input Handling: **Input System Package (New)**
- Griglia: Simple Grid Framework installato (MAST abbandonato per errore di "distruzione" dell'oggetto)
- Construct 3 scartato: si vuole un gioco **3D**

## Concept
- Gioco mobile di costruzione piste per biglie, basato sulla fisica
- Loop: Costruisci → Prova → Mira e lancia → Osserva la fisica → Adatta la strategia → Ripeti
- Lancio con gesto touch: trascinamento e rilascio, la distanza determina la potenza
- Nessun controllo della traiettoria dopo il lancio
- Le biglie restano dove si fermano: lo stato della pista è persistente tra i turni

## Modalità
- **Prova**: libera
- **Classica**: single-player contro IA, con monete
- **Multiplayer**: turni fissi P1→P2→…→Pn, tempo limitato per tiro
- Corse da **4 biglie**

## Principi di design
- Fisica prima degli script
- Il giocatore controlla l'impulso, non il risultato
- Ogni rischio ha una conseguenza fisicamente motivata
- Niente casualità gratuita
- Niente power-up tradizionali (il turbo a terra è un elemento ambientale)
- Pista ridotta pensata per **non far passare 4 biglie insieme**: il blocco è una meccanica voluta (tiro "di bocciata" che può aiutare o penalizzare l'avversario)

## Valori fissati
| Parametro | Valore |
|---|---|
| Diametro biglia | 0.5 |
| trackWidth standard | 2.4 |
| trackWidth ridotta | 1.6 |
| cellSize | 4 |
| Livelli Y | **0, +1, +2** (decisione più recente; sostituisce 5 livelli e poi -1/0/+1) |
| Dislivello massimo tra celle collegate | 1 livello |

I valori vecchi del README (cellSize 4, trackWidth 2.8/1.8) **non** sono vincolanti: i parametri si possono ridiscutere.

## Griglia e pezzi
- Griglia discreta 3D, coordinate cella riferite al centro
- Connessione cardinale N/E/S/O, `ConnectionPoint` locali che ruotano col pezzo
- Il giocatore può **ruotare di 90°** e **specchiare** i pezzi (meno prefab curva necessari)
- Rampe: **4 pezzi dedicati** (2 dritte, 2 curve), non generate automaticamente tra pezzi con ΔY=1
- Pezzi base già creati come prefab (via `TrackPieceGenerator`, profilo di salita smoothstep): rettilineo, curva 90° (+mirrored), rampa dritta, rampa curva 90° (+mirrored)
- Guardrail opzionali (`generateGuardrails`): il fuoripista è una meccanica (malus con respawn, o bonus/scorciatoia se la pista scende di livello)

## Camera
- **Prova**: camera libera
- **Classica/Multiplayer**: camera automatica (segui / dall'alto), al massimo un tasto per alternare, niente selezione libera nei turni a tempo
- Segue la biglia che sta tirando; se si ferma prima delle altre, passa all'ultima biglia ancora in movimento causata dal suo tiro (fallback: dall'alto se troppo complesso)

## Pubblicazione piste
- Pipeline: CREATE → TEST → VERIFY → PUBLISH
- Limite di tiri per validare la completabilità prima della pubblicazione

## Roadmap
- V1 fisica fondamentale
- V2 interazioni (turbo, molle, loop, bombe)
- V3 editor e stato pista (biforcazioni, porte, ponti)
- V4 contenuti avanzati e rifinitura

## Idee future (non decise)
- Skin sbloccabili per le biglie (motivi ispirati agli anni '90, originali per evitare problemi di copyright), forse anche per pezzi speciali
- Pezzo speciale a S (chicane), forse su 2 celle
- "Versione 2.0" con più livelli Y

## Calibrazioni fisiche attuali
- `MarbleRollingResistance`: Rolling Resistance Multiplier = 2
- `MarbleAutoStop`: Linear Velocity Threshold = 0.15, Angular Velocity Threshold = 0.3, Time Below Threshold = 0.15
- Materiale superficie: `SurfaceData_Asphalt` / `Asphalt_Material`, friction e bounciness 0.5
- Default Contact Offset: provato 0.03 (non risolve la cucitura tra collider)
- Solver Iterations: **default 6/1** (provato 12: biglia immobile ovunque, riportato a default)
