using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Attack", story: "[self] attack [target]", category: "Action", id: "523c955e7b5d0773a7513938b218d2eb")]
public partial class AttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    protected override Status OnStart()
    {
        IBehaviorInterface selfInterface = Self.Value.GetComponent<IBehaviorInterface>();
        if (selfInterface != null)
        { 
            selfInterface.Attack(Target.Value);
        }
        return Status.Success;
    }
    
}

