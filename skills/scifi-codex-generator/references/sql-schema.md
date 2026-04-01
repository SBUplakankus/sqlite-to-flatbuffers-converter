# SQL Schema — Veil Compact Codex Database

## Complete DDL

```sql
-- ============================================================
-- Veil Compact Codex Database
-- Designer-facing SQLite schema
-- ============================================================

PRAGMA foreign_keys = ON;

CREATE TABLE "categories" (
    "id"         INTEGER PRIMARY KEY,
    "name"       TEXT NOT NULL,
    "sort_order" INTEGER DEFAULT 0
);

CREATE TABLE "requirements" (
    "id"          INTEGER PRIMARY KEY,
    "description" TEXT NOT NULL
);

CREATE TABLE "resources" (
    "id"            INTEGER PRIMARY KEY,
    "file_path"     TEXT NOT NULL,
    "resource_type" INTEGER,    -- 0 = none, 1 = audio, 2 = image
    "url"           TEXT,
    "tts_metadata"  TEXT
);

CREATE TABLE "codex_entries" (
    "id"              INTEGER PRIMARY KEY,
    "category_id"     INTEGER NOT NULL,
    "parent_entry_id" INTEGER,           -- NULL = Primary entry; set = Secondary entry
    "requirement_id"  INTEGER,           -- NULL = always unlocked; game handles evaluation
    "entry_type"      INTEGER NOT NULL,  -- 0 = Primary, 1 = Secondary
    "title"           TEXT NOT NULL,
    "content"         TEXT NOT NULL,
    "audio_id"        INTEGER,
    "image_id"        INTEGER,
    "sort_order"      INTEGER DEFAULT 0,
    FOREIGN KEY("category_id")     REFERENCES "categories"("id"),
    FOREIGN KEY("parent_entry_id") REFERENCES "codex_entries"("id"),
    FOREIGN KEY("requirement_id")  REFERENCES "requirements"("id"),
    FOREIGN KEY("audio_id")        REFERENCES "resources"("id"),
    FOREIGN KEY("image_id")        REFERENCES "resources"("id")
);

CREATE INDEX IF NOT EXISTS idx_entries_category ON codex_entries(category_id);
CREATE INDEX IF NOT EXISTS idx_entries_parent   ON codex_entries(parent_entry_id);
CREATE INDEX IF NOT EXISTS idx_entries_type     ON codex_entries(entry_type);
```

---

## Enum Reference

### resources.resource_type
```
0 = none
1 = audio
2 = image
```

### codex_entries.entry_type
```
0 = Primary    (top-level entry within a category; has content; may have Secondary children)
1 = Secondary  (child of a Primary entry; leaf node)
```

### parent_entry_id logic
```
NULL        → Primary entry
[entry id]  → Secondary entry; value is the parent Primary entry's id
```

---

## Standard Requirements Seed

```sql
INSERT INTO requirements (id, description) VALUES
(1, 'Complete the first Hollow encounter'),
(2, 'Complete Act 1'),
(3, 'Complete Act 2'),
(4, 'Join the Reclamation');
```

---

## INSERT Block Pattern

Output one block per Primary entry + its Secondaries. Always insert Primary first.

```sql
-- ============================================================
-- Category: Aliens  |  Primary: Syrathi
-- ============================================================

-- Resources
INSERT INTO resources (id, file_path, resource_type, url, tts_metadata) VALUES
(1, 'codex/aliens/syrathi.png', 2, NULL, NULL),
(2, 'codex/aliens/syrathi.ogg', 1, NULL, '{"voice":"en_compact_f01","speed":1.0}');

-- Primary entry
INSERT INTO codex_entries
    (id, category_id, parent_entry_id, requirement_id, entry_type, title, content, audio_id, image_id, sort_order)
VALUES
(1001, 1, NULL, NULL, 0, 'Syrathi',
'[200–400 word primary overview prose]',
2, 1, 1);

-- Secondary entries
INSERT INTO codex_entries
    (id, category_id, parent_entry_id, requirement_id, entry_type, title, content, audio_id, image_id, sort_order)
VALUES
(1002, 1, 1001, NULL, 1, 'Syrathi: Biology and Physiology',
'[150–300 word prose]',
NULL, NULL, 1),

(1003, 1, 1001, NULL, 1, 'Syrathi: Society and the Matriarchy',
'[150–300 word prose]',
NULL, NULL, 2);
```

---

## Useful Queries

```sql
-- All Primary entries for a category
SELECT id, title, sort_order FROM codex_entries
WHERE category_id = 1 AND entry_type = 0
ORDER BY sort_order;

-- All Secondary entries under a Primary
SELECT id, title, sort_order FROM codex_entries
WHERE parent_entry_id = 1001
ORDER BY sort_order;

-- Full tree
SELECT c.name, p.title AS primary, s.title AS secondary, s.sort_order
FROM categories c
JOIN codex_entries p ON p.category_id = c.id AND p.entry_type = 0
LEFT JOIN codex_entries s ON s.parent_entry_id = p.id
ORDER BY c.sort_order, p.sort_order, s.sort_order;
```
