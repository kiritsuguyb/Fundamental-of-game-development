using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunchState : StateBase
{
    public override void Enter(Player player)
    {
        base.Enter(player);
        Debug.Log("Okay, now I'm going have lunch.");
    }
    public override void Exit(Player player)
    {
        base.Exit(player);
        Debug.Log("Now I'm stuffed.");
    }
    public override void HandleInput(Player player)
    {
        base.HandleInput(player);
        Debug.Log("eating...");
        if (player.someEventTriggered)
        {
            player.someEventTriggered = false;
            if (player.dropdown.value == (int)Event.Lunch_Finish)
            {
                Debug.Log("I'm almost done with lunch.");
                TurnToState(player, player.GetState<CheckingTime>());
            }
        }

    }
}