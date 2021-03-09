using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// [ENG]
/// NarrativeController is used to control the loading values from Act object
/// as call it, listed on _actValues. Also is possible to get the talk values
/// (object with the talk infos).
///
/// [PTBR]
/// NarrativeController é utilizado no controle the carregamento dos
/// valores do Ato listado no _actValues. Também é possível retornar o
/// valor de Talk (objeto que contém valores de conversa)
/// </summary>
public class NarrativeManager : Singleton<MonoBehaviour>
{
    [SerializeField]
    private Act[] _actValues;

    [SerializeField, Tooltip("Act's index to play")]
    private uint actIndexToPlay = 0;

    //Current active act on scene
    private Act currentAct;

    //Current talk
    private Talk[] currentTalk;

    //Current talk index
    private uint talkIndex = 0;

    //Load Act to be active on the scene (if isn't)
    public void LoadAct(int _actIndex = 0)
    {
        Act _act = _actValues[_actIndex];
        if (_act)
        {
            LoadAct(_act);
        }
    }

    //Load Act to be active on the scene (if isn't)
    public void LoadAct(Act _act)
    {
        if (_act)
        {
            currentAct = _act;
            this.currentAct.OnCompleteLoad += SetCurrentTalkValues;
            //load the talk values than assigned that to the currentAct field
            StartCoroutine(currentAct.QueueActValues(null));
        }
    }

    //Clean current act
    private void DisposeAct()
    {
        currentAct = null;
        currentTalk = null;
        talkIndex = 0;
    }

    //Return the value if it's not null (Debug)
    [ContextMenu("Debug Act Values")]
    private void SetCurrentTalkValues()
    {
        if (currentAct)
        {
            var _receivedValues = currentAct.DequeueAsset();
            currentTalk = _receivedValues.Talk;
        }
        else
        {
            Debug.Log("Act Null. Load the act before set try to set the talk.");
        }
    }

    //Current talk values to be charged and disposable to be view.
    public Talk NextTalk()
    {
        Talk _nextTalk = null;

        if (talkIndex < currentTalk.Length)
        {
            _nextTalk = currentTalk[talkIndex];
        }
        talkIndex++;
        return _nextTalk;
    }

    //Get all the talk values from currentAct
    public Talk[] GetCompleteTalkOnAct
    {
        get { return currentTalk; }
    }

    //Clean object from memory when destroyed.
    private void OnDestroy()
    {
        currentAct = null;
        currentTalk = null;
        _actValues = null;

        talkIndex = 0;

        //preserve the object
        DontDestroyOnLoad(this);
    }
}
