using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// [PT-BR]
/// O ato simboliza uma organização de diálogos(classe JsonValues) que é dividido
/// de forma linear.
///
/// [ENGLISH]
/// The act object represents a dialogues organization (using the JsonValues) in a
/// linear path.
/// </summary>
[CreateAssetMenu(menuName ="Act Object")]
public class Act : ScriptableObject, IAct
{
    ///a action that is called when the QueueActValues async method finish.
    public event Action OnCompleteLoad;

    //json data talk values
    [SerializeField]
    private TextAsset[] jsonAssets;

    //Queue values to act narrative control without loss values.
    private Queue<JsonValues> queuedAssets = new Queue<JsonValues>();

    //Get current value 
    public JsonValues DequeueAsset()
    {
        return queuedAssets.Dequeue();
    }

    //load json assets and return the object
    private JsonValues LoadTalkAsset(TextAsset _data)
    {
        return JsonValues.CreateFromJSON(_data.text);
    }

    //Serialize the datas from json values and add to the assets queue object
    public IEnumerator QueueActValues(Action _onCompleteActLoad)
    {
        uint indexAsset = 0;
        while (indexAsset <= (jsonAssets.Length -1))
        {
            var _val = LoadTalkAsset(jsonAssets[indexAsset]);
            AddAssetsOnQueue(_val);
            indexAsset++;
            yield return null;
        }
        _onCompleteActLoad();
    }

    //Add Assets on queued object to guarantee the assets integrity
    public void AddAssetsOnQueue(JsonValues _values)
    {
        queuedAssets.Enqueue(_values);
    }

    //Return the queue's size
    public int QueueLength
    {
        get { return queuedAssets.Count; }
    }
}

//act interface
public interface IAct
{
    JsonValues DequeueAsset();

    IEnumerator QueueActValues(Action _onCompleteActLoad);

    void AddAssetsOnQueue(JsonValues _values);
}

