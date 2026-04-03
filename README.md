# SQLite to FlatBuffers Conversion Tool

Tool in progress.

## Overview

Developing this tool so I can have the flexibility of managing large amounts of data in a SQLite database that can be modified in DB Browser, but then have the performance and runtime benefits of converting this database to binary using flatbuffers.

Designed to work with any language, but I will be developing my project in Unity with C#. The tool itself will also be in C# and a console app.

### Useful Links

[Flatbuffers Repo](https://github.com/google/flatbuffers)

[Flatbuffers Documentation](https://flatbuffers.dev/)

[DB Browser for SQLite](https://sqlitebrowser.org/)

## SQLite Layer

### Goal

The Goal of implementing SQLite into the project is so that the data can be created and modified externally  
in a structured and clean format such as from DB Browser for SQLite.

The idea behind the conversion then is for the performance at game runtime. Querying the SQLite database directly  
has some heavy drawbacks but flatbuffers is designed with high performance in mind. It is also superior to other  
common data formats such as JSON or CSV files.

The main test case I will be using is an in-game wiki based on the Mass Effect codex, more specifically the  
Mass Effect 2 codex. This features large amounts of data, clearly structured categories and sub categories.  
Primary entries with an audio narration and Secondary entries that are text only. All unlockable as you  
progress through the game and traversable through the in-game UI.

## Flatbuffers Layer

### Goal

The goal of the flatbuffers layer is to convert the data from the SQLite database into a binary format that can be easily read and accessed at runtime in the game.  
This will allow for faster loading times and better performance when accessing the data.

### Schema

The schema for the flatbuffers will be designed to mirror the structure of the SQLite database with one exception.  
Flatbuffers aims for Direct Nesting of data, so instead of having a table that references another table with an ID, the data will be directly nested within the parent table.  
This will allow for faster access to the data at runtime without the need for additional lookups.

## Conversion Layer

### Goal

The goal of the conversion layer is to convert the SQLite database into the flatbuffers schema.  
This will then be built into the .bin file that we will access at runtime in our game.