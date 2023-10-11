using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarUIDisplay : MonoBehaviour
{
    private Player _player;
    private TMP_Text _text;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _text = GetComponent<TMP_Text>();
    }
    
    void Update()
    {
        _text.SetText(_player.Stars + " Stars");
        
    }
}
