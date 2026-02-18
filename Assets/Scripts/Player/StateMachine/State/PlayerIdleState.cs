using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        //Debug.Log("Entered Idle State");
    }

    public override void ExitState(PlayerStateManager player)
    {

    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
        player.controller.Move();
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.controller.horizontalInput != 0f)
        {
            player.SwitchState(player.moveState);
        }

        if (player.controller.wantsJump)
        {
            player.SwitchState(player.jumpState);
        }
    }
}
