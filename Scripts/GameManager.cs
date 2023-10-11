using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateGameState(GameState newState) 
    {
        State = newState;

        SceneManager.LoadScene(newState.ToString(), LoadSceneMode.Additive);
        
        switch (newState)
        {
            case GameState.TownScene:
                break;
            case GameState.LabScene:
                break;
            case GameState.FlightTutorial:
                break;
            case GameState.ExtensionTutorial:
                break;
            case GameState.Play:
                break;
            default: 
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    public enum GameState {
        TownScene,
        LabScene,
        FlightTutorial,
        ExtensionTutorial,
        Play,
    }
}
