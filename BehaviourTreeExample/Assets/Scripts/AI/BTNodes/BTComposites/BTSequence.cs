using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
/// The Sequence node runs all its children in sequence one after another, only continues if a child return success, if one child fails, it returns failed itself
///
public class BTSequence : BTComposite
{
    private int currentIndex = 0;

    public BTSequence(params BTBaseNode[] children) : base(children) { }

    protected override TaskStatus OnUpdate()
    {
        for( ; currentIndex < children.Length; currentIndex++)
        {
            var result = children[currentIndex].Tick();
            switch (result) 
            {
                case TaskStatus.Success: continue;
                case TaskStatus.Failed: return TaskStatus.Failed;
                case TaskStatus.Running: return TaskStatus.Running;
            }
        }        
        return TaskStatus.Success;
    }

    protected override void OnEnter()
    {
        currentIndex = 0;
    }

    protected override void OnExit()
    {
        currentIndex = 0;
    }

    public override void OnReset()
    {
        currentIndex = 0;
        foreach (var c in children)
        {
            c.OnReset();
        }
    }
}

