using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// O controlador de narrativa que lista os atos presentes no projeto
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

    private uint talkIndex = 0;

    private void Awake()
    {
        //if (Instance) { Destroy(this.gameObject);}
        //Instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        LoadAct(_actValues[actIndexToPlay]);
    }

    //Load Act to be active on the scene (if isn't)
    public void LoadAct(Act _toLoad)
    {
        if (_toLoad)
        {
            //load the talk values than assigned that to the currentAct field
            StartCoroutine(_toLoad.QueueActValues(
                            () => {
                                this.currentAct = _toLoad;
                                ChargeCurrentTalkValues();
                                }));
        }
    }

    //Return the value if it's not null
    public void ChargeCurrentTalkValues()
    {
        if (currentAct)
        {
            var _receivedValues = currentAct.DequeueAsset();
            currentTalk = _receivedValues.Talk;
            Debug.Log("Talk by - " + currentTalk[0].Speaker);
        }
        else
        {
            Debug.Log("Act Null");
        }
    }

    //Current talk values to be charged and disposable to be view.
    public void TalkProgress()
    {
        
    }

    public bool NextSpeak()
    {
        talkIndex++;
        if (talkIndex >= currentTalk.Length)
            return false;
        return true;
    }

    //Get all the act's collection registered on +actValues
    public Act[] GetActCollection
    {
        get { return _actValues; }
    }
}
