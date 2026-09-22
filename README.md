# Marble Crazy Maker

**Titolo del gioco:** Marble Crazy Maker  
**Nome tecnico storico del repository:** RaceMaker

Mobile game di costruzione e gioco su piste modulari con **biglie** e fisica interattiva. Il cuore dell'esperienza è costruire una pista, scegliere traiettoria e potenza di lancio e osservare come collisioni, ostacoli e elementi della pista modificano fisicamente la situazione di gioco.

## Concept

Il progetto ha abbandonato il precedente concept basato sulle automobili. La nuova identità di gioco è centrata sulle biglie:

**Costruisci → Prova → Mira e lancia → Osserva la fisica → Adatta la strategia → Ripeti**

Il giocatore non guida direttamente la biglia dopo il lancio. Controlla principalmente **direzione e potenza dell'impulso**; il risultato viene determinato dalla fisica, dalle collisioni e dalla configurazione della pista.

## Core gameplay

- Biglia fisica con collisioni tra biglie e ambiente.
- Lancio manuale tramite gesto touch: trascinamento all'indietro e rilascio.
- La distanza del trascinamento determina la potenza, entro limiti min/max.
- La traiettoria successiva al lancio non è controllata direttamente.
- Collisioni multiple e concatenate, sul modello concettuale del biliardo.
- Le conseguenze di un tiro possono coinvolgere più biglie e più elementi della pista.
- Le biglie rimangono nella posizione in cui terminano il movimento.
- Il turno successivo eredita quindi uno stato della pista modificato dai tiri precedenti.

## Modalità previste

### Prova

Modalità per testare liberamente una pista senza struttura competitiva.

### Classica

Modalità single-player con avversari controllati dall'IA, potenzialmente organizzata in progressione/arcade e con sistema di monete.

### Multiplayer

Modalità prevista in prospettiva con turni alternati. L'ordine dei giocatori rimane fisso per tutta la gara:

**P1 → P2 → … → Pn → P1 → P2 → …**

Ogni giocatore dispone di un tempo limitato per preparare il proprio tiro. Dopo il lancio, la biglia completa la propria traiettoria e tutte le interazioni fisiche vengono risolte prima del turno successivo.

## Principi di design

- **Fisica prima degli script:** quando possibile, il risultato deve derivare da direzione, potenza, angolo, punto d'impatto e velocità, non da risultati predefiniti.
- **Il giocatore controlla l'impulso, non il risultato:** il rischio nasce dalla difficoltà di prevedere perfettamente le conseguenze fisiche.
- **Ogni rischio deve avere una conseguenza fisicamente motivata:** una bocciata può danneggiare la posizione di un avversario, ma un errore può danneggiare anche chi la esegue.
- **Stato persistente della pista:** le biglie non vengono automaticamente riposizionate dopo ogni tiro.
- **Semplicità:** costruire piste e giocare devono essere entrambi intuitivi.
- **Niente casualità gratuita:** gli elementi casuali non devono sostituire la fisica o trasformare il risultato in una lotteria.

## Stato tecnico attuale dell'editor

La prima infrastruttura Unity dell'editor è stata aggiornata e verificata:

- griglia **20 × 20 celle** sul piano X/Z;
- **1 cella = 4 × 4 Unity**;
- cinque livelli Y: **−2, −1, 0, +1, +2**;
- passo verticale: **2 Unity**;
- livello attivo selezionabile tramite `YLevelSelector`;
- livelli adiacenti visualizzati nella Scene View con trasparenza ridotta;
- connessioni tra ConnectionPoint valide per ΔY = 0/±1;
- blocco dello spostamento che porterebbe una connessione a ΔY > 1;
- disconnessione automatica quando viene perso l'allineamento X/Z;
- UI laterale in Screen Space - Overlay, indipendente dalla futura camera dell'editor.

I relativi script sono versionati nel repository sotto `Assets/MarbleRaceMaker/Scripts` e `Assets/MarbleRaceMaker/Editor`.

Il prossimo sviluppo dell'editor riguarda selezione/spostamento dei TrackPiece, placement mobile, camera touch e validazione completa della pista.

## Editor delle piste

L'editor è basato su una **griglia discreta tridimensionale**: X e Z definiscono la posizione sul piano, mentre Y definisce un livello di altezza discreto.

### Griglia di costruzione

- **1 cella = 4 × 4 Unity** sul piano X/Z.
- Le coordinate della cella sono riferite al **centro della cella**, scelta adottata per semplificare placement, rotazione e snap.
- La cella rimane sempre 4 × 4 Unity anche quando contiene un TrackPiece ridotto: in questo caso è la **geometria della pista** a essere ridotta, non la dimensione della cella.
- La dimensione X/Z della cella e il passo verticale Y sono indipendenti.
- **1 livello Y = 2 Unity** di altezza.
- Livelli disponibili inizialmente: **Y = −2, −1, 0, +1, +2**.
- Il dislivello tra due celle direttamente collegate non può superare **1 livello Y**.

