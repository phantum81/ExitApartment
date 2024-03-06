using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableInputBinding
{
    public BindPair[] bindPairs;

    public SerializableInputBinding(InputBinding _binding)
    {
        int len = _binding.BindingDic.Count;
        int idx = 0;

        bindPairs = new BindPair[len];

        foreach(var pair in _binding.BindingDic)
        {
            bindPairs[idx++] = new BindPair(pair.Key, pair.Value);
        }
    }
}

[Serializable]
public class BindPair
{
    public EuserAction key;
    public KeyCode value;

    public BindPair(EuserAction _key, KeyCode _value)
    {
        key = _key;
        value = _value;
    }
}
