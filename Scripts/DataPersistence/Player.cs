using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,ISaveState
{
    [SerializeField] private float speed = .6f;
    [SerializeField] private Vector3 turnSpeed = new Vector3(0, 120, 0);
    float horizontal;
    float vertical;
    float turn;
    bool isWalking;
    bool isWalkingStraight;
    bool isWalkingLeft;
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_DeltaRotation;
    Vector3 globalForward = new Vector3(0, 0, 1);
    public int Stars
    {
        get; private set;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame --> Rendering // Speed changes based on FPS of computer and complexity of computations // Animator uses this by default
    // FixedUpdate is called, on default, 50 times every second --> Physics System // Same speed // Rigidbody uses this by default
    //    - have to use FixedUpdate because OnAnimatorMove() has Rigidbody changes, which default to physics computations that rely on values determined in FixedUpdate()
    void FixedUpdate()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        isWalking = hasHorizontalInput || hasVerticalInput;
        isWalkingStraight = !hasHorizontalInput;
        isWalkingLeft = m_Movement.x < 0;
        m_Animator.SetBool("IsWalking", isWalking);
        m_Animator.SetBool("IsWalkingStraight", isWalkingStraight);
        m_Animator.SetBool("IsWalkingLeft", isWalkingLeft);
        turn = Input.GetAxis("Mouse X");
        m_DeltaRotation = Quaternion.Euler(turn * turnSpeed * Time.deltaTime);
    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * m_DeltaRotation);
        if (isWalking)
        {
            float angle = Vector3.Angle(globalForward, m_Movement);
            Quaternion differenceRotation;
            if (m_Movement.x < 0)
            {
                differenceRotation = Quaternion.AngleAxis(-angle, transform.up);
            }
            else
            {
                differenceRotation = Quaternion.AngleAxis(angle, transform.up);
            }
            Vector3 direction = differenceRotation * transform.forward;
            m_Rigidbody.MovePosition(m_Rigidbody.position + direction * speed * Time.deltaTime);
        }
    }

    public void Save()
    {
        var json = JsonUtility.ToJson(transform.position);
        PlayerPrefs.SetString("PlayerPosition", json);
        PlayerPrefs.SetInt("PlayerStars", Stars);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("PlayerPosition"))
        {
            var json = PlayerPrefs.GetString("PlayerPosition");
            transform.position = JsonUtility.FromJson<Vector3>(json);
            Stars = PlayerPrefs.GetInt("PlayerStars");

        }
    }

    public void AddStars()
    {
        Stars++;
    }

   
}


