# Sprint 5 Report (2/2/2024 - 3/2/2024)

## Sprint 5 Demo Video
See the following link to view our sprint demo video: https://youtu.be/VSCbj58wkTU

## What's New (User Facing)
 * Smooth transition between the mine scene and main scene
 * Game boundary keeps the player within the restricted boundary of the game
 * All buildings are now furnished
 * Fog in the atmosphere
 * Hiding Places now Exist Around the Map
 * NPC Dialogue has been Updated
 * Fixed Timers
 * Bed can be slept in
 * Naturally-occuring transitions from day->night and night->day

## Work Summary (Developer Facing)
Throughout this sprint our team worked twoards a working playable demo, which included fine tuning some issues to allow the system to run smoothly without developer supervision as well as filling in some missing features that had been put off but were needed for the game to run through completely. There was large amounts of work on the night and mine scenes done this sprint, which unearthed some issues with systems that were originally designed to work with a single scene. These systems were fixed or rewritten, and things were adjusted to be extensible for moving between scenes going forward.

## Unfinished Work
One item that was not finished was the integration with NPCs and items. This is something needed for the final build of the game, but the items themselves are not needed in our testing demo. Because of this it was decided the items were fine to sit on the ground for now and the placement and NPC integration would happen in the next sprint. Also, some cosmetic helper messages to display to the player were omitted and will be added later, since they did not effect functionality. Bug testing in the mines and in inter-scene changing must continue.

## Completed Issues/User Stories
Here are links to the issues that we completed in this sprint:

 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/163 Jump Reset Bug
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/226 Hiding Objects
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/291 House 7 model with interior
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/293 Fix rendering issues
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/311 Time not displayed in mine
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/312 Player turned wrong way when entering mine
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/318 Update Hiding Camera Shifting
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/319 Find Box Models
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/341 Add furniture to empty buildings
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/347 Game boundary
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/348 Add fog to main scene
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/356 Add alcohol bottles to saloon
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/366 Message displayed when player hits game boundary
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/367 Fix player position when player exits the mine
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/368 Fix mine model
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/380 Hiding Crash (looked into)
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/383 Updated Character Descriptions
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/384 Camera Manager
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/385 Inventory Refactoring
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/388 Add Additional Hiding Places
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/390 Tag objects as ground
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/268 Add Journal/Map Menu
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/269 Add Lose Screen
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/328 Add Effect Radius to Monster
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/329 Add Damage Effects to Monster
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/330 Create Monster Manager
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/331 Monster relocates on player hide 
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/333 Update Health variable to be a singleton
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/369 integrate monster and objective with nighttime scene
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/395 monster path towards player/objective
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/398 create objective model
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/399 adjust monster behavior
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/411 Fix skybox light (darker)
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/408 Helper dev commands Documentation
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/410 Back to Bed when Monster -1 Lives the Player
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/407 Sleep interaction as "Hiding Place"
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/405 Freeze all Restraints on Player during Sleep Teleportation
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/373 Add Custom API Key scene pathway from LoadGameState Scene
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/372 Make naturally-occuring transitions in case of Day/Night timers running out
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/350 Sleep feature
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/323 Update/Patch Fade Subtext Logic
 
 
 ## Incomplete Issues/User Stories
 Here are links to issues we worked on but did not complete in this sprint:
 
 * [https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/349] <<Other issues needed for the playable demo took priority, so we did not have time to finish this issue.>>
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/412 Going back to main menu we will have to either depopulate the GMI and all its accociated children, or manually add attribute resets for many instance classes we were not prepared to do. We'll bringg this up and plan out a solution at a future team meeting.
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/409 This was purely cosmetic, and fixing timer cycles/night scene/functionalities came first, since helper notation subtexts are provided in notes elsewhere.

## Code Files for Review
Please review the following code files, which were actively developed during this sprint, for quality:
 * [MineAndMainTransition.cs](https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/blob/main/Wicked%20West/Assets/Scripts/MineAndMainTransition.cs)
 * [MineTransition.cs](https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/blob/main/Wicked%20West/Assets/Scripts/Player/MinePosition.cs)
 * [PlayerInteraction.cs](https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/blob/main/Wicked%20West/Assets/Scripts/Player/PlayerInteraction.cs)
 
## Retrospective Summary
Here's what went well:
  * Our team continued to use good communication.
  * Work was divided evenly as well as allowing people to take focuses and work on the sections that tied in to their previous work.
  * Different sections integrated together well when worked on seperately, and communication was smooth in patching integration issues.
 
Here's what we'd like to improve:
   * Working throughout the week rather than waiting until right before a meeting to complete issues.
   * Finding different times to work on the project to minimize needing to merge branches.
   * Setting aside time to bug-test functional issues in runtime.
  
Here are changes we plan to implement in the next sprint:
   * Item system upgrade/update
   * Inter-scene system update
   * Setting up a system for adding integrational changes that can't be worked on/merged in parallel.
