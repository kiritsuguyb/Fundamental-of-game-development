using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingState : StateBase
{
    public override void Enter(Player player)
    {
        base.Enter(player);
        Debug.Log("Okay, now I'm going to sleep.");
    }
    public override void Exit(Player player)
    {
        base.Exit(player);
        Debug.Log("Okay, now I stop sleeping");
    }
    public override void HandleInput(Player player)
    {
        base.HandleInput(player);
        Debug.Log("sleeping zzz...");
        if (player.someEventTriggered)
        {
            player.someEventTriggered = false;
            if (player.dropdown.value == (int)Event.Seven_Clock)
            {
                Debug.Log("Wow, It's seven o'clock, I guess I have to get up!");
                TurnToState(player, player.GetState<BreakFastState>());
            }
        }
        
    }
}