### Larghezza della pista e TrackPiece ridotti

La pista standard occupa l'intera larghezza della cella:

- superficie percorribile: **2,8 Unity**;
- guard-rail: **0,1 Unity per lato**;
- larghezza complessiva: **3 Unity**, con 0,5 Unity di spazio residuo per lato nella cella.

I TrackPiece ridotti mantengono invece la stessa dimensione del prefab/cella **4 × 4 Unity**, ma la superficie della pista è più stretta e centrata nella cella:

- superficie percorribile: **1,8 Unity**;
- guard-rail: **0,1 Unity per lato**;
- larghezza complessiva della pista: **2 Unity**.

Lo spazio laterale residuo nella cella viene riempito di default in funzione della natura della pista (per esempio asfalto, erba, sabbia, ecc.). Potrà inoltre essere utilizzato per elementi decorativi, la cui modalità di placement sarà definita successivamente.

Questa soluzione evita una seconda griglia di celle ridotte e mantiene uniforme la logica di placement: la cella resta sempre 4 × 4 Unity.

Gli eventuali **adattatori di larghezza** sono una categoria distinta dal catalogo standard. È previsto un unico adapter **2,8 ↔ 1,8**, riutilizzabile tramite rotazione del TrackPiece.

### Sistema di connessione

Le connessioni sono cardinali: **N, E, S, W**. I TrackPiece ruotano di 90°.

Ogni ConnectionPoint è definito tramite **posizione e orientamento locali rispetto al TrackPiece**. Quando il TrackPiece ruota, posizione e orientamento dei ConnectionPoint ruotano con esso. L'orientamento non è quindi assoluto nel mondo.

Catalogo base:
- **rettilineo:** 1 cella;
- **varianti ridotte:** TrackPiece 4 × 4 Unity con pista larga la metà, centrata nella cella;
- **curva 90°:** 1 cella;
- **biforcazione:** 1 cella, 1 ingresso + 2 uscite;
- **incrocio +:** 1 cella;
- **ponte:** 3 celle, Y iniziale +1;
- **loop:** 3 celle, Y iniziale +2.

Non servono prefab separati per ogni variante di salita/discesa: la quota delle celle determina il raccordo.

### ConnectionPoint e adattatore di larghezza

I ConnectionPoint sono posizionati al **centro dei lati della cella** e seguono la trasformazione del TrackPiece. La larghezza della pista non modifica la posizione del ConnectionPoint.

L'adapter tra pista standard e ridotta utilizza un unico prefab con due ConnectionPoint compatibili rispettivamente con **2,8** e **1,8 Unity**. Ruotandolo di 180° lo stesso prefab può collegare la pista ridotta a quella standard nel verso opposto, senza creare un secondo adapter.

### Raccordo verticale automatico

- **ΔY = 0** → collegamento piano;
- **ΔY = +1** → salita;
- **ΔY = −1** → discesa;
- **|ΔY| > 1** → collegamento non valido.

Con cella da 4 Unity e passo Y da 2 Unity, un dislivello di un livello corrisponde a 2 Unity verticali su 4 Unity orizzontali. Il valore dovrà essere verificato nel prototipo.

### Vincoli di costruzione

Il sistema dovrà validare compatibilità delle connessioni, ΔY, occupazione 3D, spazio verticale libero, collisioni e vincoli specifici di ponte/loop. I parametri devono restare configurabili.

L'editor rimane modulare e basato su pezzi predefiniti, ma viene ripensato per le biglie.

Possibili categorie:

- rettilinei;
- curve;
- curve a S;
- incroci;
- biforcazioni;
- salite e discese;
- rampe;
- salti;
- ponti;
- tunnel;
- loop;
- ostacoli;
- trappole;
- elementi mobili;
- elementi che modificano permanentemente lo stato della pista.

Le biforcazioni possono creare percorsi alternativi che si ricongiungono successivamente, con possibili differenze tra percorso sicuro e percorso più breve/rischioso.

Le **barriere laterali/guardrail sono opzionali**: possono essere inserite dall'autore della pista solo in alcune sezioni. Dove non sono presenti, uscire dal tracciato può causare una caduta e il respawn.

## Prova e pubblicazione delle piste

La modalità **Prova** deve distinguere tra validità tecnica e verifica di giocabilità. Il creator deve riuscire a completare la pista entro un limite di tiri stabilito prima che possa essere candidata alla pubblicazione.

Pipeline concettuale: **CREATE → TEST → VERIFY → PUBLISH**.

La verifica dimostra che la pista è fisicamente completabile; il divertimento/qualità possono essere valutati successivamente attraverso l'esperienza dei giocatori.

## Elementi fisici e interattivi

Elementi già considerati per il progetto:

