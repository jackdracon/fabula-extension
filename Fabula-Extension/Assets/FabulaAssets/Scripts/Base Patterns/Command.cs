using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Command base class that will be related to control the narrative
/// by input. 
/// </summary>
public abstract class Command
{
    protected PlayNarrative narrative;

    public Command(PlayNarrative _narrative)
    {
        narrative = _narrative;
    }

    public abstract void Execute();

}
