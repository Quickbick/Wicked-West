# Sprint x Report (10/6/23 - 11/5/23)

## Sprint 2 Demo Video
See the following link to view our sprint demo video: https://1drv.ms/v/s!AjR7hx24e-B5hIAh44IQ66LqbnKUkg?e=zjc1r1

See the following for a one-time download in case the above does not work: https://file.io/NdmFISmgVm7U

## What's New (User Facing)
 * 3D Saloon Model
 * 3D Shed Model that players can walk into
 * 3D Inn Model
 * Unity Scene with all models so far
 * Crosshair Now Changes To Highlight Types of Interactable Objects
 * Items (Currently Small Square Boxes) Can Be Picked Up By Pressing E When In Interact Range
 * A Demo NPC Now Can Be Briefly Spoken To
 * Pause screen
 * Start menu on game run
 * Player and game fields can be saved
 * In-game saving button in pause menu
 * Quit application system + button
 * Transitional fades integrated
 * Saving Dialogue Conversation Lists added
 * Scene load heiarchy setup
 * Outhouse door .anim patch
 * 16:9 aspect ratio implemented in build settings
 * In-game object collisions with buildings and stages
 * AudioManager with pitch, loop, and volume settings.

## Work Summary (Developer Facing)
During this sprint our team began implemeting an actual game space, complete with templates of various things that will be seen in the actual game as well as menus and other backend pieces required to make it all work. This involved a lot of frameworks on the programming side which will allow for easier creation of further features in the future. We also added a large amount of art and model assets to the game this sprint. We discovered a lot of complexity that comes with the integration of frameworks, APIs, and other premade materals. A lot of time was spent on art assets, which was more than was anticipated by the team. We also further learned about the Unity engine and some of the various smaller issues that can come with working in it.
Within Unity, we have newly integrated an OpenAI-connected Dialogue System, which is set up with our regular API key. This will allow GPT conversation within the environment to come. Additionally, other changes not apparent to the user during this Sprint integrated have been a background audio management script, which can handle all sounds with pitch/volume control variables, and new helper methods for testing within our Unity Editor environment.

## Unfinished Work
The only work planned for this sprint that was left unfinished were some design items regarding the monster as well as the items that can be found by the player. The monster design is unfinished due to a recently released game having a similar monster and our group deciding to do a redesign to lessen similarities. The item list still needs to be finalized by the group, and further work on the inventory was put off to next sprint, so having items designed was not needed for this sprint.

## Completed Issues/User Stories
Here are links to the issues that we completed in this sprint:

 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/16 Saloon Model
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/64 Shed Model
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/65 Inn Model
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/66 Add current models into game
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/67 Player Interaction Range and Item Detection
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/83 Basic Dialogue System
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/104 Item Interaction and Inventory System Framework
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/105 Item Class
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/114 Pause Menu Designed 
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/79 Create Stylized Crosshairs
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/113 Start Menu Designed
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/80 Create Stylized Menu Buttons
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/106 Game Aspect Ratio/Resolution Setup
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/102 Integrate Dialogue System OpenAI Addon into Unity Project
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/96 Addition of Environment Requirements to Testing and Acceptance Plan
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/94 Finish In-Game GameManager Implementation
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/74 Implement Save/Load by System Requirements Specification
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/72 Update OpenAI Usage
 
 ## Incomplete Issues/User Stories
 Here are links to issues we worked on but did not complete in this sprint:
 
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/86 Finalize monster design and model <<Recently discovered design is extremely similar to another game's, will redesign and produce model then>>
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/68 <<Need to decide on final items\>>

## Code Files for Review
Please review the following code files, which were actively developed during this sprint, for quality:
 * (https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/blob/main/Wicked%20West/Assets/Scripts/SaveLoadScript.cs) [SaveLoadScript.cs]
 * (https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/blob/main/Wicked%20West/Assets/GameManager.cs) [GameManager.cs]
 * (https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/blob/main/Wicked%20West/Assets/Scripts/AudioManagerScript.cs) [AudioManagerScript.cs]
 
## Retrospective Summary
Here's what went well:
  * Lots of frameworks have been created that will allow for easy reuse and creation of the more unique elements that will be seen in the final game.
  * Fast communication in helping merge conflicts, commit issues, and more.
  * High quality artwork is beginning to give the game it's own identity.
 
Here's what we'd like to improve:
   * Working with the newly integrated DialogueSystem, learning its logic, and understanding the pricepoint of each conversation.
   * Movement system of the player could be optimized to improve gameplay / the in-game testing environment.
   * Consolidation scattered files and items within Unity.
  
Here are changes we plan to implement in the next sprint:
   * Individual Items and NPCs That Will Be Seen In The Actual Game
   * Addition of sliders for giving control of audio levels to the users for music+sound.
   * A Functional Inventory System