- lati e sponde;
- rampe;
- salti;
- superfici con proprietà fisiche differenti;
- turbo a terra, che applica un impulso aggiuntivo quando la biglia attraversa l'area;
- molle/pulsanti a parete o a pavimento, preferibilmente implementati come impulsi controllati;
- loop verticali;
- bombe/TNT come ostacoli che applicano un impulso di esplosione prevalentemente all'indietro o lateralmente;
- blocchi mobili con movimenti semplici predefiniti;
- pendoli con movimento predefinito;
- barre rotanti con collider fisico;
- buchi/trappole;
- porte che possono chiudere un passaggio dopo il primo attraversamento;
- ponti levatoi;
- ponti instabili che collassano dopo il primo attraversamento.

### Loop

Un loop deve essere basato principalmente sulla fisica: velocità sufficiente permette di completarlo; velocità insufficiente può causare arresto in salita e ritorno all'indietro. Nel caso particolare in cui la biglia raggiunga la zona superiore, può essere usata una piccola area di controllo invisibile per rendere coerente il comportamento e permettere, se previsto dal design, la caduta sulla pista sottostante.

### Ponti instabili

Un ponte instabile può essere attraversato una sola volta: il primo attraversamento attiva il collasso, dopo il quale il passaggio rimane inutilizzabile. Una biglia che cade viene respawnata in prossimità dell'elemento ma sulla pista principale.

## Trappole e respawn

Le trappole possono comprendere:

- buchi;
- cadute oltre il bordo del tracciato;
- sezioni rese inutilizzabili da elementi permanenti;
- altre situazioni definite dalla pista.

Il respawn avviene in un punto predefinito associato all'elemento o alla sezione della pista. La penalità deve essere principalmente **posizionale**, evitando tempi morti eccessivi.

I **buchi nascosti/coperti sono stati scartati** come meccanica di gameplay: senza un sistema specifico di rilevamento rischiano di rendere il gioco imprevedibile anziché basato sull'abilità.

## Micro-missioni e monete

Per evitare che la strategia dominante sia semplicemente avanzare in sicurezza con il minor numero possibile di tiri, la modalità Classica può utilizzare micro-missioni dinamiche.

Esempi:

- colpire almeno due avversari in sequenza;
- creare una catena di almeno tre collisioni;
- mandare un avversario in una trappola;
- colpire un avversario dopo un rimbalzo;
- espellere una biglia dal tracciato;
- provocare una collisione indiretta.

Le missioni sono incentivi, non necessariamente obiettivi obbligatori.

Ogni obiettivo può avere un valore base in monete. Il premio effettivo potrà tenere conto di difficoltà, rischio e concatenazione degli eventi, con un eventuale moltiplicatore di catena. I valori finali devono essere interi.

Formula concettuale ancora da definire:

`reward = base × difficulty × risk × chain_multiplier`

## Power-up tradizionali

I power-up tradizionali sono stati **rimossi dal concept attuale**. Il gioco non necessita di scudi, missili, teletrasporti o altri effetti attivati dal giocatore: il sistema di interazioni fisiche della pista deve essere il principale generatore di varietà.

Il **turbo a terra** rimane invece un elemento ambientale della pista e non un power-up tradizionale.

## Eventi ambientali

Eventi casuali di area che influenzano tutti i giocatori indipendentemente dalle loro azioni, come fulmini che colpiscono casualmente la pista, sono stati rimossi come meccaniche di gameplay perché rischiano di introdurre casualità non legata alle decisioni del giocatore.

Effetti simili possono eventualmente essere mantenuti come **elementi puramente estetici/atmosferici**.

## Sviluppo incrementale

### V1 — Fisica fondamentale

- pista;
- sponde;
- uscita dal tracciato;
- buchi;
- rampe;
- salti;
- 1–2 tipi di superficie;
- collisioni biglia-bigia e biglia-ambiente;
- respawn.

### V2 — Interazioni

- turbo;
- molle;
- loop;
- bombe;
- blocchi mobili;
- pendoli.

### V3 — Editor e stato della pista

- biforcazioni;
- porte;
- ponti levatoi;
- guardrail opzionali;
- ponti instabili;
- combinazione libera dei moduli.

### V4 — Contenuti avanzati

- barre rotanti;
- ulteriori ostacoli e trappole;
- elementi fisici avanzati;
- rifinitura audiovisiva.

## Prototipo prioritario

Prima di costruire un editor complesso, il progetto deve verificare che il nucleo sia già divertente:

**trascina → mira → rilascia → la biglia colpisce qualcosa → rimbalza → colpisce un'altra biglia → osserva il risultato.**

Se questa interazione non è soddisfacente, aggiungere numerosi elementi di pista non risolverà il problema.

## Tecnologie

- Unity 6.3 LTS.
- Target iniziale: mobile.
- Architettura basata su moduli di pista e dati leggeri, con possibilità futura di salvataggio e condivisione delle piste.

## Stato del progetto

**Pre-produzione — concept in evoluzione.**

Il titolo di riferimento del gioco è ora **Marble Crazy Maker**. `RaceMaker` rimane il nome tecnico storico del repository e del progetto.

Il prossimo obiettivo tecnico immediato è completare il placement dell'editor e la camera touch, mantenendo separata la successiva validazione completa della pista dal semplice posizionamento dei moduli.
