using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StateBase : MonoBehaviour
{
    virtual public void HandleInput(Player player) { }
    virtual public void Enter(Player player) { }
    virtual public void Exit(Player player) {
        player.state = player.GetComponent<StateBase>();
    }
    protected void TurnToState(Player player,StateBase state) {
        player.state.Exit(player);
        player.state = state;
        player.state.Enter(player);
    }
}
