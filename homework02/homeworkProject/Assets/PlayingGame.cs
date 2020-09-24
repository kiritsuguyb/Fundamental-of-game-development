using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingGame : StateBase
{
    public override void Enter(Player player)
    {
        base.Enter(player);
        Debug.Log("Let's play some game.");
    }
    public override void Exit(Player player)
    {
        base.Exit(player);
        Debug.Log("I stop playing game.");
    }
    public override void HandleInput(Player player)
    {
        base.HandleInput(player);
        Debug.Log("playing games...");
        if (player.someEventTriggered)
        {
            player.someEventTriggered = false;
            if (player.dropdown.value == (int)Event.Too_Late)
            {
                Debug.Log("It's too late.");
                TurnToState(player, player.GetState<TakingLastClassState>());
            }
        }

    }
}