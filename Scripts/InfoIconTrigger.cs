using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoIconTrigger : MonoBehaviour
{
    [SerializeField] private GameObject InfoIcon;
    [SerializeField] private GameObject UIToBeTriggered;
    private DialogueManager dialogueInst;
    private bool playerInRange;

    private void Start()
    {
        dialogueInst = DialogueManager.GetInstance();
        UIToBeTriggered.SetActive(false);
        playerInRange = false;
    }

    void Update()
    {
        if (playerInRange && !dialogueInst.dialogueIsPlaying && !UIToBeTriggered.activeSelf)
        {
            InfoIcon.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Z))
            {
                UIToBeTriggered.SetActive(true);
            }
        }
        else
        {
            InfoIcon.SetActive(false);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
