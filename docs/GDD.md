# Marble Crazy Maker — Game Design Document

**Status:** Pre-production / concept in evolution  
**Version:** 0.5  
**Titolo:** Marble Crazy Maker

## 1. Vision

RaceMaker è un mobile game basato sulla costruzione di piste modulari per biglie e su un sistema di fisica interattiva.

Il giocatore costruisce una pista, prepara il tiro della propria biglia tramite direzione e potenza, quindi lascia che la fisica determini il risultato. Collisioni tra biglie, sponde, ostacoli, salti, trappole e altri elementi modificano la situazione di gioco.

Il precedente concept basato sulle automobili è stato abbandonato. `RaceMaker` resta il nome tecnico storico del repository/progetto.

### Core loop

**Costruisci → Prova → Mira → Lancia → Osserva le conseguenze → Adatta la strategia**

## 2. Principi di design

### 2.1 Il giocatore controlla l'impulso, non il risultato

Il gesto fondamentale è:

1. toccare la biglia;
2. trascinarla all'indietro;
3. orientare il tiro;
4. scegliere la potenza tramite la distanza del trascinamento;
5. rilasciare.

La potenza è compresa tra un minimo e un massimo. Il drag è libero in tutte le direzioni e la direzione del lancio è opposta al vettore del drag. La distanza massima del drag è **4 Unity**, cioè una Cella di costruzione; oltre tale distanza la potenza rimane al massimo. Dopo il rilascio il giocatore non controlla direttamente la biglia.

L'obiettivo è ottenere una sensazione simile al biliardo: il giocatore controlla il colpo, mentre il risultato emerge dalla fisica.

### 2.2 Fisica prima degli script

Quando possibile, il risultato deve derivare da:

- direzione;
- potenza;
- velocità;
- angolo;
- punto d'impatto;
- proprietà della superficie;
- geometria della pista.

Non devono essere usati risultati arbitrari quando la fisica può produrre naturalmente l'effetto desiderato.

### 2.3 Catene di collisioni

Una singola azione può generare una sequenza di eventi:

**P1 → P2 → sponda → P3 → trappola**

Le collisioni possono quindi essere multiple e indirette. Idealmente non deve esistere un limite artificiale alla lunghezza della catena; saranno comunque necessari normali meccanismi tecnici di sicurezza per evitare fenomeni patologici della simulazione fisica.

### 2.4 Stato persistente

Dopo ogni tiro, le biglie rimangono nella posizione in cui hanno terminato il movimento. Il turno successivo deve quindi giocare su uno stato della pista modificato dai turni precedenti.

Questo permette strategie come:

- avanzamento sicuro;
- posizionamento tattico;
- bocciata contro un avversario;
- spostamento difensivo;
- tiro rischioso per ottenere una combinazione di collisioni.

Ogni azione rischiosa deve però comportare un rischio fisicamente credibile anche per chi la esegue.

## 3. Modalità di gioco

### 3.1 Prova

Modalità libera per testare una pista senza una struttura competitiva.

### 3.2 Classica

Modalità single-player contro avversari controllati dall'IA. Può evolvere in una struttura arcade/campionato con più piste e sistema di monete.

### 3.3 Multiplayer

Previsto come sviluppo successivo.

Il modello attualmente preferito è **a turni**, con ordine fisso:

**P1 → P2 → … → Pn → P1 → P2 → …**

L'ordine non cambia in base alla posizione delle biglie.

Il giocatore attivo dispone di un tempo limitato per preparare il tiro. Dopo il lancio, la biglia completa la propria traiettoria e tutte le interazioni fisiche vengono risolte prima del turno successivo.

## 3.4 Biglie e lancio

La biglia ha attualmente un **diametro massimo di 0,5 Unity**. Sono previste fino a **4 biglie per corsa**.

Le diverse biglie possono avere aspetti differenti, ma nella fase attuale sono **identiche dal punto di vista fisico**: nessuna differenza di massa, velocità, attrito, rimbalzo o altre caratteristiche di gameplay viene introdotta in base all'estetica.

