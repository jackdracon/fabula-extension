using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Input receive from user.
/// </summary>
public class InputReader : MonoBehaviour
{
    [SerializeField, Tooltip("Go to the next talk on act")]
    private KeyCode goToNextTalk = KeyCode.E;

    [SerializeField, Tooltip("Load Next Act")]
    private KeyCode nextAct = KeyCode.Space;

    [SerializeField, Tooltip("Load the next act")]
    private KeyCode loadAct = KeyCode.L;

    [SerializeField, Tooltip("enable the narrative object")]
    private KeyCode enableNarrative = KeyCode.A;

    //Get the read status 
    public bool NextTalk()
    {
        if (Input.GetKeyUp(goToNextTalk))
            return true;
        return false;
    }

    //Get the act status
    public bool JumpToNextAct()
    {
        if (Input.GetKeyUp(nextAct))
            return true;
        return false;
    }

    //get the load of next act
    public bool LoadNextAct()
    {
        if (Input.GetKeyUp(loadAct))
            return true;
        return false;
    }

    //Enable Act to play narrative
    public bool EnableAct()
    {
        if (Input.GetKeyUp(enableNarrative))
            return true;
        return false;
    }
}
