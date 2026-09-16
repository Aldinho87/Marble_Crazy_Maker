# RaceMaker — Roadmap

**Status:** Pre-produzione  
**Titolo definitivo:** da definire

> `RaceMaker` è attualmente il nome tecnico provvisorio del repository/progetto. Il concept precedente basato sulle automobili è stato sostituito da un gioco di costruzione e gioco con biglie fisiche.

## Phase 0 — Fondazione del concept

- [x] Creare repository GitHub
- [x] Definire il nuovo concept basato sulle biglie
- [x] Abbandonare il precedente concept automobilistico
- [x] Definire il gesto di lancio manuale
- [x] Definire il principio "il giocatore controlla l'impulso, non il risultato"
- [x] Definire collisioni concatenate e stato persistente delle biglie
- [x] Definire modalità Prova / Classica / Multiplayer
- [x] Definire modello multiplayer a turni come direzione attuale
- [x] Definire editor modulare
- [x] Definire elementi fisici principali
- [x] Definire trappole e respawn
- [x] Scartare power-up tradizionali
- [x] Scartare eventi ambientali casuali come meccaniche di gameplay
- [x] Definire concept di micro-missioni e monete
- [x] Definire sviluppo incrementale del prototipo

## Phase 1 — Prototipo fisico fondamentale

### Setup

- [ ] Creare/configurare progetto Unity 6.3 LTS
- [ ] Configurare target mobile
- [ ] Stabilire struttura delle cartelle
- [ ] Configurare input touch
- [ ] Creare scena di test

### Biglia e lancio

- [ ] Creare biglia fisica
- [ ] Implementare interazione touch
- [ ] Implementare trascinamento all'indietro
- [ ] Collegare distanza del trascinamento alla potenza
- [ ] Definire potenza minima/massima
- [ ] Implementare orientamento del tiro
- [ ] Implementare animazione/feedback del gesto di lancio
- [ ] Testare feeling del lancio

### Fisica di base

- [ ] Implementare pista semplice
- [ ] Implementare sponde
- [ ] Implementare collisioni biglia-bigia
- [ ] Implementare collisioni biglia-ambiente
- [ ] Implementare uscita dal tracciato
- [ ] Implementare buchi
- [ ] Implementare respawn
- [ ] Implementare rampe
- [ ] Implementare salti
- [ ] Implementare 1–2 superfici con proprietà differenti

### Primo test fondamentale

- [ ] Verificare che "trascina → lancia → collisione → rimbalzo → seconda collisione" sia già divertente
- [ ] Correggere il feeling fisico prima di aggiungere contenuti

## Phase 2 — Interazioni fisiche

- [ ] Implementare turbo a terra
- [ ] Implementare molle/pulsanti
- [ ] Implementare loop
- [ ] Implementare bombe/TNT
- [ ] Implementare blocchi mobili
- [ ] Implementare pendoli
- [ ] Testare catene di collisioni multiple
- [ ] Verificare stabilità della simulazione fisica

## Phase 3 — Editor e stato della pista

### Editor base

- [ ] Definire sistema di moduli e punti di connessione
- [ ] Implementare rettilinei
- [ ] Implementare curve
- [ ] Implementare curve a S
- [ ] Implementare raccordi
- [ ] Implementare start/finish
- [ ] Implementare placement e rotazione
- [ ] Implementare validazione della pista
- [ ] Implementare modalità Test
- [ ] Implementare salvataggio/caricamento locale

### Editor avanzato

- [ ] Implementare biforcazioni
- [ ] Implementare percorsi alternativi
- [ ] Implementare guardrail opzionali
- [ ] Implementare porte
- [ ] Implementare ponti levatoi
- [ ] Implementare ponti instabili
- [ ] Implementare barre rotanti

## Phase 4 — Modalità Classica

- [ ] Definire struttura della progressione
- [ ] Implementare avversari IA
- [ ] Implementare sistema di tiro dell'IA
- [ ] Implementare interazione dell'IA con altre biglie
- [ ] Implementare condizioni di gara
- [ ] Implementare micro-missioni dinamiche
- [ ] Implementare sistema di ricompense in monete
- [ ] Testare moltiplicatori di catena

## Phase 5 — Multiplayer

- [ ] Definire protocollo del turno
- [ ] Definire tempo massimo di preparazione del tiro
- [ ] Implementare ordine fisso dei giocatori
- [ ] Implementare risoluzione completa della traiettoria prima del turno successivo
- [ ] Verificare sincronizzazione della fisica
- [ ] Implementare stanze/private match
- [ ] Implementare matchmaking
- [ ] Definire comportamento IA per slot mancanti, se confermato

## Phase 6 — Condivisione e community

- [ ] Pubblicazione delle piste
- [ ] Ricerca/discovery
- [ ] Codici di condivisione
- [ ] Profili creatore
- [ ] Like/rating
- [ ] Moderazione
- [ ] Piste del giorno
- [ ] Eventuali categorie/community challenges

## Phase 7 — Polish e contenuti

- [ ] Definire visual style definitivo
- [ ] Audio e musica
- [ ] Feedback visivi delle collisioni
- [ ] Animazioni
- [ ] Ulteriori moduli di pista
- [ ] Ulteriori ostacoli/trappole
- [ ] Ottimizzazione mobile
- [ ] Test di usabilità

## Milestone principali

### Milestone A — Fun Physics

Una singola biglia può essere lanciata con precisione sufficiente e interagire con pista e altre biglie in modo soddisfacente.

### Milestone B — Fun Track

È possibile costruire una pista semplice, provarla e ottenere un'esperienza divertente senza necessità di contenuti avanzati.

### Milestone C — Emergent Gameplay

Collisioni concatenate e stato persistente producono situazioni tattiche interessanti.

### Milestone D — Playable Game

La modalità Classica con IA, missioni e ricompense è giocabile dall'inizio alla fine.

### Milestone E — Community Game

Le piste possono essere condivise e giocate da altri utenti.
