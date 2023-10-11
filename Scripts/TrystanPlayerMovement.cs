using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrystanPlayerMovement : MonoBehaviour
{
    private DialogueManager dialogueInst;
    CharacterController controller;
    Animator animator;
    Rigidbody rigidbody;
    public Transform cam;
    float speed;
    float turnSmoothTime;
    [SerializeField] private float SprintSpeed = 1f;
    [SerializeField] private float WalkSpeed = .6f;
    float turnSmoothVelocity;
    bool isWalking;
    bool isWalkingStraight;
    bool isWalkingLeft;

    void Start() {
        speed = 0.6f;
        turnSmoothTime = 0.01f;
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
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

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // figure out which animation should be playing based on desired player movement
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        isWalking = hasHorizontalInput || hasVerticalInput;
        isWalkingStraight = !hasHorizontalInput;
        isWalkingLeft = horizontal < 0;
        animator.SetBool("IsWalking", isWalking);
        animator.SetBool("IsWalkingStraight", isWalkingStraight);
        animator.SetBool("IsWalkingLeft", isWalkingLeft);

        if (direction.magnitude > 0)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}
