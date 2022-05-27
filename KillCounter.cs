using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class KillCounter : MonoBehaviour
{
    public static Action OnTrigger_Open;
    public int killRemaining;
    public int maxKill;

    private TextMeshProUGUI killCount;

    private void Awake()
    {
        killCount = GameObject.Find("KillCount").GetComponent<TextMeshProUGUI>();
        maxKill = killRemaining;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {
            killRemaining -= 1;
        }
        if(killRemaining <= 0)
        {
            OnTrigger_Open?.Invoke();
        }

        killCount.text = $"Kill Count: {killRemaining}/{maxKill} ";
    }
}
