# Advanced-3D-Assignment-2

## Game Title: Potion Ghost

### Student Name: Anthony O'Keeffe
### Student ID: 20093999

## Github Repository: https://github.com/GuyGoose/Advanced-3D-Assignment-2

## Features Implemented:
 
### Marking for the Quest System:
##### 0%-10%: The XML file for the Quest has been created.
- Completed
##### 11%-20%: the XML file includes all necessary information for each level (e.g., name, description, and objectives).
- Completed
##### 21%-30%: The XML file for the quest can be read and the content displayed in the console window.
- Completed
##### 31%-50%: Information about each level is displayed to the player when this level is starting (e.g., name, description, and objectives) and the player has the choice to hide/display this information throughout the level.
- Completed - The players current objective is displayed in the bottom left corner of the screen.
##### 51%-70%: In each level, the player’s actions are checked against the objectives described in the XML file and XPs are attributed when the player has achieved some of these.
- Completed - The player gains XP upon gaining an item needed for the quest.
##### 71%-100%: When all the objectives for a level (i.e., the objectives described in the XML file) are achieved by the player, the player is redirected to the next level.
- Completed - A portal will spawn when the player has collected all the items needed for the quest. This portal will take the player to the next level.


### Marking for the Inventory System:
##### 0%-10%: A class that can be used for each item has been created based on the description above.
- Completed
##### 11%-20%: An inventory has been created for the player and items can be added/updated/removed to/from it.
- Completed
##### 21%-40%: The player can collect items and add them to the inventory.
- Completed
##### 41%-60%: The inventory can be displayed on screen with an image and the corresponding amount.
- Completed - When Tab is pressed the inventory will be displayed in the right-hand side of the screen.
##### 61%-70%: Food items can be used to increase the player’s health (initially 50%).
- Completed - There is a potion item in the second level that will increase the players health by by 1 and increase the players speed by 2.
##### 71%-80%: The player cannot carry/collect more than the maximum amount allowed for each item.
- Completed - The player cannot collect more items than what is needed for the quest.
##### 81%-100%: items can be bought in an in-game shop based on the players’ gold coins, and the cost of each item available in the shop.
- Not Implemented


### Marking for the Dialogue System:
##### 0%-10%: An XML file for the dialogues has been created and includes several dialogues, each with 3 possible answers.
- Completed - Not every Dialogue has 3 possible answers.
##### 11%-30%: The game can read all possible dialogues for a specific NPC and display same (for example, in the console window).
- Completed - The dialogues are displayed in the console window on start and when the player interacts with an NPC.
##### 31%-50%: Upon colliding with (or entering a trigger zone around) an NPC, a dialogue window should open and display dialogues based on the NPC.
- Completed - The player can press E to interact with an NPC and the dialogue will be displayed.
##### 51-70%: The player can choose between the 3 possible answers using the keys A, B or C.
- Completed - The player can press buttons displayed on the screen to choose an answer.
##### 71-90%: The player can talk to an NPC several times, and the dialogue window will disappear at the end of each conversation.
- Completed - The player can interact with an NPC multiple times and the dialogue will be displayed each time.
##### 91%-100%: Each NPC includes different animations depending on whether they are idle, talking to the player or listening to the player.
- Semi Implemented - The NPC plays a constant animation.


### Marking for the Binary Tree Algorithm:
##### 40%-50%: The level was created procedurally, and all rooms can be accessed.
- Completed - The level is created procedurally and all rooms can be accessed.
##### 51%-70%: The player can navigate through the level and perform the actions set for that level in the Quest System XML file.
- Completed - The player can navigate through the level and complete the quest.
##### 71%-80%: The player can progress to the next level after completing all the objectives set for that level.
- Completed - The player can progress to the next level after completing the quest.
##### 81%-100%: The SideWinder algorithm was used. Please see this site for more information on that algorithm.
- Not Implemented - The backtracking omni-directional binary tree algorithm was used.


### Marking for the Dynamic Gameplay:
##### 40%-50%: Changes for the player’s XP are monitored frequently and the difficulty level is adjusted.
- Completed - The player gains XP upon gaining an item needed for the quest.
##### 51%-70%: The NPCs’ speed and detection capability are dynamically adjusted based on the user’s progress.
- Completed - Ghosts in the second level will increase in speed and detection range as the player gains XP.
##### 71%-80%: The NPCs’ spawning frequency is dynamically adjusted based on the user’s progress.
- Completed - The ghosts in the second level will spawn more frequently as the player gains XP.
##### 81%-90%: The health packs spawning frequency is dynamically adjusted based on the user’s progress (i.e., XPs).
- Not Implemented - There is only one potion available in the second level.
##### 91%-100%: The size or position of some items in the level are dynamically adjusted based on the user’s progress (e.g., bridges’ width).
- Completed - The width of the bridges in the second level will decrease as the player gains XP.

## Bugs and Issues Encountered:
- The player can go faster when strafing and moving forward at the same time.
- Items can moved by the player off the map.
- The player can move while in the dialogue window.
- Quests do not load correctly in the web build.