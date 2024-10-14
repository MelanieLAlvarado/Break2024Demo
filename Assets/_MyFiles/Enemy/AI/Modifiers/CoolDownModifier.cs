using System;
using Unity.Behavior;
using UnityEngine;
using Modifier = Unity.Behavior.Modifier;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CoolDown", story: "Cooldown After [CooldownDuration] Seconds", category: "Flow", id: "f62d3f1c11496190632e91e79dd02816")]
public partial class CoolDownModifier : Modifier
{
    [SerializeReference] public BlackboardVariable<float> CooldownDuration;
    private float _lastExecutionTime = -1;

    protected override Status OnStart()
    {
        float currentTime = Time.timeSinceLevelLoad;

        if (_lastExecutionTime < 0 || currentTime - _lastExecutionTime > CooldownDuration.Value)
        { 
            _lastExecutionTime = currentTime;
            return StartNode(Child);
        }
        return Status.Success;
    }

    protected override Status OnUpdate()
    {
        return Child.CurrentStatus;
    }
}

