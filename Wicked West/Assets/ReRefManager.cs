using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// * Message on Code Comments in this File:
// ? INSTALL + APPLY the plugin 'Better Comments' by iguissouma to see color-differentiation between comments dependent on header sybmols (*, ?, !, etc.)

public class ReRefManager : MonoBehaviour {

    [SerializeField] public GameObject crossHair; // * Populate in inspector
    [SerializeField] public GameObject gamePauseCanvasObject; // * Populate in inspector also
    private string lastBoolString; // * local bool-like string to remember what was last, during update lookups of crosshair checks.
    
    private void Start() {
        
        lastBoolString = "nothing yet...";

    }

    // * Properly updated the Crosshair and pauseCanvas in update here:
    private void Update() {

        if (GameManager.Instance.isPaused) { // * check each frame if paused or not, then check if different, and update if so.

            if (lastBoolString != "t") {

                crossHair.SetActive(false); 
                //gamePauseCanvasObject.SetActive(true);   //! MOVED TO GameManager.cs [01/24/24] so other functions could use pause effects without menu.
                lastBoolString = "t";

            } // ! This could get added maybe:  // else { return; }

        }

        else {

            if (lastBoolString != "f") {

                crossHair.SetActive(true);
                //gamePauseCanvasObject.SetActive(false); //! MOVED TO GameManager.cs [01/24/24] so other functions could use pause effects without menu.
                lastBoolString = "f";

            } // ! else { return; }

        }

    }

}

// * Saves testing notes:

// * ITERATION 1:
// "DAYDAY"
// "Day 16"
// "Time: 00h:15m:51s

// ! sus files edited in git so far: 
// none


//Todo ACTION: Save Game after 20ish seconds of playtime, on the porch of a building.

// * ITERATION 2:
// "DAYDAY"
// "Day 16"
// "Time: 00h:16m:13s // ? Wait..... this means timing system for days
//                       ? can be replaced entirely if its not properly linked
//                       ? with the time clock in saving/loading scenarios....

// ! sus files edited in git so far: 
// * none!!!!?!

// ?NOTE: Spawned in correctly in correct location


// Todo ACTION: Get to day 17, and walk to an npc far-ish away.'
// Todo ACTION: Save, Unpause, Pause, Quit button.

// * ITERATION 3:
// "DAYDAY"
// "Day 16"
// "Time: 00h:16m:13s // ? Wait..... this means timing system for days
//                       ? can be replaced entirely if its not properly linked
//                       ? with the time clock in saving/loading scenarios....

// ! sus files edited in git so far: 
// * none!!! c:

// * ITERATION 4:
// "DAYDAY"
// "Day 17"
// "Time: 00h:16m:53s 

// Todo DEV: Closed and committed, have now reopened the application.

// * Note: It seems unity's lock file could be keeping ES3 Reference Manager from adding any more references
// *       now that auto-add has been disabled and that system has been changed, since it goes from 3210 refs
// *       to 4208 in runtime, but as long as it gets back to editor and out, it goes back to 3210 and same
//*        hash-type file, I believe.......


// Todo ACTION: Save, Unpause, Pause, Quit button.

// ! sus files edited in git so far: 
// * none!!! c: (yes really still, surprisingly... I think I've got them all, now, I believe...)

// * ITERATION 5:
// "DAYDAY"
// "Day 17"
// "Time: 00h:16m:58s 

// *Note: I see backups getting saved to Temp\_Backup...... but hopefully temp gets cleared (I thought it was already in gitignore, was that overridden?)

// Todo DEV: Closed and committed again, have now reopened the application.

// * I've now tried pressing new game, wait and fade are nice, debug says cleared keys, then waited until day 18 and saved, then quit.....
// "DAYDAY"
// "Day 18"
// "Time: 00h:17m:48s 

// ! sus files edited in git so far:
// ! Actually, its bad they were not... makes me think possibly wrong file is being cleared?


// * ITERATION 6:
// ! BIG Test without directory header:

// ? SUS FILES CHANGED ON KEY CLEAR:
// ? None...????? Holy we might be golden and git-friendly going forward....

// "Player"
// "Day: 1"
// "Time: Xh:Xm 

// * [SUCCESS]:
// * - Location successful.
// * - Home screen stats successful.

// Todo ACTION: Save again after 0->1 day increment.

// * ITERATION 7: 
// "Wholesome East" 
// "Day 1"
// "Time: 00h:01m:02s 

// * [01/21/24] Downtime notes for team:

//   ! Can now Save in-game without causing merge-conflicts.
//   ! Can also do New Game option once again (reset keys file) without causing merge-conflicts.
//   ! New Game re-implemented and enabled.
//   * ES3 won't (shouldn't) trigger any more reference saves unless specifically triggered to, which we should avoid.

// Todo: Pull new updates from main or start new branches w/ origin main.

// ? Basically, Save and New Game are 100% git-friendly, I believe. If any new implementation are broken lmk!!
//   See ReRefManager.cs for more documentation on testing + patch + re-implementation.