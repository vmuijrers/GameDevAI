using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTWait : BTBaseNode
{
    protected float maxWaitTime = 0;
    protected float currentWaitTime = 0;
    
    public BTWait(float seconds)
    {
        maxWaitTime = seconds;
    }

    protected override void OnEnter()
    {
        currentWaitTime = maxWaitTime;
    }

    protected override TaskStatus OnUpdate()
    {
        if(currentWaitTime > 0)
        {
            currentWaitTime -= Time.deltaTime;
            if(currentWaitTime > 0) { return TaskStatus.Running; }
        }
        return TaskStatus.Success;
    }
}
