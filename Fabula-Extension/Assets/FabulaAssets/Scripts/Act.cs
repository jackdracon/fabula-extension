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

    //json data talk values
    [SerializeField]
    private TextAsset[] jsonAssets;

    //Get current value 
    public Talk GetTalk(Act _act)
    {
        throw new System.NotImplementedException();
    }

    private JsonValues LoadTalkAsset(TextAsset _data)
    {
        return JsonValues.CreateFromJSON(_data.text);
    }

    //only test the json received values
    public void Test()
    {
         var _val = LoadTalkAsset(jsonAssets[0]);
        Debug.Log("Asset - " +_val.Talk[1].Speaker);
    }
}

//act interface
public interface IAct
{

    Talk GetTalk(Act _act);
}

