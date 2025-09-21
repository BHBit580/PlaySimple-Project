**Word Boggle Project - Technical Implementation Explanation**

**Event System Architecture**
In this project, I used **ScriptableObject events** as the core communication system. I really like this concept because it allows classes to communicate without direct references to each other. A dedicated ScriptableObject handles the events, and classes can simply subscribe to it and do their job.

For example, with a "game over" event, many different conditions can trigger it, but ideally you shouldn't worry about where it triggers. You just take a reference to that event and handle it accordingly. This **maintains independence** between systems and promotes loose coupling.

**Core Architecture**
My basic unit is the **LetterTile**, and I have a **GridManager** that handles the spawning and destroying of tiles.

**Endless Mode Flow**
Let's start with endless mode:
1. **BoggleGame Class**: There's a dedicated class called BoggleGame that creates a 2D char matrix using a word placement algorithm.
2. **EndlessLevelController**: This gets the matrix from BoggleGame, then initializes the GridManager, and grid spawning starts.
3. **Event-Driven Updates**: When new words are formed, an event is raised. The EndlessLevelController listens to this event, gets new matrix data from the BoggleGame about which positions need new letters, then tells the grid to update accordingly. The GridManager handles the tile replacement animations.

**Scoring System Clarification**
To be honest, I didn't fully understand the scoring system requirements - like whether different characters should have different point values. There was some ambiguity in the assignment, so I made each character worth the same value (1 point).

**Example**:
* APPLE = 5 points
* PIG = 3 points

This keeps it simple and functional.

**Level Mode Flow**
Now for level mode:
1. **DataLoader**: I have level data that gets parsed by the DataLoader class.
2. **LevelController**: This fetches the parsed data (around 10 levels' worth) and randomly selects one level to play.
3. **GridManager Integration**: The LevelController tells the GridManager to spawn the specific grid based on the selected level data.
4. **Tile Properties**: When the GridManager spawns tiles, it also initializes their properties (Normal, Rock, Bug) based on the level data.
5. **TileAbilityManager**: There's a dedicated class managing tile abilities. When new words are formed, it determines what happens to special tiles (like how to destroy rocks or collect bugs).

**What I Would Do Differently With More Time**

**1. Improved Ability System**
My **TileAbilityManager** needs improvement. Currently, for this simple project, there are only 2 abilities, but if there were 10+ abilities, there should ideally be a **dedicated ability type management system** with:
* Individual ability classes
* An ability factory pattern
* A more scalable ability registration system

**2. Enhanced Challenge Management System**
The current challenge management system is simple and works fine, but I think it could be **more scalable and better structured** with:
* A strategy pattern for different challenge types
* More flexible objective combinations
* Better progression tracking
* Configurable challenge parameters

**3. Advanced Features**
* More sophisticated word generation algorithms
* A save/load system for progression

Due to the limited time constraint, I prioritized:
* **Functional architecture** 
* **Working features**
* **Clean separation of concerns** 
* **Event-driven design**
