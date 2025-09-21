# Word Boggle Project - Technical Implementation Explanation

## Event System Architecture
In this project, I implemented ScriptableObject events as the core communication system. This approach is particularly effective because it allows classes to communicate without requiring direct references to each other. A dedicated ScriptableObject handles the events, and classes can simply subscribe to it and execute their respective functions.

## Core Architecture
The basic unit of the system is the LetterTile, and I have a GridManager that handles the spawning and destruction of tiles.

## Endless Mode Flow
The endless mode follows this sequence:

1. **BoggleGame Class**: A dedicated class called BoggleGame creates a 2D character matrix using a word placement algorithm.

2. **EndlessLevelController**: This component retrieves the matrix from BoggleGame, initializes the GridManager, and begins grid spawning.

3. **Event-Driven Updates**: When new words are formed, an event is triggered. The EndlessLevelController listens to this event, retrieves new matrix data from BoggleGame indicating which positions need new letters, then instructs the grid to update accordingly. The GridManager handles the tile replacement animations.

## Scoring Value Clarification from the assignment
The Assignment requirements contained some ambiguity regarding whether different characters should have varying point values. To maintain simplicity and functionality, I implemented a uniform scoring system where each character is worth the same value (1 point).

Issue was which character to give what points , i mean that was confusing so i made all character points same

**Examples:**
- **APPLE = 5 points**
- **PIG = 3 points**


## Level Mode Flow
The level mode operates as follows:

1. **DataLoader**: Level data is parsed by the DataLoader class.

2. **LevelController**: This component retrieves the parsed data (approximately 10 levels) and randomly selects one level for gameplay.

3. **GridManager Integration**: The LevelController instructs the GridManager to spawn the specific grid based on the selected level data.

4. **Tile Properties**: When the GridManager spawns tiles, it also initializes their properties (Normal, Rock, Bug) based on the level data.

5. **TileAbilityManager**: A dedicated class manages tile abilities. When new words are formed, it determines the behavior of special tiles (such as how to destroy rocks or collect bugs).

## What I Would Do Differently With More Time

### 1. Improved Word Generation Algorithms – IMPORTANT
Currently, the word generation in infinite mode for Word Boggle is very simple. I could make it much better and implement the bonus tasks that were given in the assignment. This is the highest priority improvement needed for the system.

### 2. Improved Ability System
The current TileAbilityManager requires enhancement. While it works adequately for this simple project with only 2 abilities, a system with 10+ abilities would benefit from a dedicated ability type management system featuring:
- **Individual ability classes**
- **An ability factory pattern**
- **A more scalable ability registration system**

### 3. Enhanced Challenge Management System
The current challenge management system is functional but could be more scalable and better structured with:
- **A strategy pattern for different challenge types**
- **More flexible objective combinations**
- **Better progression tracking**

## Conclusion
Due to time constraints, I prioritized:
- **Functional architecture**
- **Working features**
- **Clean separation of concerns**

The current implementation provides a solid foundation that can be expanded and refined in future iterations.
