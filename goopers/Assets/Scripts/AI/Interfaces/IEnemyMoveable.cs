using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public interface IEnemyMoveable
{
    Rigidbody RB { get; set; }
    bool IsFacingRight { get; set; }
    void MoveEnemy(float state);
    // walk is 0, attackTD is 1, attackPlayer is 2
    void CheckForLeftOrRightFacing(Vector2 velocity);
}
