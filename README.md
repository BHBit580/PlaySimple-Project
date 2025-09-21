# Word Boggle Game

A Unity-based implementation of the classic word-finding puzzle game with both endless and level-based gameplay modes.

## Overview

This project implements a Word Boggle game in Unity 2022.3.32f1+ featuring drag-to-select word formation, special tile mechanics, and multiple challenge types. The focus is on clean architecture and modular design rather than visual polish.

## Features

### Game Modes
- **Endless Mode**: 4x4 grid with dynamic letter replacement
- **Level Mode**: Static grids with objectives and special tiles

### Core Mechanics
- Drag-to-select word formation with visual connection lines
- Adjacent tile selection (8-directional)
- Dictionary-based word validation
- Real-time scoring system

### Special Tiles (Level Mode)
- **Bug Tiles**: Collectible bonus rewards
- **Rock Tiles**: Blocked tiles that can be cleared by adjacent words
- **Normal Tiles**: Standard letter tiles

### Challenge Types
1. Make X words
2. Reach X score in Y time
3. Make X words in Y time

## Installation & Setup

### Requirements
- Unity 2022.3.32f1 or newer
- DOTween (for animations)
- TextMeshPro (for UI text)

### Setup Steps
1. Clone/download the project
2. Open in Unity 2022.3.32f1+
3. Ensure DOTween is imported via Package Manager
4. Assign word dictionary text file to DataLoader
5. Configure level data JSON file (optional)
6. Build and run

## How to Play

### Endless Mode
1. Drag across adjacent letters to form words
2. Words must be at least 3 letters long
3. Used letters disappear and new ones drop from top
4. Score points based on word length

### Level Mode
1. Complete specific objectives within time/move limits
2. Collect bugs by including them in words
3. Break rocks by forming words adjacent to them
4. Meet challenge requirements to win

## Technical Architecture

### Core Systems
- **TileSelectionController**: Handles user input and word formation
- **GridManager**: Manages tile grid creation and animations
- **DataLoader**: Loads dictionary and level data
- **ScoreManager**: Tracks scoring and statistics
- **ChallengeHandler**: Manages level objectives

### Design Patterns
- **Singleton**: For manager classes
- **Observer**: ScriptableObject events for loose coupling
- **Strategy**: Different challenge types
- **Component**: Modular tile abilities

### Event System
```csharp
DataEventChannelSO newWordFormedEvent
├── ScoreManager (scoring)
├── TileAbilityManager (special tiles)
├── EndlessLevelController (tile replacement)
└── ChallengeHandler (objective tracking)
```

## Project Structure

```
Scripts/
├── Core/
│   ├── TileSelectionController.cs
│   ├── GridManager.cs
│   └── LetterTile.cs
├── GameModes/
│   ├── EndlessLevelController.cs
│   └── LevelController.cs
├── Data/
│   ├── DataLoader.cs
│   ├── LevelData.cs
│   └── BoggleGame.cs
├── Systems/
│   ├── ScoreManager.cs
│   ├── ChallengeHandler.cs
│   └── TileAbilityManager.cs
└── UI/
    ├── MainMenuUI.cs
    ├── LevelWon.cs
    └── LevelFailed.cs
```

## Configuration

### Word Dictionary
- Assign a .txt file with one word per line to DataLoader
- Words are automatically converted to lowercase
- Used for word validation

### Level Data
- JSON format with grid size, tile types, and objectives
- Optional - system generates levels if not provided
- See LevelData.cs for structure

## Known Limitations

- Basic word generation algorithm in endless mode
- No object pooling (performance impact)
- Simple scoring system (1 point per letter)
- No save/persistence system
- Limited error handling

## Future Improvements

- Object pooling for tile management
- Advanced word generation algorithms
- More sophisticated scoring system
- Save/load game state
- Additional special tile types
- Sound effects and better animations

## Development Notes

**Time Constraint**: 6-8 hours
**Focus**: Architecture and functionality over visual polish
**Key Decisions**:
- Event-driven architecture for modularity
- ScriptableObject events for loose coupling
- Modular challenge system for extensibility
- Simple but effective tile selection mechanics

## Technical Requirements Met

- ✅ Two game modes (Endless/Levels)
- ✅ 4x4 grid with drag selection
- ✅ Scoring system with UI display
- ✅ Dynamic letter replacement (Endless)
- ✅ Special tiles (bugs/rocks) in Level mode
- ✅ Three challenge types
- ✅ JSON level data support
- ✅ Clean architecture focus

## Build Information

- Target Platform: Android APK + PC Standalone
- Unity Version: 2022.3.32f1
- Dependencies: DOTween, TextMeshPro

---

*This project prioritizes clean code architecture and game functionality over visual aesthetics, as specified in the assignment requirements.*
