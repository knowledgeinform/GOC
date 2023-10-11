using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LootRandom : MonoBehaviour
{
    [SerializeField] private GameObject LootArea;
    [SerializeField] private GameObject Loot;
    public static int Gold;
    public Text LootIndicator;

    void Start()
    {
        TransformLoot();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("ship")) {
            Debug.Log("entered OnTriggerEnter");
            TransformLoot();
            Gold +=1;
        }
    }
    private void TransformLoot()
    {
        Loot.transform.position = new Vector3(Random.Range(LootArea.GetComponent<Renderer>().bounds.min.x,LootArea.GetComponent<Renderer>().bounds.max.x),1f,Random.Range(LootArea.GetComponent<Renderer>().bounds.min.z,LootArea.GetComponent<Renderer>().bounds.max.z));
    }
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Z)) // this was just to test persistence, will be deleted when implemented
        {
            SceneManager.LoadScene(3);
        }
        LootIndicator.text = Gold.ToString();
    }
}
