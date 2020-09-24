using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakFastState : StateBase
{
    public override void Enter(Player player)
    {
        base.Enter(player);
        Debug.Log("Okay, now I'm going to have my breakfast.");
    }
    public override void Exit(Player player)
    {
        base.Exit(player);
        Debug.Log("Okay, now I finished eating");
    }
    public override void HandleInput(Player player)
    {
        base.HandleInput(player);
        Debug.Log("eating...");
        if (player.someEventTriggered)
        {
            player.someEventTriggered = false;
            if (player.dropdown.value == (int)Event.BreakFast_Finish)
            {
                Debug.Log("Almost done with breakfast! Time for attending class!");
                TurnToState(player, player.GetState<TakingClassState>());
            }
        }

    }
}
