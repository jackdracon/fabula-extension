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
public class NarrativeController : Singleton<MonoBehaviour>
{
    [SerializeField]
    private Act[] _actValues;

    [SerializeField, Tooltip("Index from act to play")]
    private uint actIndexToPlay = 0;

    //private static NarrativeController instance;

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
            this.currentAct = _act;
            //load the talk values than assigned that to the currentAct field
            StartCoroutine(currentAct.QueueActValues(
                            () => {
                                SetCurrentTalkValues();
                                Debug.Log(" AMOUNT " + currentAct.QueueLength);
                            }));
        }
    }

    //Load Act to be active on the scene (if isn't)
    public void LoadAct(Act _act)
    {
        if (_act)
        {
            currentAct = _act;
            //load the talk values than assigned that to the currentAct field
            StartCoroutine(currentAct.QueueActValues(
                            () => {
                                SetCurrentTalkValues();
                                Debug.Log(" AMOUNT " + currentAct.QueueLength);
                            }));
        }
    }

    //Return the value if it's not null (Debug)
    [ContextMenu("Debug Act Values")]
    private void SetCurrentTalkValues()
    {
        Debug.Log(" TALK AMOUNT " + currentAct.QueueLength);
        if (currentAct)
        {
            var _receivedValues = currentAct.DequeueAsset();
            currentTalk = _receivedValues.Talk;
            //Debug.Log("Talk by - " + currentTalk[0].Speaker);
        }
        else
        {
            Debug.Log("Act Null");
        }
        Debug.Log("After get Talk - " + currentAct.QueueLength);
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
