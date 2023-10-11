using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    private DialogueManager dialogueInst;
    private Animator animator;
    private CharacterController controller;
    private PlayerInput playerInput;
    private Transform cameraTransform;

    private InputAction moveAction;
    private InputAction lookAction;

    [SerializeField] private float speed; // speed of movement
    [SerializeField] private float turnSpeed; // speed of player turning
    [SerializeField] private float SprintSpeed = 1f;
    [SerializeField] private float WalkSpeed = .4f;

    float turnAngle;

    bool isWalking;
    bool isWalkingStraight;
    bool isWalkingLeft;
    bool groundedPlayer;

    Vector2 moveInput;
    Vector3 move;
    Quaternion targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.4f;
        turnSpeed = 5f;
        animator = GetComponent<Animator>();
        cameraTransform = Camera.main.transform;
        controller = GetComponent<CharacterController>();
        dialogueInst = DialogueManager.GetInstance();
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
    }

    void Update()
    {
        // if dialogue is playing, disable movement
        if (dialogueInst != null)
        {
            if (dialogueInst.dialogueIsPlaying)
            {

                return;
            }
        }

        if(Input.GetKeyDown("left shift"))
        {
            speed = SprintSpeed;
            
        } else if (Input.GetKeyUp("left shift"))
        {
            speed = WalkSpeed;
        }

        // find desired player movement through arrow keys input and direction camera is facing
        moveInput = moveAction.ReadValue<Vector2>();
        move = new Vector3(moveInput.x, 0, moveInput.y);
        move.y = 0f;
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;

        // find desired player rotation through mouse movement
        targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        
        
        // figure out which animation should be playing based on desired player movement
        bool hasHorizontalInput = !Mathf.Approximately(moveInput.x, 0f);
        bool hasVerticalInput = !Mathf.Approximately(moveInput.y, 0f);
        isWalking = hasHorizontalInput || hasVerticalInput;
        isWalkingStraight = !hasHorizontalInput;
        isWalkingLeft = moveInput.x < 0;
        animator.SetBool("IsWalking", isWalking);
        animator.SetBool("IsWalkingStraight", isWalkingStraight);
        animator.SetBool("IsWalkingLeft", isWalkingLeft);
    }

    // only enters this function when an animation is playing (always true for this code since I have an idle animation)
    void OnAnimatorMove()
    {
        // rotate character
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        // figure out angle of movement in global world space and then move player
        if (isWalking) {
            controller.Move(move * Time.deltaTime * speed);
        }
    }
}

