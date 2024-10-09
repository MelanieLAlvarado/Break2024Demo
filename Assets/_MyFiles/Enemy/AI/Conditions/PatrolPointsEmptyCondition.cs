using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "PatrolPointsEmpty", story: "[PatrolPoints] is Empty [Condition]", category: "Conditions", id: "e3e551ceceb25597c74c51c07ead9fee")]
public partial class PatrolPointsEmptyCondition : Condition
{
    [SerializeReference] public BlackboardVariable<List<GameObject>> PatrolPoints;
    [SerializeReference] public BlackboardVariable<bool> Condition;

    public override bool IsTrue()
    {
        Debug.Log($"{this.GameObject}'s Patrol count == {PatrolPoints.Value.Count}");
        bool bIsPatrolPointsEmpty = PatrolPoints.Value.Count == 0;
        return bIsPatrolPointsEmpty == Condition.Value;
    }
}
