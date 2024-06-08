# Sprint 4 Report (1/8/24 - 2/2/24)

## Sprint 4 Demo Video
See the following link to view our sprint demo video: https://www.youtube.com/watch?v=h1rgtRof9I8

## What's New (User Facing)
 * All doors animate when the player approaches them
 * Game environment is formatted to match the game map
 * 3D models for five new houses
 * 3D model for blacksmith's shop
 * Foilage including trees, shrubs, and grass are now included in the game
 * Players are now able to sprint
 * All 8 NPCs exist in-game
 * All NPCs have dynamic text communication (open text input for dialogue)
 * Day and Night cycles exist in-game
 * Saved game files can be cleared via New Game feature

## Work Summary (Developer Facing)
Throughout this sprint, our team put out the most forward facing work we have in any single sprint so far during this project. Many of the different pieces that have been in development for a long time were finally completed and able to be put into a user facing state, allowing us to see the results of our work. Every team member overcame daunting tasks that they had been set on including environment design, model creation, AI conversation linking, and backend saving systems. Some of the largest barriers we overcame this sprint were in regards to the intermixing of systems that were previously only designed by one person. At this point in the project, many of the previously standalone components are now being integrated with other old components or directly involved in new ones that are being created by someone different than the programmer for the original programmer. This has resulted in some various changes to systems to facilitiate these new pieces. These barriers were able to be overcome with clear communication and some troubleshooting between multiple team members. This has led to the learning of further planning designs for integration and being prepared to attach new components to exisitng designs.

## Unfinished Work
For the most part, we completed all of what we wanted to accomplish during this sprint with the exception of the transition between the mine interior scene and the main scene. This will be implemented first thing during the next sprint. 

## Completed Issues/User Stories
Here are links to the issues that we completed in this sprint:

 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/132 Fix all doors
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/188 NPC Setup
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/192 Ground
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/193 House 2 model
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/195 House 3 model
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/196 Put all models in their map positions
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/200 Tree, shrub, and grass 3D objects
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/203 Arrange trees, shrubs, and grass in main scene
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/209 Add all static NPCs
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/210 Make NPC communication dynamic
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/213 Menus not opening correctly
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/232 House 4 model
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/254 make rest of NPC dialogue dynamic
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/263 House 5 model
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/264 House 6 model
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/286 Blacksmith shop 3D model
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/221 Added sprint function to player
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/271 Implement health system
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/279 Menu System Overhaul
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/270 Create Monster Animations
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/267 Model and Pose Character Models
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/231 Finish Monster AI (attack, rotate towards player)
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/247 Fix In-game Fader Render Issues
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/227 Throughly Test New Game and Clearing, then Saving again
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/224 Create nighttime system in-game
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/219 Hide NPCs Scripts
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/222 Find AudioListener on the Player Cam
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/225 Create time-shifting fades in Scene_With_Models
 
 ## Incomplete Issues/User Stories
 Here are links to issues we worked on but did not complete in this sprint:
 
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/194 We ran out of time to complete this issue. It will be worked on first thing next sprint.

## Code Files for Review
Please review the following code files, which were actively developed during this sprint, for quality:
 * [GameManager.cs](https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/blob/main/Wicked%20West/Assets/GameManager.cs)
 * [PlayerOpenMenu.cs](https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/blob/main/Wicked%20West/Assets/Scripts/Player/PlayerOpenMenu.cs)
 * [EnemyAI.cs](https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/blob/main/Wicked%20West/Assets/EnemyAI.cs)

 
## Retrospective Summary
Here's what went well:
  * Major Progress on NPCs which is the main unique feature of the game
  * Communication in merging to main
  * Timely completion of tasks
 
Here's what we'd like to improve:
   * Better linking of tasks between team members
   * Continuous integration in testing added features prior to merging to main
   * Giving our game to testers to detect flaws
  
Here are changes we plan to implement in the next sprint:
   * Creating Hiding Mechanic
   * Futher work on daytime story and linking NPCs and items
   * Game Balence alterations to movement of player/monster
