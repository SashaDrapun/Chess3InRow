using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue>
{
    [SerializeField]
    private List<TKey> keys = new List<TKey>();

    [SerializeField]
    private List<TValue> values = new List<TValue>();

    public Dictionary<TKey, TValue> ToDictionary()
    {
        var dictionary = new Dictionary<TKey, TValue>();
        for (int i = 0; i < keys.Count; i++)
        {
            dictionary[keys[i]] = values[i];
        }
        return dictionary;
    }

    public void FromDictionary(Dictionary<TKey, TValue> dictionary)
    {
        keys.Clear();
        values.Clear();
        foreach (var keyValuePair in dictionary)
        {
            keys.Add(keyValuePair.Key);
            values.Add(keyValuePair.Value);
        }
    }
}
