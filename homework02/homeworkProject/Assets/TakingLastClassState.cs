using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakingLastClassState : StateBase
{
    public override void Enter(Player player)
    {
        base.Enter(player);
        Debug.Log("The last class today!!");
    }
    public override void Exit(Player player)
    {
        base.Exit(player);
    }
    public override void HandleInput(Player player)
    {
        base.HandleInput(player);
        Debug.Log("listening...");
        if (player.someEventTriggered)
        {
            player.someEventTriggered = false;
            Debug.Log("A day is over. Quit to start again.");
            Exit(player);
        }

    }
}
