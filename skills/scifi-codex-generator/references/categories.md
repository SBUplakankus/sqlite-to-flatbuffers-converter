# Codex Category Hierarchy — The Veil Compact

Structure mirrors Mass Effect 2 exactly:
- **Categories** — flat list, navigation only
- **Primary entries** — `entry_type = 0`, `parent_entry_id = NULL`
- **Secondary entries** — `entry_type = 1`, `parent_entry_id = [primary id]`

Total target: **400–500 entries**

---

## ID Ranges

| Category | ID | Primary ID range | Secondary ID range |
|----------|----|------------------|--------------------|
| Aliens | 1 | 1000–1099 | 1100–1999 |
| Humanity | 2 | 2000–2099 | 2100–2999 |
| Government | 3 | 3000–3099 | 3100–3999 |
| Military | 4 | 4000–4099 | 4100–4999 |
| Technology | 5 | 5000–5099 | 5100–5999 |
| Worlds | 6 | 6000–6099 | 6100–6999 |
| History | 7 | 7000–7099 | 7100–7999 |
| Culture | 8 | 8000–8099 | 8100–8999 |
| Economy | 9 | 9000–9099 | 9100–9999 |
| The Hollow | 10 | 10000–10099 | 10100–10999 |

Primary entries increment by 10 (1000, 1010, 1020…) leaving room for secondaries in between if needed.
Secondary entries start at [primary_id + 1] and increment by 1.

---

## Category 1: Aliens  (ID: 1)

| Primary Entry | Primary ID | Secondaries |
|--------------|------------|-------------|
| Syrathi | 1000 | Biology and Physiology · Society and the Matriarchy · Resonance and Culture · The Three Life Phases · Neural-Bond Reproduction |
| Vaekar | 1010 | Biology and Physiology · The Hierarchy System · Facial Markings and Colony Culture · Dextro-Amino Physiology · Vaekar and the Wasting Curse |
| Keth | 1020 | Biology and Physiology · The Data Union and House System · Intelligence Culture · Lifespan and the Culture of Urgency |
| Durakh | 1030 | Biology and Physiology · The Wasting Curse · Clan Culture and the Kor'vath · The Bombardment of Dur'ka · Durakh Mercenary Culture |
| Nomari | 1040 | The Immune Deficiency · Life Aboard the Wandering Armada · Nomari Engineering Culture · Nomari-Construct Relations |
| Constructs | 1050 | Origins and the Nomari Uprising · Consensus Architecture · Independent Platforms · Legal Status in the Compact |
| Raeth | 1060 | Biology and Society · The Raeth Slave Trade |
| Oshari | 1070 | Biology and Environment · Oshari and the Credit System |
| Vesk | 1080 | Biology and Communication · Vesk Diplomacy |
| Chora | 1090 | Biology and Environment · Architect Theology |
| Veld | 1095 | Eidetic Memory · Veld-Chora Arrangement |

**Target: ~55–65 entries**

---

## Category 2: Humanity  (ID: 2)

| Primary Entry | Primary ID | Secondaries |
|--------------|------------|-------------|
| Terran Hegemony | 2000 | Council of Ministers · Hegemony Parliament · The Hundred Systems Initiative |
| Hegemony Military | 2010 | Hegemony Fleet Command · Hegemony Combined Arms · Special Operations: Null Lancers |
| Hegemony Intelligence | 2020 | HID Structure · Known Operations · The Black Veil Programme |
| Solari History | 2030 | Pre-FTL History · Discovery of the Sol Gate · The Sundering (Solari perspective) |
| Solari Culture | 2040 | Philosophy and Religion · Arts and Expression · Sport and Recreation |
| Solari Colonies | 2050 | The Reach Frontier · New Providence · Colony Tensions |
| The Ascendancy | 2060 | Founding Ideology · Known Operations · Project Null-Born · The Ascendancy and the Hollow |

**Target: ~35–45 entries**

---

## Category 3: Government  (ID: 3)

| Primary Entry | Primary ID | Secondaries |
|--------------|------------|-------------|
| The Spire | 3000 | History and Architect Origins · Districts and Layout · The Foundry District · Population and Economy |
| The Accord Council | 3010 | Structure and Voting · The Solari Junior Seat · Council Crises |
| The Arbiters | 3020 | Role and Authority · Selection and Training · Known Operations (Redacted) |
| Accord Law | 3030 | The Intelligence Prohibition · Accord Resonance Authority · Key Treaties |
| Accord Agencies | 3040 | Archaeological Directorate · Medical Directorate · Diplomatic Service |
| Non-Member Relations | 3050 | Durakh Non-Membership · Construct Quarantine · Raeth Sanctions |

