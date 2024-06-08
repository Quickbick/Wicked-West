# Sprint 7 Report (4/2/2024 - 5/2/2024)

## Sprint 7 Demo Video
See the following link to view our sprint demo video: https://www.youtube.com/watch?v=qIk6_8sA1vY

## What's New (User Facing)
 * Text prompts in addition to crosshairs to inform users how they can interact with different objects.
 * Boss fight is triggered after the player adds all the items to the altar.
 * Journal with the game map and instructions on what items the player needs to gather.
 * Item spawn after NPC dies.
 * Rate GPT function which determines if an NPC is willingly handing over an item to the player.
 * Updated and adjusted NPC dialogue settings
 * Many Bug Fixes

## Work Summary (Developer Facing)
This sprint our team worked to get the game to a finished playable state, to do this we fixed bugs, cleaned up and finished mechanics, and put the game through user testing. We moved twoards our end goal and were able to get the project to a point that we were all satisfied with. The biggest challenges we faced were integration of systems that had not been integrated before, which led to many of the bugs we needed to resolve this sprint. The biggest issue was scene changes, which caused problems with many of the already implemented systems. Another big challenge was doing everything we did in limited time. This sprint took place in one of the busiest times of the semester, which meant it was paired with other projects and exams. This limited the total working time our group had and forced us to be smarter and plan better when it came to devoting time to this project. Additionally, the saving and loading mechanics were added back in, which had to be done manually and with primitive type saving after all features were in. This, while a challenge, was solved by simplifying our saving system, such as how each loaded game loads from the start of a day, rather than from in the middle of a course of player action. Additionally, saving in night is now prohibited to ensure saves and loads could work right without causing game-breaking failures.

## Unfinished Work
With this being our final sprint, we are submitting our game in a complete condition and are satisfied with the work we are turning in. While we all have ideas on ways to improve the game in the future, our team was able to accomplish everything we set out to do during this sprint. We now have a build of the game that can be played from start to finish. 

## Completed Issues/User Stories
Here are links to the issues that we completed in this sprint:

 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/491 Fixed fog references
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/492 Interact text under crosshairs
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/496 Adjust door colliders
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/499 Altar animation
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/501 Player no longer facing correct direction when entering mine
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/505 NPCs ask to follow
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/515 Enhance Item Giving Conditions
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/524 Inventory Resets on Scene Change
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/538 Pressing ESC in dialogue to walk away
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/539 Index Error on inventory open
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/553 Primitive Save/Loads of Alive/Dead system
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/552 Primitive Save/Loads of Inventory
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/551 Primitive Save/Loads for day/night/lives, etc. + daytime saves requirement implemented
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/531 Last (Before Presentation Build): SKYBOXs, RateGPT Threshold Adjuster, NightTimer Patch
 * https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/issues/502 RateGPT v1.25
 
 ## Incomplete Issues/User Stories
 Overall, we are happy with the state that our game is in. There were no issues we were planning for this sprint that we weren't able to implement. 

## Code Files for Review
Please review the following code files, which were actively developed during this sprint, for quality:
 * NewSaves.cs(https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/blob/main/Wicked%20West/Assets/NewSaves.cs)
 * RateGPT.cs(https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/blob/main/Wicked%20West/Assets/Scripts/Dialogue/RateGPT.cs)
 * LoadGameStateManager.cs(https://github.com/WSUCptSCapstone-F23-S24/msft-aiassistedgamedev/blob/main/Wicked%20West/Assets/LoadGameStateManager.cs)
 
## Retrospective Summary
Here's what went well:
  * Having play testers helped us uncover many new bugs.
  * Had a complete game ready to play during our final presentation.
  * We're proud of the work we've put in over the last two semesters. 
 
Here's what we'd like to improve:
   * We would like to continue testing the game and fixing new bugs that come out.
   * Potential to make some of the game play components more interesting.
   * Further handling for break points in the future, such as when API rules/requirements change from OpenAI's end.
  
Here are changes we plan to implement in the next sprint: There is no next sprint