Il sistema di lancio usa un drag libero in tutte le direzioni. Il giocatore può trascinare il dito in qualsiasi direzione rispetto alla biglia; la biglia viene lanciata nella direzione opposta. La distanza del drag determina la potenza. Il limite massimo è **4 Unity**, pari alla distanza di una Cella dalla biglia. Oltre 4 Unity il drag viene saturato alla potenza massima.

La pista ridotta da **1,8 Unity** è volutamente stretta rispetto alla biglia da 0,5 Unity: le biglie non sono pensate per disporsi comodamente affiancate e la maggiore densità deve favorire collisioni e catene di collisioni.

## 4. Editor delle piste

### 4.0 Cella di costruzione e dimensioni della pista

La **Cella** è l'unità fondamentale della griglia di costruzione ed è sempre **4 × 4 Unity sul piano X/Z**. Le coordinate della cella sono riferite al suo **centro**.

Cella e TrackPiece non coincidono concettualmente: la cella definisce lo spazio di costruzione, mentre il TrackPiece definisce la geometria della pista contenuta in quello spazio.

La pista standard è centrata nella cella e ha:

- superficie percorribile: **2,8 Unity**;
- guard-rail: **0,1 Unity per lato**;
- larghezza complessiva dell'ingombro con guard-rail: **3 Unity**;
- spazio residuo nella cella: **0,5 Unity per lato**.

I TrackPiece ridotti mantengono comunque un ingombro prefab/cella di **4 × 4 Unity**, ma la superficie della pista è più stretta e centrata nella cella:

- superficie percorribile: **1,8 Unity**;
- guard-rail: **0,1 Unity per lato**;
- larghezza complessiva: **2 Unity**.

Lo spazio laterale residuo viene riempito di default in funzione della natura della pista, per esempio con asfalto, erba, sabbia o altro materiale ambientale coerente. La possibilità di aggiungere elementi decorativi rimane prevista; la modalità di placement sarà definita successivamente.

Questa soluzione mantiene una sola griglia di costruzione e rende i TrackPiece ridotti una variante geometrica, non una nuova tipologia di cella.

Gli **adattatori tra larghezza standard e ridotta** sono una categoria separata. È previsto un unico prefab **Adapter 2,8 ↔ 1,8**, con due ConnectionPoint di compatibilità diversa, riutilizzabile tramite rotazione anche nel verso opposto.

L'editor rimane basato su pezzi modulari predefiniti. L'obiettivo è consentire la costruzione di piste complesse senza trasformare l'editor in un sistema di modellazione libero.

Possibili moduli:

- rettilinei;
- curve;
- curve a S;
- incroci;
- biforcazioni;
- raccordi;
- salite/discese;
- rampe;
- salti;
- ponti;
- tunnel;
- loop;
- ostacoli;
- trappole;
- elementi mobili;
- elementi che modificano permanentemente la pista.

### 4.1 ConnectionPoint e snapping

I ConnectionPoint sono collocati al **centro dei lati della cella**. Ogni punto possiede posizione e orientamento **locali rispetto al TrackPiece**; la rotazione del TrackPiece determina automaticamente la posizione e l'orientamento del punto nello spazio di costruzione.

La larghezza della pista non modifica la posizione del ConnectionPoint: sia pista standard sia pista ridotta utilizzano lo stesso riferimento centrale del lato della cella. Lo snapping deve verificare sia l'orientamento risultante sia la compatibilità del tipo di connessione.

Per l'adapter 2,8 ↔ 1,8 non sono necessari due prefab distinti. Un unico prefab possiede un ConnectionPoint compatibile con 2,8 Unity e uno compatibile con 1,8 Unity; ruotandolo di 180° può svolgere la stessa funzione nel verso opposto.

### 4.2 Biforcazioni

Sono previste sia biforcazioni brevi sia due percorsi più distinti che si ricongiungono successivamente.

Le due vie possono avere caratteristiche diverse, per esempio:

- percorso più lungo ma sicuro;
- percorso più corto ma rischioso.

### 4.3 Guardrail

Le barriere laterali/guard-rail sono opzionali in funzione della sezione della pista. Quando presenti, la dimensione è **0,1 Unity per lato** sia nei TrackPiece standard sia nei TrackPiece ridotti. Possono essere inserite solo in alcune sezioni della pista.

