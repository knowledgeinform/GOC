using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIZtrigger : MonoBehaviour
{
    private DialogueManager dialogueInst;
    [SerializeField] private GameObject UItobeTriggered;

    private void Start()
    {
        dialogueInst = DialogueManager.GetInstance();
        UItobeTriggered.SetActive(false); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !dialogueInst.dialogueIsPlaying)
        {
            UItobeTriggered.SetActive(true);
        }
    }
}
