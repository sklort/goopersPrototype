using Unity.VisualScripting;
using UnityEngine;

public class EnemyWalkState : EnemyState
{
    //For lower TD point
    private GameObject lowerTDPoint;
    
    private Vector3 _targetPos;
    private Vector3 _direction;
    
   
    public EnemyWalkState(Target enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        lowerTDPoint = GameObject.Find("TDPoint1");
        _targetPos = lowerTDPoint.transform.position;
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        
        enemy.MoveEnemy(0f);

        if ((enemy.transform.position - _targetPos).sqrMagnitude <= 2.0f)
        {
            enemy.StateMachine.ChangeState(enemy.AttackTDState);
        }
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
