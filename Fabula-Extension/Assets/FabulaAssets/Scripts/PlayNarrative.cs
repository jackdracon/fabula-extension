﻿using System.Collections;
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

    //Called to load
    public virtual void OnLoadNarrative(int _actIndex = 0)
    {
        if (narrative_Controller)
        {
            narrative_Controller.LoadAct(_actIndex);
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
            narrativeStatus = PlayNarrativeStatus.PLAYING;
            var talkInfo = narrative_Controller.NextTalk();
            if (talkInfo != null)
                Debug.Log("<color=yellow>Talk - " + talkInfo.Speaker + " @ " + talkInfo.Speak + "</color>");
            else
            {
                narrativeStatus = PlayNarrativeStatus.STOP;
                Debug.Log("<color=red>Talk - END CURRENT TALK</color>");
            }
        }
    }

    //Call it to stop narrative
    public virtual void OnStopNarrative()
    {
        if (narrative_Controller)
        {
            narrativeStatus = PlayNarrativeStatus.STOP;
        }
    }

    //Call to when has some error
    public virtual void OnErrorToLoad()
    {

        if (narrative_Controller)
        {
            narrativeStatus = PlayNarrativeStatus.ERROR_ON_LOAD;
        }
    }

    private void Update()
    {
        if (narrativeStatus == PlayNarrativeStatus.LOADED ||
            narrativeStatus == PlayNarrativeStatus.PLAYING)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                OnPlayNarrative();
            }
        }
    }
}

//Status related to the narrative disposable to show.
public enum PlayNarrativeStatus
{
    AVAILABLE = 0,
    LOADED = 1,
    PLAYING = 2,
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