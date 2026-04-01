---
name: scifi-codex-generator
description: Generate a complete, Mass Effect-inspired original sci-fi universe codex — the Veil Compact — with hundreds of detailed lore entries across all universe topics. Use this skill whenever the user wants to generate sci-fi universe lore, game codex entries, worldbuilding database content, codex seed data for a game, SQLite + FlatBuffers pipeline for a codex system, or bulk worldbuilding content for testing database systems. Trigger for any mention of codex generation, sci-fi lore, universe worldbuilding at scale, Veil Compact entries, or game database seed content — even if the user just says "generate some more entries" or "give me the Species section."
---

# Sci-Fi Codex Generator — The Veil Compact

Generates an original sci-fi universe called **The Veil Compact** — a Mass Effect-inspired setting that is a 1-to-1 thematic parallel but entirely original in name, species, factions, history, and lore. Outputs structured content as SQLite seed SQL and a FlatBuffers schema for a Unity C# runtime pipeline.

---

## How to Use This Skill

### Before generating anything
Read these reference files in order:
1. `references/universe-bible.md` — Universe name, concept, all ME parallels, species, factions, history, technology, locations. **Read this first every time to maintain lore consistency.**
2. `references/categories.md` — The full two-tier codex category hierarchy with SQL IDs and target entry counts per sub-category.
3. `references/sql-schema.md` — Complete SQLite DDL and INSERT patterns.
4. `references/flatbuffers-schema.md` — The .fbs schema and C# generation notes.

### Generation workflow

**For a full generation run (all entries):**
1. Read universe-bible.md
2. Read categories.md — identify all sub-categories and their target entry counts
3. Generate entries category by category (one primary category at a time)
4. For each entry: write a full, rich prose body (150–400 words, encyclopaedic tone, present-tense as if written in-universe)
5. Output as SQL INSERT blocks (see sql-schema.md for format)
6. After all entries: output the .fbs schema and note the C# codegen command

**For partial generation (one category or section):**
1. Read universe-bible.md (always)
2. Read categories.md (find the relevant section)
3. Generate only the requested category

**For adding new entries to an existing run:**
1. Read universe-bible.md
2. Ask the user what category and what `id` offset to start from (to avoid PK collisions)
3. Generate and output SQL INSERTs with correct IDs

---

## Entry Writing Style

Entries must feel like they belong in a real game codex. Follow these rules:

- **Tone**: In-universe encyclopaedia. Authoritative, slightly academic, occasionally dry. Written as if by a Veil Compact Accord Intelligence Directorate analyst.
- **Length**: 150–400 words per entry body. Sub-entries tend to be shorter (150–250 words); primary category overview entries can be longer (300–400 words).
- **No meta-references**: Never reference Mass Effect, Earth pop culture, or the real world.
- **Cross-referencing**: Naturally reference other in-universe entities (species names, locations, factions, technologies) to build a cohesive feel. Do not use hyperlinks — write them as plain text references, e.g. "See also: Void Gates."
- **Avoid allegory**: Don't make it obvious that Syrathi = Asari or Durakh = Krogan. The parallels are structural, not textual.
- **Specificity**: Include specific dates, figures, statistics, proper nouns. Invented detail makes lore feel real.
- **Vocabulary**: Use in-universe terminology consistently (Null-Matter, Resonance, Resonants, Void Gates, Arbiters, the Spire, the Accord, the Hollow, the Architects).

---

## Output Format

### SQL output
- One block per Primary entry (plus its Secondaries)
- Block header comment: `-- Category: [Name]  |  Primary: [Title]`
- Insert order within each block: resources → primary entry → secondary entries
- See `references/sql-schema.md` for full DDL and INSERT templates

### FlatBuffers output
- Output the .fbs schema once, at the end of a full generation run or on explicit request
- See `references/flatbuffers-schema.md` for the full .fbs file, flatc command, and Unity C# usage

---

## Target Entry Count

Full codex target: **350–500 entries** across all categories.
See `references/categories.md` for per-sub-category targets.

---

## Reference Files Index

| File | Purpose | When to read |
|------|---------|--------------|
| `references/universe-bible.md` | Universe lore, all species, factions, history, tech, locations | Always — read first |
| `references/categories.md` | Full two-tier category hierarchy with IDs and entry counts | When generating or navigating categories |
| `references/sql-schema.md` | SQLite DDL + INSERT patterns | When outputting SQL |
| `references/flatbuffers-schema.md` | .fbs schema + C# Unity notes | When outputting FlatBuffers schema |
