using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class PlayerHotjoin : MonoBehaviour
{

    public GameObject[] Players;
    public GameObject[] PlayersUI;
    public GameObject[] PlayersJoinUI;

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
        foreach (var player in PlayersJoinUI)
        {
            player.SetActive(true);
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
            PlayersJoinUI[i].SetActive(false);
        }
    }

}
