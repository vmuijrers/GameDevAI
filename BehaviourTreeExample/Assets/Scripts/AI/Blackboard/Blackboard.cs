using System.Collections.Generic;
using UnityEngine;

public class Blackboard
{
    private Dictionary<string, object> dictionary = new Dictionary<string, object>();

    public T GetVariable<T>(string name)
    {
        if (dictionary.ContainsKey(name))
        {
            return (T)dictionary[name];
        }
        return default(T);
    }

    public void SetVariable<T>(string name, T variable)
    {
        if (dictionary.ContainsKey(name))
        {
            dictionary[name] = variable;
        }
        else
        {
            dictionary.Add(name, variable);
        }
    }
}
