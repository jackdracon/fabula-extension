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
    public static PlayNarrativeStatus narrativeStatus = PlayNarrativeStatus.AVAILABLE;

    private void Start()
    {
        SetNarrativeController();
    }

    public void SetNarrativeController()
    {
        narrative_Controller = GetComponent<NarrativeController>();
        if (narrative_Controller)
        {
            narrativeStatus = PlayNarrativeStatus.AVAILABLE;
            Debug.Log("Narrative found");
            OnLoadNarrative();
        }
    }


    public virtual void OnLoadNarrative()
    {
        if (narrative_Controller)
        {
            narrative_Controller.LoadAct();
            narrativeStatus = PlayNarrativeStatus.LOADED;
        }
        else
        {
            narrativeStatus = PlayNarrativeStatus.ERROR_ON_LOAD;
        }
    }

    //Call it to play the narrative by input
    public virtual void OnPlayNarrative()
    {
        if (narrative_Controller)
        {
            var talkInfo = narrative_Controller.NextTalk();
            Debug.Log("<color=yellow>Talk - " + talkInfo.Speaker + " @ " + talkInfo.Speak + "</color>");
        }
    }

    public virtual void OnStopNarrative() { }

    public virtual void OnErrorToLoad() { }
}

//Status related to the narrative disposable to show.
public enum PlayNarrativeStatus
{
    AVAILABLE = 0,
    LOADED = 1,
    READY_TO_PLAY = 2,
    STOP = 3,
    ERROR_ON_LOAD = 4
}

//A Singleton design pattern to be prevent a duplicates
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<T>();
                if(_instance == null)
                {
                    _instance = new GameObject("Instance of " + typeof(T)).AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
            Destroy(this.gameObject); //prevent duplicates
    }
}