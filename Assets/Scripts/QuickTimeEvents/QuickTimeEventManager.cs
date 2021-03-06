﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuickTimeEventManager : MonoBehaviour
{

    private static QuickTimeEvent[] eventsObjects;
    void Start()
    {
        eventsObjects = GetComponentsInChildren<QuickTimeEvent>(true);
        eventsObjects = eventsObjects.OrderBy(qte => qte.Player).ToArray();
    }

    public static void StartQuickTimeEventForPlayer(int player, Vector3 position, Action<int> successAction, Action<int> failAction)
    {
        eventsObjects[player].StartQuickTimeEvent(position,successAction,failAction);
    }

    public static void StopQuickTimeEventForPlayer(int player)
    {
        eventsObjects[player].StopQuickTimeEvent();
    }

}
