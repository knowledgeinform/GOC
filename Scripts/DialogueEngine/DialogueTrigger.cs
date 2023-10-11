using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // private PlayerController playerInst;

    [SerializeField] private GameObject visualCue;
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private bool isSystemText = false;
    private DialogueManager dialogueInst;
    private bool playerInRange;

    private void Start()
    {
        playerInRange = false;
        visualCue.SetActive(false);
        dialogueInst = DialogueManager.GetInstance();
        // playerInst = PlayerController.GetInstance();
    }

    private void Update()
    {
        // only shows new dialogue if player is in range AND all other dialogue boxes are finished
        if (playerInRange && !dialogueInst.dialogueIsPlaying)
        {
            FaceTarget();

            visualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log(isSystemText);
                 
                if (isSystemText)
                {
                    dialogueInst.isSystemText = true;
                }

                dialogueInst.StartDialogue(inkJSON);
            }
        }
        else
        {
            visualCue.SetActive(false);
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

    private void FaceTarget()
	{
        Transform parent = this.transform.parent.transform;
		Vector3 direction = (GameObject.FindWithTag("Player").transform.position - parent.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		parent.rotation = Quaternion.Slerp(parent.rotation, lookRotation, Time.deltaTime * 5f);
	}
}
