using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static ActiveNPCLogic;

// This will be used for managing the monster's location on different nights, along with the night's objectives.
//ONLY USE ON THE MAIN SCENE, NOT CAVE SCENE
public class MonsterManager : MonoBehaviour
{
    public GameObject monster;
    public GameObject bossMonster;
    public GameObject objective;
    public GameObject bossObjective;
    public GameObject npchandler;

    public int random; //random npc to die that night

    [SerializeField] public List<GameObject> spawnLocations;

    public float healthStart;
    public int curPhase;

    private bool nightStarted = false;
    private

    // Start is called before the first frame update
    void Start()
    {
        monster = GameObject.Find("monster");
        bossMonster = GameObject.Find("bossmonster");
        objective = GameObject.Find("nightObjective");
        bossObjective = GameObject.Find("bossobjective");
        npchandler = GameObject.Find("NPCStateHandler");
        curPhase = GameManager.Instance.phase;

        monster.SetActive(false);
        objective.SetActive(false);
        bossMonster.SetActive(false);
        bossObjective.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.Instance.isNighttime == false) //daytime logic
        {
            nightStarted = false;
            monster.SetActive(false);
            objective.SetActive(false);
            bossMonster.SetActive(false);
            bossObjective.SetActive(false);

            if (GameManager.Instance.days == 8)
            {
                GameManager.DestroyInstance();
                SceneManager.LoadScene("LoseScene");
            }
        }
        else //nighttime logic
        {
            if (nightStarted == false)
            {
                newNight();

                // Set objectives active
                monster.SetActive(true);
                objective.SetActive(true);

                healthStart = GameManager.Instance.Health;

                // if altar is completed...

            }

            // if altar is activated
            if (GameManager.Instance.bossActive == true)
            {
                // disable normal monster and objective
                monster.SetActive(false);
                objective.SetActive(false);

                // set boss stuff to active
                bossMonster.SetActive(true);
                bossObjective.SetActive(true);


                // check current phase constantly to update (boss) monster and objective location
                if (curPhase != GameManager.Instance.phase)
                {
                    curPhase = GameManager.Instance.phase;
                    switchSpawn();
                }

                // check the damage
                checkDamageBoss();
            }
            else
            {
                // check if the player has lost health and BOSS OBJ NOT ACTIVE
                checkDamage();
            }

        }
    }

    // Set spawn of objective and monster
    // Right now, will randomly assign here. Maybe move outside of this script?
    public void newNight()
    {
        nightStarted = true;

        random = Random.Range(0, (GameManager.Instance.attacked.Count) - 1);
        GameManager.Instance.attacked.Remove(random);

        switch (random)
        {
            case 0:
                SetSpawn("Hunter");
                break;
            case 1:
                SetSpawn("Miner");
                break;
            case 2:
                SetSpawn("Salesman");
                break;
            case 3:
                SetSpawn("Blacksmith");
                break;
            case 4:
                SetSpawn("Bartender");
                break;
            case 5:
                SetSpawn("Pastor");
                break;
            case 6:
                SetSpawn("Sheriff");
                break;
            case 7:
                SetSpawn("Researcher");
                break;
        }
    }

    // randomly shift location of objective (for boss fight)
    public void switchSpawn()
    {
        random = Random.Range(0, 7);

        switch (random)
        {
            case 0:
                SetSpawnBoss("Hunter");
                break;
            case 1:
                SetSpawnBoss("Miner");
                break;
            case 2:
                SetSpawnBoss("Salesman");
                break;
            case 3:
                SetSpawnBoss("Blacksmith");
                break;
            case 4:
                SetSpawnBoss("Bartender");
                break;
            case 5:
                SetSpawnBoss("Pastor");
                break;
            case 6:
                SetSpawnBoss("Sheriff");
                break;
            case 7:
                SetSpawnBoss("Researcher");
                break;
        }
    }

    // Set the spawn of the monster and objective each night.
    public void SetSpawn(string npc)
    {
        monster.TryGetComponent<EnemyAI>(out EnemyAI enemy);
        objective.TryGetComponent<NightObjective>(out NightObjective obj);

        Debug.Log(npc);

        switch (npc)
        {
            case "Sheriff":
                enemy.Teleport(spawnLocations[0].transform);
                obj.Teleport(spawnLocations[0].transform);
                break;
            case "Miner":
                enemy.Teleport(spawnLocations[1].transform);
                obj.Teleport(spawnLocations[1].transform);
                break;
            case "Hunter":
                enemy.Teleport(spawnLocations[2].transform);
                obj.Teleport(spawnLocations[2].transform);
                break;
            case "Bartender":
                enemy.Teleport(spawnLocations[3].transform);
                obj.Teleport(spawnLocations[3].transform);
                break;
            case "Blacksmith":
                enemy.Teleport(spawnLocations[4].transform);
                obj.Teleport(spawnLocations[4].transform);
                break;
            case "Researcher":
                enemy.Teleport(spawnLocations[5].transform);
                obj.Teleport(spawnLocations[5].transform);
                break;
            case "Pastor":
                enemy.Teleport(spawnLocations[6].transform);
                obj.Teleport(spawnLocations[6].transform);
                break;
            case "Salesman":
                enemy.Teleport(spawnLocations[7].transform);
                obj.Teleport(spawnLocations[7].transform);
                break;
        }
    }

    // Set the spawn of the monster and objective each night.
    public void SetSpawnBoss(string npc)
    {
        bossMonster.TryGetComponent<BossAI>(out BossAI enemy);
        bossObjective.TryGetComponent<BossObjective>(out BossObjective obj);

        Debug.Log(npc);

        switch (npc)
        {
            case "Sheriff":
                enemy.Teleport(spawnLocations[0].transform);
                obj.Teleport(spawnLocations[0].transform);
                break;
            case "Miner":
                enemy.Teleport(spawnLocations[1].transform);
                obj.Teleport(spawnLocations[1].transform);
                break;
            case "Hunter":
                enemy.Teleport(spawnLocations[2].transform);
                obj.Teleport(spawnLocations[2].transform);
                break;
            case "Bartender":
                enemy.Teleport(spawnLocations[3].transform);
                obj.Teleport(spawnLocations[3].transform);
                break;
            case "Blacksmith":
                enemy.Teleport(spawnLocations[4].transform);
                obj.Teleport(spawnLocations[4].transform);
                break;
            case "Researcher":
                enemy.Teleport(spawnLocations[5].transform);
                obj.Teleport(spawnLocations[5].transform);
                break;
            case "Pastor":
                enemy.Teleport(spawnLocations[6].transform);
                obj.Teleport(spawnLocations[6].transform);
                break;
            case "Salesman":
                enemy.Teleport(spawnLocations[7].transform);
                obj.Teleport(spawnLocations[7].transform);
                break;
        }
    }

    public void checkDamage()
    {
        // if player takes damage, just wipe from map
        // This should really only be temporary
        if (GameManager.Instance.Health < healthStart)
        {
            monster.SetActive(false);
            if (objective.TryGetComponent<NightObjective>(out NightObjective no))
            {
                no.Reset();
            }
            objective.SetActive(false);

            // set npc to deceased
            if (GameManager.Instance.objComplete == false)
            {
                if (npchandler.TryGetComponent<ActiveNPCLogic>(out ActiveNPCLogic npc))
                {
                    npc.KillNPC(random);
                }
            }
        }
    }

    // when the boss is active, health will be checked differently
    public void checkDamageBoss()
    {
        // if player takes damage, just wipe from map
        // This should really only be temporary
        if (GameManager.Instance.Health < healthStart)
        {
            healthStart = GameManager.Instance.Health;
            switchSpawn();
        }

        // manage health here if boss is active. just to avoid more player changes
        if (GameManager.Instance.Health <= 0)
        {
            GameManager.DestroyInstance();
            SceneManager.LoadScene("LoseScene");
        }
    }
}

