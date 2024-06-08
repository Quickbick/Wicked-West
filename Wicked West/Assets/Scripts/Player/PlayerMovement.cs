using Language.Lua;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFSW.QC;

public class PlayerMovement : MonoBehaviour{
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool readyToJump;

    public float offset;
    public LayerMask whatIsGround;
    private bool grounded;

    public Transform orientation;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;

    public bool haltMovement; // * A script to make sure transform.position isn't being effected (movement) during teleport times (ex: falling asleep)

    private Rigidbody rb;

    // Sprint variables
    public float Stamina; // Player Stamina
    private float MaxStamina = 1000; // Max player stamina
    private float normalSpeed;
    public float sprintMod;
    public float sprintTime = 50;
    private float curFOV; // used for changing fov during sprint
    private float fovMod = .2f;
    public Image staminaBar;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    private void Start(){
        haltMovement = false;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;

        //sprint initilization
        normalSpeed = moveSpeed;
        GameManager.Instance.Stamina = MaxStamina;
        curFOV = Camera.main.fieldOfView;
    }

    private void Update(){
            //ground check
            grounded = Physics.Raycast(transform.position, Vector3.down, GetComponent<Collider>().bounds.extents.y + offset, whatIsGround);

            //update stamina bar
            staminaBar.fillAmount = GameManager.Instance.Stamina / MaxStamina; // ! modify sprint bar image

            if (!haltMovement && !GameManager.Instance.isHiding){
                CollectInput();
            }
            SpeedControl();

            //handle drag
            if (grounded){
                rb.drag = groundDrag;
            }
            else{
                rb.drag = 0;
            }

            if (GameManager.Instance.transform.Find("Timer").GetComponent<DayCycleIncrementer>().fadeToNightAKASleep){
                gameObject.GetComponent<MinePosition>().PlayerFallenSleepAction(); // ! Call vector teleport (should teleport to house with bed, wherever that is)
                GameManager.Instance.transform.Find("Timer").GetComponent<DayCycleIncrementer>().fadeToNightAKASleep = false;

            }

            if (GameManager.Instance.transform.Find("NightTimer").GetComponent<NightCycleIncrementer>().fadeToDayAKASleep){
                gameObject.GetComponent<MinePosition>().PlayerMadeMorningAction(); // ! Call vector teleport (should teleport to house with bed, wherever that is)
                GameManager.Instance.transform.Find("NightTimer").GetComponent<NightCycleIncrementer>().fadeToDayAKASleep = false;
            }

            if (GameManager.Instance.transform.Find("Timer").GetComponent<DayCycleIncrementer>().fadeToNightAKAHaltMovement) {
                haltMovement = true;
                GameManager.Instance.transform.Find("Timer").GetComponent<DayCycleIncrementer>().fadeToNightAKAHaltMovement = false; 
            }

            if (GameManager.Instance.transform.Find("NightTimer").GetComponent<NightCycleIncrementer>().fadeToDayAKAHaltMovement) {
                haltMovement = true;
                GameManager.Instance.transform.Find("NightTimer").GetComponent<NightCycleIncrementer>().fadeToDayAKAHaltMovement = false; 
            }
    }

    private void FixedUpdate(){
        MovePlayer();
        moveSpeed = normalSpeed;
    }

    //collects player input from Unity
    private void CollectInput(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded){
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        //when to sprint
        if (Input.GetKey(sprintKey) && GameManager.Instance.Stamina >= sprintTime){
            Sprint();
        }
        else{
            if (GameManager.Instance.Stamina < MaxStamina) GameManager.Instance.Stamina += sprintTime / 2;
        }
    }

    //moves player based on input
    private void MovePlayer(){
        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        
        if (grounded){ //on ground
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded){
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }


    }

    //sets speed cap
    private void SpeedControl(){
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velocity if needed
        if(flatVel.magnitude > moveSpeed){
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    //launches player upwards
    private void Jump(){
        //reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    //resets jump
    private void ResetJump(){
        readyToJump = true;
    }

    //Sprint function.
    private void Sprint()
    {
        if (GameManager.Instance.Stamina > MaxStamina / 5){
            moveSpeed = normalSpeed * sprintMod;
        }
        else{
            moveSpeed = normalSpeed * (((sprintMod - 1) / 2) + 1);
        }
        //decrease sprint bar
        if (GameManager.Instance.isNighttime)
            GameManager.Instance.Stamina -= sprintTime;
    }

    //loads player position and rotation
    private void StartFromSavedLocation() {
        // Check if the savedTransform is not null
        if (ES3.KeyExists("pFieldPlayerPosition")) { // * if exists, rotation and scale must exist too....

            Vector3 savedPosition = ES3.Load<Vector3>("pFieldPlayerPosition");
            Quaternion savedRotation = ES3.Load<Quaternion>("pFieldPlayerRotation");
            Vector3 savedScale = ES3.Load<Vector3>("pFieldPlayerScale");

            // Set the GameObject's position, rotation, and scale
            transform.position = savedPosition;
            transform.rotation = savedRotation;
            transform.localScale = savedScale;

            Debug.Log("Loaded from saved position.............");
        } else {
            Debug.Log("No pFieldPlayerPosition to load from, so loading from default starting point.");
        }

    }

        // Command to change some variables in-game w/ dev console:
    [Command]
    public void ChangePlayerMoveSpeed(float newSpeed) {

        moveSpeed = newSpeed;
        Debug.Log("Player stat Updated!");

    }

    [Command]
    public void ChangePlayerMaxStamina(float newSpeed) {

        MaxStamina = newSpeed;
        Debug.Log("Player stat Updated!");

    }

    // ? Not certain, but seems to relate to stamina....
    [Command]
    public void ChangePlayerNormalSpeed(float newSpeed) { // ! Correct one for moving/speed, I think.

        normalSpeed = newSpeed;
        Debug.Log("Player stat Updated!");

    }

    [Command]
    public void ChangeJumpHeight(float newJumpHeight) {

        jumpForce = newJumpHeight;
        Debug.Log("Player stat Updated!");

    }
}
