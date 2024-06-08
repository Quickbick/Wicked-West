# Sprint 6 Report (3/2/2024 - 4/2/2024)

## Sprint 6 Demo Video
See the following link to view our sprint demo video: https://file.io/rEgnF68289JK

## What's New (User Facing)
 * New fog type for night time and updated night skybox
 * Fog turns off when the player enters buildings
 * Altar appears in the mine when the player has collected all game items, player places all their items on the altar. 
 * Fixed Saloon model so the bar is shorter
 * New models outside game boundary create a more filled game environment
 * Items are now tied to the various NPCs and can be spawned by getting the NPC to give the item to you.
 * Movement has been redone for better control and feel
 * Button message assistant popups that are Texts for more information.
 * In-Game Feedback Form Link
 * No more Black Screen of Death (BSOD) from custom API Key inputs

## Work Summary (Developer Facing)
During this sprint, our team focused on testing existing game components, while also prioritizing enhancing the game play and improving the overall look and feel of the game. We continued working with the dialog system and are now able to scrub the player's conversations with the NPCs for specific keywords relating to the items in our game. Recognition of these keywords coming from an NPC will make that item available to the player, as if the NPC is giving the item to them. To improve the overall feel of the game, we added a new skybox for the night time, which is now much darker, and implemented some new fog mechanics to improve the look of the environment during the night time as well. There are now two types of fog in the game, a day fog and a night fog, which are only displayed during the correct time of day. We also added new models outside of the game boundary to create a more continuos looking space, and added light above the hiding spots so they are easier to track down during the night. Our play testers made comments on the player's movement not feeling natural, so we also refactored the player's movement during this sprint. RateGPT was started.

## Unfinished Work
If applicable, explain the work you did not finish in this sprint. For issues/user stories in the current sprint that have not been closed, (a) any progress toward completion of the issues has been clearly tracked (by checking the checkboxes of  acceptance criteria), (b) a comment has been added to the issue to explain why the issue could not be completed (e.g., "we ran out of time" or "we did not anticipate it would be so much work"), and (c) the issue is added to a subsequent sprint, so that it can be addressed later.
Mine Scene-To Main scene testing, while occuring for several weeks, has several aspects holding it up, and with a new addition of a new main-to-mine bug, we'll have to discuss at a meeting the way to implement instance references. (https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/412)

## Completed Issues/User Stories
Here are links to the issues that we completed in this sprint:

 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/349 Integrate NPCs With Items
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/391 Edit Saloon model so that bar is shorter. 
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/432 Mine altar model
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/438 Add backgrop
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/439 Add new models to mine scene
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/440 Update Skybox
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/442 Scrub Text From Dialogue Box
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/452 Fog too bright during night time
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/453 Player places items on altar when inventory is full
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/454 Turn off fog when player enters buildings
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/467 Add overlay to hiding spots
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/458 Lights over Hiding Places
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/448 Untextured playermodel
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/429 Revamp monster behavior (chase)
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/423 Fix monster animation not resetting
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/396 add more sound effects to monster
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/479 Spawn Items on NPC death
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/447 Movement refactoring
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/457 Items can be infinitely spawned
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/487 Refactor RateGPT Helpers into GMI + Add to (new) DialogueScrubber (lines 19-21 or updated)
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/476 RateGPT V1.0
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/445 Feedback Form Link In-Game
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/435 Figure Out API Key Issues from Client Feedback
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/409 Msg Instance

 ## Incomplete Issues/User Stories
 Here are links to issues we worked on but did not complete in this sprint:
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/412 [Though this was worked on, mine-scene to main scene problems exist, and now new main scene-to mine scene issues have come up too].
 
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/446 Full keybind menu is currently a stretch goal

## Code Files for Review
Please review the following code files, which were actively developed during this sprint, for quality:
 * [activateAltar.cs](https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/blob/main/Wicked%20West/Assets/activateAltar.cs)
 * [PlayerInteraction.cs](https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/blob/main/Wicked%20West/Assets/Scripts/Player/PlayerInteraction.cs)
 * [PlayerMovement.cs](https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/blob/main/Wicked%20West/Assets/Scripts/Player/PlayerMovement.cs)
 * [GameManager.cs](https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/blob/main/Wicked%20West/Assets/GameManager.cs)
 
## Retrospective Summary
Here's what went well:
  * Our team had been making great progress week after week.
  * Each team member knows what each other is planning on working on each week. 
  * We had a few testers who were able to provide valuable insight and uncover new bugs. 
 
Here's what we'd like to improve:
   * We would like to gather feedback from more play testers. 
   * The mine scene is still in need of more testing. 
   * Adding gameplay mechanics to make the game more fun and interesting, such as a way to fight or stun the monster. 
  
Here are changes we plan to implement in the next sprint:
   * We will be publishing an up to date version of our game on itch.io for play testing. 
   * We will continue testing the mine scene so that there is seamless integration between it and our main scene by the time of our presentation.
   * A new detailed plan for inter-scene instance handling and null-reference handling.
