using System;
using Unity.Behavior;
using UnityEngine;
using Modifier = Unity.Behavior.Modifier;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ActionCooldown", story: "Coolown After [CooldownDuration] Seconds", category: "Flow", id: "221c58ea87af824a14d96a13657b6fc3")]
public partial class ActionCooldownModifier : Modifier
{
    [SerializeReference] public BlackboardVariable<float> CooldownDuration;

    private float _lastExecutionTime= -1;
    
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