**Target: ~30–40 entries**

---

## Category 4: Military  (ID: 4)

| Primary Entry | Primary ID | Secondaries |
|--------------|------------|-------------|
| Void Gate Combat | 4000 | Approach Tactics · Gate Denial Operations · Ambush at the Threshold |
| Warship Classes | 4010 | Dreadnoughts · Cruisers and Frigates · Fighter Wings · Nomari Vessels |
| Ship Weapons | 4020 | Mass Driver Batteries · Null-Cannons · Point Defence Systems |
| Ship Defences | 4030 | Kinetic Barrier Architecture · Ablative Plating · Electronic Warfare |
| Ground Combat | 4040 | Infantry Doctrine · Resonant Squad Integration · Urban Siege Tactics |
| Species Militaries | 4050 | Vaekar Hierarchy Forces · Hegemony Combined Arms · Syrathi Commando Units · Durakh Raider Tactics |
| Mercenary Organisations | 4060 | Iron Covenant · Null Cartel · Durakh Raiders |
| Personal Weapons | 4070 | Pistols · Assault Rifles · Sniper Platforms · Resonance-Amp Weapons · Heavy Weapons |
| Armour and Equipment | 4080 | Shieldtech · Combat Suits · Tactical Gear · Resonance Dampeners |

**Target: ~50–65 entries**

---

## Category 5: Technology  (ID: 5)

| Primary Entry | Primary ID | Secondaries |
|--------------|------------|-------------|
| Null-Matter | 5000 | Physics and Properties · Aethite Mining · Industrial Applications · Weaponised Null-Matter |
| Void Gates | 5010 | Gate Mechanics · Known Gate Network · Gate Failure Events · The Keth'Sa Incident |
| Resonance | 5020 | Neural Bonding Biology · Training and Academies · Combat Applications · Resonance Law |
| Propulsion | 5030 | In-System Drives · FTL Transit Physics · The Dark Between Gates |
| AI and Synthetics | 5040 | Echo-AI Definition and Law · True AI History · The Intelligence Prohibition Incident |
| Architect Technology | 5050 | Overview · The Spire Systems · Known Relics · Relic Excavation Ethics |
| Medical Technology | 5060 | Battlefield Stabilisers · Neural Reconstruction · Cross-Species Medicine · Stasis Technology |
| Communications | 5070 | Quantum Entanglement Comms · Subspace Relay Network · Encryption Standards |
| Shieldtech | 5080 | Consumer Grade · Military Grade · Resonant Shield Interaction |

**Target: ~50–65 entries**

---

## Category 6: Worlds  (ID: 6)

| Primary Entry | Primary ID | Secondaries |
|--------------|------------|-------------|
| Terra | 6000 | Sol System · Hegemony Capital · Culture and Population |
| Syrath Prime | 6010 | Environment · Resonance Academies · Cultural Significance |
| Vael | 6020 | Environment · The Hierarchy Academies · Vael's Moons |
| Keth'Sa | 6030 | Environment · Post-Incident Reconstruction · Data Union Infrastructure |
| Dur'ka | 6040 | Irradiated Surface · Clan Fortresses · Durakh Resilience |
| Nom'ara | 6050 | Lost Homeworld · Construct Occupation · Nomari Reclamation Movement |
| Core Systems | 6060 | Auris System (The Spire) · Compact Core Worlds · Trade Hub Worlds |
| Frontier and Colony Worlds | 6070 | The Reach · New Providence · Contested Border Colonies |
| The Veil Expanse | 6080 | Navigation Hazards · Hollow Sighting Zones · Exploration Expeditions |
| The Null Rift | 6090 | Anomalous Properties · Research Presence · Reclamation Interest |
| Ruins and Archaeological Sites | 6095 | Great Silence Impact Sites · Major Architect Ruins · Active Excavations |

**Target: ~50–65 entries**

---

## Category 7: History  (ID: 7)

