using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private string objectId;
    DontDestroy[] _objs;

    private void Awake()
    {
        objectId = name + transform.position.ToString() + transform.eulerAngles.ToString();
        _objs = Object.FindObjectsOfType<DontDestroy>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < _objs.Length; i++)
        {
            if (_objs[i] != this && _objs[i].objectId == this.objectId)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
