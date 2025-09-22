# Word Boggle Unity Game

A Unity-based Word Boggle game implementation featuring both Endless and Level-based gameplay modes with special tile mechanics and challenge systems.

## Technical Architecture

### Event System Architecture

This project implements an event-driven architecture using ScriptableObjects to facilitate decoupled communication between game systems. This approach allows classes to communicate without requiring direct references to each other, improving maintainability and scalability.

**Event Types:**
- **VoidEventChannelSO** - Simple notifications (e.g., level completion)
- **DataEventChannelSO** - Data transfer between systems (e.g., word formation events with tile and word data)

### Core Architecture

The system is built around the **LetterTile** as the fundamental interactive unit, managed by the **GridManager** which handles:
- Tile spawning and destruction
- Grid layout and positioning  
- Animation coordination

The **TileSelectionController** manages:
- Connection formation between selected tiles
- Visual connection lines using dynamically created UI elements
- Word formation validation

---

## Game Modes

### Endless Mode Flow

1. **BoggleGame Class** - Creates a 2D character matrix using word placement algorithms
   - **Data Structure**: Uses 2D Character Arrays (`char[,]`) for efficient grid representation
   - The BoggleGame class acts as the mastermind behind grid generation
   
2. **EndlessLevelController** - Retrieves the matrix from BoggleGame and initializes the GridManager for grid spawning

### Level Mode Flow

1. **DataLoader** - Parses level data from JSON files
2. **LevelController** - Retrieves parsed data (~10 levels) and randomly selects gameplay level
3. **GridManager Integration** - Spawns specific grid layout based on selected level data
4. **Tile Properties** - Initializes tile properties (Normal, Rock, Bug) from level data
5. **TileAbilityManager** - Manages special tile behaviors and interactions

---

## Scoring System

Simple point-based scoring system where each letter contributes **1 point** to the total word score.

**Examples:**
- `APPLE` = 5 points
- `PIG` = 3 points

---

## Challenge Management System

Flexible challenge system supporting three challenge types:

- **Make X words**
- **Reach X score in Y time** 
- **Make X words in Y time**

The **ChallengeHandler** monitors game state and provides real-time progress updates with immediate feedback on objectives and remaining time.

---

## Future Improvements

### 1. Enhanced Boggle Generation Algorithm
- Current 2D character array generation is basic
- Would implement advanced word placement algorithms
- Add bonus features for ensuring valid word possibilities

### 2. Modular Tile Property System
- Current **TileAbilityManager** works for 2 tile properties (rock, bug)
- Needs scalable architecture for 10+ property types
- Implement component-based ability system

### 3. Scalable Challenge Management
- Current system functional but limited
- Would implement database-driven challenge system
- Better management for challenge types and variations

---

## 🎯 Development Priorities

Due to time constraints, development focused on:

✅ **Functional architecture**  
✅ **Working core features**  
✅ **Clean separation of concerns**
