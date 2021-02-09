using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeController : MonoBehaviour
{

    [SerializeField]
    private Act[] _actValues;

    private uint currentActIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(_actValues != null)
        {
            _actValues[currentActIndex].Test();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
