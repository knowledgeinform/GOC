using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeShipColor : MonoBehaviour
{
    [SerializeField] private Material[] _mats;
    TextMeshPro colorText;
    private Renderer _rend0;
    private Renderer _rend1;
    private Renderer _rend2;

    private void Awake()
    {
        _rend0 = transform.GetChild(0).gameObject.GetComponent<Renderer>();
        _rend1 = transform.GetChild(1).gameObject.GetComponent<Renderer>();
        _rend2 = transform.GetChild(2).gameObject.GetComponent<Renderer>();
        colorText = GameObject.Find("ShipColorChange").GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        if (colorText == null)
        {
            Debug.LogError("Ship's color text is invalid: " + colorText.text);
        }
        else
        {
            switch (colorText.text)
            {
                case "black":
                    _rend0.material = _mats[0];
                    _rend1.material = _mats[0];
                    _rend2.material = _mats[0];
                    break;
                
                case "white":
                    _rend0.material = _mats[1];
                    _rend1.material = _mats[1];
                    _rend2.material = _mats[1];
                    break;
            }
        }
    }
}
