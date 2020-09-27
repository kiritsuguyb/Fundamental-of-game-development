using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckingTime : StateBase
{
    public override void Enter(Player player)
    {
        base.Enter(player);
        Debug.Log("Let's check the time.");
    }
    public override void Exit(Player player)
    {
        base.Exit(player);
    }
    public override void HandleInput(Player player)
    {
        base.HandleInput(player);
        Debug.Log("checking...");
        if (player.someEventTriggered)
        {
            player.someEventTriggered = false;
            if (player.dropdown.value == (int)Event.Too_Late)
            {
                Debug.Log("It's too late.");
                TurnToState(player, player.GetState<TakingLastClassState>());
            }
            if (player.dropdown.value == (int)Event.Still_Early)
            {
                Debug.Log("It's still early for now.");
                TurnToState(player, player.GetState<PlayingGame>());
            }
        }

    }
}