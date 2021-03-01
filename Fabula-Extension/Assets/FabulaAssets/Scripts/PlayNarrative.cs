using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// [ENG]
/// PlayNarrative is related to control the narrative actions
/// 
/// [PTBR]
/// 
/// </summary>
public class PlayNarrative : Singleton<MonoBehaviour>
{
    //narrative instance on gameobject related to the narrative object
    private NarrativeController narrative_Controller;

    //Current PlayNarrative status
    public static NarrativeStatus narrativeStatus = NarrativeStatus.AVAILABLE;

    //Input Reader component
    private InputReader inputReader;

    private void Start()
    {
        SetNarrativeController();
        inputReader = GetComponent<InputReader>();
        if (inputReader == null)
            Debug.LogError("INPUT READER NOT FOUND ON OBJECT");
    }

    public void SetNarrativeController()
    {
        narrative_Controller = GetComponent<NarrativeController>();
        if (narrative_Controller)
        {
            narrativeStatus = NarrativeStatus.AVAILABLE;
            Debug.Log("Narrative found");
            //OnLoadNarrative();
        }
    }

    //Called to load
    public virtual void OnLoadNarrative(int _actIndex = 0)
    {
        if (narrative_Controller)
        {
            narrative_Controller.LoadAct(_actIndex);
            narrativeStatus = NarrativeStatus.LOADED;
        }
        else
        {
            narrativeStatus = NarrativeStatus.ERROR_ON_LOAD;
        }
    }

    //Call it to play the narrative by input
    public virtual void OnPlayNarrative()
    {
        if (narrative_Controller)
        {
            narrativeStatus = NarrativeStatus.PLAYING;
            var talkInfo = narrative_Controller.NextTalk();
            if (talkInfo != null)
                Debug.Log("<color=yellow>Talk - " + talkInfo.Speaker + " @ " + talkInfo.Speak + "</color>");
            else
            {
                narrativeStatus = NarrativeStatus.STOP;
                Debug.Log("<color=red>Talk - END CURRENT TALK</color>");
            }
        }
    }

    //Call it to stop narrative
    public virtual void OnStopNarrative()
    {
        if (narrative_Controller)
        {
            narrativeStatus = NarrativeStatus.STOP;
        }
    }

    //Call to when has some error
    public virtual void OnErrorToLoad()
    {

        if (narrative_Controller)
        {
            narrativeStatus = NarrativeStatus.ERROR_ON_LOAD;
        }
    }

    private void Update()
    {
        TickInput();
    }

    //Input Monitor every update
    public virtual void TickInput()
    {
        if (inputReader != null)
        {
            if (inputReader.EnableAct()) {
                Debug.Log("ACT ENABLED");
            }

            if (inputReader.LoadNextAct())
            {
                Debug.Log("LOAD ACT");
                OnLoadNarrative();
            }

            if (inputReader.JumpToNextAct())
            {
                Debug.Log("JUMP TO NEXT ACT");
            }

            if (inputReader.NextTalk())
            {
                Debug.Log("NEXT TALK ON ACT");
                OnPlayNarrative();
            }
        }
    }
}

//Status related to the narrative disposable to show.
public enum NarrativeStatus
{
    AVAILABLE = 0,
    LOADED = 1,
    PLAYING = 2,
    STOP = 3,
    ERROR_ON_LOAD = 4
}