Dove non esiste una barriera, una biglia che supera il bordo può essere considerata fuori pista e attivare il respawn.

## 5. Elementi fisici

### 5.1 Elementi di base

- sponde/lati;
- rampe;
- salti;
- superfici con differenti proprietà fisiche.

### 5.2 Turbo a terra

Il turbo è un elemento ambientale della pista, non un power-up tradizionale.

Quando la biglia attraversa la zona, riceve un impulso aggiuntivo nella direzione prevista dal modulo. La forza del boost deve essere sufficiente a creare differenze tattiche senza rendere irrilevante la precisione del tiro.

### 5.3 Molle/pulsanti

Possibili varianti:

- elemento a parete: collisione con impulso maggiore rispetto a una sponda normale;
- elemento a pavimento: salto con traiettoria più curva rispetto a una semplice rampa.

L'effetto può essere implementato come impulso controllato invece di una simulazione fisica completa della molla.

### 5.4 Loop

Il loop deve sfruttare principalmente la fisica.

- velocità sufficiente → completamento del loop;
- velocità insufficiente → perdita di velocità, arresto e possibile ritorno indietro;
- se la biglia raggiunge la zona superiore in una configurazione prevista dal design, una piccola zona di controllo invisibile può rendere coerente l'eventuale caduta sulla pista sottostante.

L'uso di un controllo locale non deve sostituire la fisica generale del loop.

### 5.5 Bombe/TNT

Le bombe sono ostacoli statici che reagiscono alla collisione con un impulso di esplosione.

L'effetto deve essere progettato in modo da non diventare un semplice "boost": l'esplosione deve spostare la biglia prevalentemente all'indietro o lateralmente rispetto alla direzione di percorrenza prevista.

### 5.6 Blocchi mobili

Movimenti semplici e predefiniti:

- sinistra/destra;
- alto/basso;
- eventuali varianti verticali.

Non è previsto inizialmente un sistema per creare traiettorie arbitrarie.

### 5.7 Pendoli

Ostacoli con movimento predefinito e ripetibile. Il giocatore deve imparare a sincronizzare il tiro con il loro movimento.

### 5.8 Barre rotanti

Una o più barre rotanti possono agire come ostacoli fisici. Il collider segue la rotazione e la traiettoria della biglia determina il risultato della collisione.

Eventuali varianti possono essere ottenute tramite configurazioni dello stesso modulo invece di creare categorie separate nell'editor.

## 6. Trappole e modifiche della pista

### 6.1 Buchi

Un buco è una trappola permanente. La biglia che vi cade viene respawnata in un punto definito dalla pista.

### 6.2 Fuori pista

Una biglia che supera il bordo in una sezione senza guardrail può essere considerata fuori pista e respawnata.

### 6.3 Porte

Una porta può creare un passaggio/shortcut che viene chiuso dopo il primo attraversamento. La durata esatta della chiusura rimane da definire.

### 6.4 Ponti levatoi

Un ponte può alternare stati aperto/chiuso a intervalli regolari. A seconda della posizione durante il passaggio può funzionare come:

- normale attraversamento;
- rampa;
- ostacolo.

### 6.5 Ponte instabile

Il primo attraversamento provoca il collasso del ponte. Dopo il collasso il passaggio rimane inutilizzabile.

Una biglia che cade viene respawnata in prossimità dell'elemento, ma sulla pista principale.

Nella prima implementazione il ponte non deve essere simulato pezzo per pezzo: è sufficiente una transizione controllata tra stato integro e stato collassato, con animazione e disattivazione del collider.

### 6.6 Buchi nascosti

**Scartati.** Senza un sistema specifico di rilevamento, rischiano di rendere il gioco imprevedibile e frustrante invece di premiare abilità e lettura della pista.

## 7. Respawn

Il respawn avviene in un punto definito dalla pista o associato alla trappola/alla sezione interessata.

Esempi:

- davanti al buco;
- vicino al bordo dal quale la biglia è uscita;
- prima di un ponte levatoio;
- vicino a una porta ma sulla via principale.

La penalità dovrebbe essere soprattutto **posizionale**, evitando tempi morti eccessivi.