| Primary Entry | Primary ID | Secondaries |
|--------------|------------|-------------|
| The Architect Age | 7000 | Architect Civilisation · The Void Gate Construction Era · What We Know |
| The Great Silence | 7010 | The Extinction Event · Archaeological Evidence · Leading Theories |
| The Awakening | 7020 | Rediscovery of the Gates · Early Interstellar Contact · First Conflicts |
| The Founding Wars | 7030 | Causes and Factions · The Syrathi Mediation · The Compact Accords |
| The Swarm Incursion | 7040 | The Swarm · The Incursion Campaign · Aftermath and Lessons |
| The Durakh Campaigns | 7050 | Origins of Conflict · The Campaigns · Deployment of the Wasting Curse · Long-Term Consequences |
| The Sundering | 7060 | First Contact · Escalation · The Sundering Treaty · Vaekar Veterans' Perspective |
| Recent History | 7070 | Post-Sundering Compact · The Keth'Sa Gate Incident · Political Realignments |
| The Breach | 7080 | The Survey Vessel Report · Seven Years of Suppression · The Leak · Current Political Crisis |
| Historical Figures | 7090 | Founding-era leaders · Swarm War heroes · Durakh Campaign architects · Breach whistleblower |

**Target: ~50–60 entries**

---

## Category 8: Culture  (ID: 8)

| Primary Entry | Primary ID | Secondaries |
|--------------|------------|-------------|
| Philosophy and Religion | 8000 | Syrathi Elder Philosophy · Chora Architect Theology · Vaekar Honour Code · Solari Secular Traditions |
| Arts and Entertainment | 8010 | Visual Arts · Music Across Species · Holodramas · Literature |
| Customs and Traditions | 8020 | Syrathi Life Rites · Vaekar Markings · Durakh Blooding · Nomari Suit Customs |
| Language | 8030 | Major Compact Languages · Translation Technology · Language and Politics |
| Sport and Recreation | 8040 | Resonance Competitions · Solari Sport Exports · Cross-Species Athletics |
| Food and Cuisine | 8050 | Dextro-Levo Divide · Notable Cuisines · The Spire Food Culture |
| Crime and Justice | 8060 | Accord Legal System · Notable Criminal Organisations · Punishment Traditions |

**Target: ~35–45 entries**

---

## Category 9: Economy  (ID: 9)

| Primary Entry | Primary ID | Secondaries |
|--------------|------------|-------------|
| The Accord Credit | 9000 | Currency History · Oshari Banking Dominance · Inflation and Crises |
| Major Corporations | 9010 | Null-Matter Extraction Corps · Defence Contractors · Medical Giants |
| Aethite Trade | 9020 | Mining Operations · Strategic Reserves · Smuggling and the Null Cartel |
| Trade Routes | 9030 | Gate-Adjacent Commerce · Major Routes · Chokepoints and Piracy |
| Black Market | 9040 | The Foundry Economy · Raeth Trafficking · Illegal Tech Trade |

**Target: ~25–35 entries**

---

## Category 10: The Hollow  (ID: 10)

| Primary Entry | Primary ID | Secondaries |
|--------------|------------|-------------|
| The Hollow | 10000 | What Is Known · First Confirmed Sighting · Scale and Capabilities |
| Hollow Vessel Classifications | 10010 | Observed Types · Estimated Scale · Weapon Signatures |
| The Great Silence Connection | 10020 | Archaeological Evidence · The Accord's Official Position · Dissenting Theories |
| Communication Attempts | 10030 | Methods Attempted · Pattern Analysis · The Keth Linguistic Team |
| Veil Expanse Incidents | 10040 | Post-Breach Sightings · The Suppressed Report · Current Monitoring |
| Counter-Hollow Research | 10050 | Accord Research Directorate · Reclamation Independent Research · Architect Tech as Defence |

**Target: ~25–35 entries**

---

## entry_type Values
```
0 = Primary    (parent_entry_id = NULL)
1 = Secondary  (parent_entry_id = primary entry id)
```

## requirement_id Conventions
```
NULL = always unlocked
1    = Complete the first Hollow encounter
2    = Complete Act 1
3    = Complete Act 2
4    = Join the Reclamation
```

## Resource ID Convention
```
Image IDs: [category_id * 10000 + entry_id_last_4_digits]  (e.g. entry 1000 image = resource 11000)
Audio IDs: image_id + 50000
All paths:  codex/[category_slug]/[entry_slug].[ext]
```
