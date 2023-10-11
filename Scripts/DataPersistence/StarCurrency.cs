using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCurrency : MonoBehaviour, ISaveState
{
    public string PickedUpKey => $"Stars-{gameObject.name}-PickedUp";

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player)) {
            player.AddStars();
            gameObject.SetActive(false);

        }
    }


    public void Save()
    {
        PlayerPrefs.SetInt(PickedUpKey, gameObject.activeSelf ? 0 : 1);

    }

    public void Load()
    {
        int isActive = PlayerPrefs.GetInt(PickedUpKey);
        gameObject.SetActive(isActive == 0);

    }
}
