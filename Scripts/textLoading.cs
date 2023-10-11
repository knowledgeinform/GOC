using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textLoading : MonoBehaviour
{

    [SerializeField] private Text loading;
    private string loadingText;

    // Start is called before the first frame update
    void Awake()
    {
        loadingText = loading.text;
        Debug.Log(loadingText);
        
    }
    void Start() 
    {
        TypeSentence(loadingText);
    }

    void Update()
    {
    }
    void TypeSentence(string sentence)
    {
        loading.text = "";
        GameObject loadingScreen = GameObject.Find("LoadingScreen");

        while((loadingScreen.activeSelf)) 
        {
            foreach (char c in sentence.ToCharArray()) 
            {
                loading.text += c;
                new WaitForSeconds(.4f);
            }
            loading.text = "";
        }
        
    }
}

