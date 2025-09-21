**Word Boggle Project - Technical Implementation Explanation**

**Event System Architecture**
In this project, I implemented an event system using ScriptableObjects. This approach is particularly effective because it allows classes to communicate without requiring direct references to each other. A dedicated ScriptableObject handles the events, and classes can simply subscribe to it and execute their respective functions.

**Core Architecture**
The basic unit of the system is the LetterTile, with a GridManager that handles the spawning and destruction of LetterTile instances.

**Endless Mode Flow**
The endless mode follows this sequence:

1. **BoggleGame Class**: A dedicated class called BoggleGame creates a 2D character matrix using a word placement algorithm.
2. **EndlessLevelController**: This component retrieves the matrix from BoggleGame, initializes the GridManager, and begins grid spawning.

**Scoring System Clarification**
I had some confusion regarding the scoring system from the assignment specifications, particularly which characters should receive what point values. To keep things simple, I assigned 1 point to each character.

**Examples:**

- **APPLE = 5 points**
- **PIG = 3 points**

**Level Mode Flow**
The level mode operates as follows:

1. **DataLoader**: Level data is parsed by the DataLoader class.
2. **LevelController**: This component retrieves the parsed data (approximately 10 levels) and randomly selects one level for gameplay.
3. **GridManager Integration**: The LevelController instructs the GridManager to spawn the specific grid based on the selected level data.
4. **Tile Properties**: When the GridManager spawns tiles, it also initializes their properties (Normal, Rock, Bug) based on the level data.
5. **TileAbilityManager**: A dedicated class manages tile abilities. When new words are formed, it determines the behavior of special tiles (such as how to destroy rocks or collect bugs).

**What I Would Do Differently With More Time**

**1. Improved Boggle Generation Algorithm – IMPORTANT**
Currently, the 2D character array generation in endless mode for Word Boggle is very basic. I could significantly improve this and implement the bonus tasks that were specified in the assignment.

**2. Enhanced Tile Property System**
The current TileAbilityManager requires enhancement. While it works for this simple project with only 2 tile properties (rock and bug), a system with 10+ properties would require a more robust ability/property type management system.

**3. Scalable Challenge Management System**
The current challenge management system is functional but could be more scalable. I would implement a challenge database system to better manage different challenge types and variations.

**Conclusion**
Due to time constraints, I prioritized:

- **Functional architecture**
- **Working core features**
- **Clean separation of concerns**