## 8. Micro-missioni e monete

Le micro-missioni hanno lo scopo di evitare che la strategia dominante sia sempre "gioca sicuro e usa meno tiri possibile".

Le missioni possono cambiare da una partita all'altra in base agli elementi disponibili sulla pista.

Esempi:

- colpire almeno due avversari in sequenza;
- creare una catena di almeno tre collisioni;
- mandare un avversario in una trappola;
- colpire dopo un rimbalzo;
- espellere una biglia dal tracciato;
- provocare una collisione indiretta.

Le missioni sono incentivi e non necessariamente obiettivi obbligatori.

### 8.1 Ricompense dinamiche

Ogni missione può avere un valore base. Il premio effettivo può tenere conto di:

- difficoltà;
- rischio;
- concatenazione con altri eventi;
- eventuale moltiplicatore di catena.

Formula concettuale ancora da definire:

`reward = base × difficulty × risk × chain_multiplier`

Il risultato finale deve essere espresso come numero intero di monete.

## 9. Power-up tradizionali

**Rimossi dal concept attuale.**

Non sono previsti scudi, missili, teletrasporti, magneti o altri power-up attivati direttamente dal giocatore. Il sistema di fisica e gli elementi della pista devono essere sufficienti a generare varietà.

Il turbo rimane un elemento ambientale della pista.

## 10. Eventi ambientali casuali

Eventi come fulmini che colpiscono casualmente la pista e influenzano i giocatori senza dipendere dalle loro azioni sono stati rimossi come meccaniche di gameplay.

Possono eventualmente essere utilizzati come effetti visivi o atmosferici privi di conseguenze sul gameplay.

## 11. AI

La modalità Classica richiederà avversari controllati dall'IA.

L'IA dovrà essere in grado di:

- scegliere un tiro;
- considerare posizione e direzione;
- interagire con gli elementi della pista;
- tenere conto delle altre biglie;
- eventualmente perseguire micro-missioni.

L'obiettivo iniziale non è un'IA perfetta, ma un comportamento credibile e divertente.

## 12. Salvataggio e condivisione

Le piste devono essere rappresentate tramite dati leggeri basati sui moduli utilizzati e sui relativi parametri, anziché dipendere da grandi scene monolitiche.

Questo faciliterà in futuro:

- salvataggio locale;
- caricamento;
- validazione;
- condivisione delle piste;
- eventuale multiplayer online.

## 13. Sviluppo incrementale

### V1 — Fisica fondamentale

- pista;
- sponde;
- uscita dal tracciato;
- buchi;
- rampe;
- salti;
- 1–2 superfici;
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
- rifinitura audiovisiva;
- eventuali nuove interazioni fisiche.

## 14. Prototipo prioritario

Prima di costruire un editor complesso, bisogna verificare che il nucleo fisico sia già divertente.

Il test fondamentale è:

**trascina → mira → rilascia → la biglia colpisce qualcosa → rimbalza → colpisce un'altra biglia → osserva il risultato.**

Se questo non è soddisfacente, aggiungere contenuti non risolverà il problema.

## 15. Tecnologie

- Unity 6.3 LTS.
- Target iniziale: mobile.
- Architettura modulare per le piste.
- Dati delle piste leggeri e potenzialmente condivisibili.

## 16. Stato e decisioni aperte

**Stato:** Pre-produzione / concept in evoluzione.

Decisioni ancora aperte:


- visual style e prospettiva/presentazione definitiva;
- numero e dimensioni dei moduli iniziali;
- catalogo definitivo dei TrackPiece ridotti;
- geometria, occupazione e comportamento degli adattatori tra larghezza standard e ridotta;
- definizione dello sfondo/superficie di riempimento nelle aree della cella non occupate dalla pista;
- modello esatto delle superfici fisiche;
- forza dei turbo e delle molle;
- regole precise di porte e ponti levatoi;
- durata del turno multiplayer;
- struttura definitiva delle micro-missioni e delle ricompense;
- modello di IA;
- struttura della modalità Classica;
- criteri di validazione delle piste.

`RaceMaker` resta il nome tecnico storico del repository; il titolo di riferimento del gioco è **Marble Crazy Maker**.
