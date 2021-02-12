using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NarrativeController))]
public class PlayNarrative : Singleton<MonoBehaviour>
{
    private static NarrativeController narrative_Controller;

    public static PlayNarrativeStatus narrativeStatus = PlayNarrativeStatus.AVAILABLE;


    private void Start()
    {
        narrative_Controller = GetComponent<NarrativeController>();
        if (narrative_Controller)
            Debug.Log("Narrative found");
    }

    private void OnLoadNarrative() { }

    private void OnPlayNarrative() { }

    private void OnStopNarrative() { }

    private void OnErrorToLoad() { }




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