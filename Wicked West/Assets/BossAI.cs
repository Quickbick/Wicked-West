using Mono.CSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    GameObject player;

    NavMeshAgent agent;

    [SerializeField] LayerMask groundLayer, playerLayer;

    Animator animator;

    public AudioSource audioSource;
    public AudioClip scream;
    public AudioClip bite;

    BoxCollider boxCollider;

    //patrolling
    Vector3 destPoint;
    bool walkpointSet;
    [SerializeField] float range;

    // State change
    [SerializeField] float sightRange, attackRange;
    bool playerInSight, playerInAttackRange;
    bool isSpotted = false; //whenever the monster spots the player
    bool isChasing = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        // if (player != null) Debug.Log("Found player");
        animator = GetComponent<Animator>();
        // audioSource = GetComponent<AudioSource>();
        boxCollider = GetComponentInChildren<BoxCollider>();
    }

    void OnEnable()
    {
        isSpotted = false; //whenever the monster spots the player
        isChasing = false;
    }

    // Update is called once per frame
    void Update()
    {
        // check if player is in ranges
        playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        agent.velocity = agent.desiredVelocity; // doesnt slide

        // Monster states

        // if 

        if ((!playerInSight && !playerInAttackRange && !isSpotted && !isChasing) || GameManager.Instance.isHiding) Patrol();

        // If player is hiding, monster cannot chase
        // If player hides while being chased/attacked, trigger hide function

        if (!GameManager.Instance.isHiding)
        {
            if (playerInSight && !playerInAttackRange && !isSpotted && !isChasing) Alert();
            if (isSpotted) LookAt(player.transform);
            if (isChasing) Chase();
            if (playerInAttackRange) Attack();
        }

        // DEBUG TEST H
        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    Debug.Log(GameManager.Instance.isHiding);
        //    if (GameManager.Instance.isHiding == false)
        //        GameManager.Instance.isHiding = true;
        //    else
        //        GameManager.Instance.isHiding = false;
        //}

        if (GameManager.Instance.isPaused == true)
        {
            audioSource.Pause();
        }
    }

    // set to true once player is hidden
    bool hideInvoked = false;

    void checkPlayerHidden()
    {
        // Check if hiding 
        if (GameManager.Instance.isHiding == true && hideInvoked == false)
        {
            Invoke("Hide", 3);
            hideInvoked = true;
            //boxCollider.enabled = false;
        } 
    }

    // MAKE SURE TO SET AT LEAST 3 OF THESE INCLUDING THE MINE SCENE
    [SerializeField]
    GameObject teleport1;
    [SerializeField]
    GameObject teleport2;
    [SerializeField]
    GameObject teleport3;

    // Teleport to random location
    void Hide()
    {
        if (GameManager.Instance.isHiding == true)
        {
            int random = Random.Range(1, 3);

            switch (random)
            {
                case 1:
                    Teleport(teleport1.transform);
                    break;
                case 2:
                    Teleport(teleport2.transform);
                    break;
                case 3:
                    Teleport(teleport3.transform);
                    break;
            }
        }
        hideInvoked = false;
    }

    // Player enters monster range, isSpotted not true
    void Alert()
    {
        isSpotted = true;
        agent.speed = 0;
        LookAt(player.transform);
        Invoke("AlertEnd", 3.0f);
        Debug.Log("Alerted");

        audioSource.clip = scream;
        audioSource.Play();
    }

    void AlertEnd()
    {
        Debug.Log("Chasing");
        isSpotted = false;
        isChasing = true;
    }

    // Player spotted, is chasing
    void Chase()
    {
        agent.SetDestination(player.transform.position);
        agent.speed = 10;
        LookAt(player.transform);

        //checkPlayerHidden();
    }

    // Player in range, attack
    void Attack()
    {
        animator.SetTrigger("Attack");
        agent.SetDestination(transform.position); // stop walking

        //checkPlayerHidden();
    }

    void PlayBite()
    {
        audioSource.PlayOneShot(bite, 0.7f);
    }

    void EnableAttack()
    {
        boxCollider.enabled = true;
    }

    void DisableAttack()
    {
        boxCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Attack");
        if (other.CompareTag("Player"))
        {
            // Add player damage function here! :3
            if (other.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth player))
            {
                player.takeDamage();
            }
        }
    }

    // Player not spotted, moving around
    void Patrol()
    {
        isSpotted = false;
        isChasing = false;

        if (!walkpointSet) SearchForDest();
        if (walkpointSet) { agent.SetDestination(destPoint); }
        if (Vector3.Distance(player.transform.position, destPoint) > 50) walkpointSet = false; //if the player has moved x units from the monster's last recorded destination, set a new destination

        agent.speed = 5;
        LookAt(destPoint);

        // check the distance to the player, if within range then invoke hide
        if (playerInSight)
            checkPlayerHidden();
    }

    void SearchForDest()
    {
        float z = Random.Range(-range, range);
        float x = Random.Range(-range, range);

        // Debug.Log("New destination set");

        destPoint = new Vector3(player.transform.position.x + x, player.transform.position.y, player.transform.position.z + z);

        // Issue: Need to check if destpoint is valid, otherwise monster may not move until new position is calculated

        // disabling this might be a temporary fix. Hopefully can keep this way.
        //if (Physics.Raycast(destPoint, Vector3.down, groundLayer))
        //{
        walkpointSet = true;
        //}
    }

    // Look at point only on the Yaxis
    void LookAt(Transform target)
    {
        // rotation towards player
        transform.LookAt(target);
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles.x = 0;
        eulerAngles.z = 0;

        // Set the altered rotation back
        transform.rotation = Quaternion.Euler(eulerAngles);
    }
    void LookAt(Vector3 target)
    {
        // rotation towards player
        transform.LookAt(target);
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles.x = 0;
        eulerAngles.z = 0;

        // Set the altered rotation back
        transform.rotation = Quaternion.Euler(eulerAngles);
    }

    // Teleport the enemy to desired location (to be used for night events, after player hides)
    public void Teleport(Transform target)
    {
        transform.position = target.position;
    }
}
