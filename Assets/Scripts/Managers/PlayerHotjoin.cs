using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class PlayerHotjoin : MonoBehaviour
{

    public GameObject[] Players;
    public GameObject[] PlayersUI;

    void Start()
    {
        foreach (var player in Players)
        {
            player.SetActive(false);
        }
        foreach (var player in PlayersUI)
        {
            player.SetActive(false);
        }
    }

    void Update()
    {
        for (int i = 0; i < Players.Length; ++i)
        {
            if(!InputManager.GetPlayerButtonDown((InputManager.Player)i,InputManager.Buttons.Start))
                continue;

            Players[i].SetActive(true);
            PlayersUI[i].SetActive(true);
        }
    }

}
