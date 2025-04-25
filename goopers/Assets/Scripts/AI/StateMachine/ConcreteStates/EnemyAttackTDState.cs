using System.Collections;
using UnityEngine;

public class EnemyAttackTDState : EnemyState
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public EnemyAttackTDState(Target enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        enemy.damageTDOn = true;
    }
    
    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AnimationTriggerEvent(Target.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }
    
}
