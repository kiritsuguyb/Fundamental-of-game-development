using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakingClassState : StateBase
{
    public override void Enter(Player player)
    {
        base.Enter(player);
        Debug.Log("Okay, now I'm going to attend my class.");
    }
    public override void Exit(Player player)
    {
        base.Exit(player);
        Debug.Log("Yeah! The class is finally over.");
    }
    public override void HandleInput(Player player)
    {
        base.HandleInput(player);
        Debug.Log("listening carefully....");
        if (player.someEventTriggered)
        {
            player.someEventTriggered = false;
            if (player.dropdown.value == (int)Event.Class_Finish)
            {
                Debug.Log("I think the class is going to be over.");
                TurnToState(player, player.GetState<LunchState>());
            }
        }

    }
}