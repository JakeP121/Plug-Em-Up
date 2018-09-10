using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Tuple<TKey1, TKey2>
{
    public TKey1 key1 { get; private set; }
    public TKey2 key2 { get; private set; }

    public Tuple(TKey1 key1, TKey2 key2)
    {
        this.key1 = key1;
        this.key2 = key2;
    }
}

public struct Tuple<TKey1, TKey2, TKey3>
{
    public TKey1 key1 { get; private set; }
    public TKey2 key2 { get; private set; }
    public TKey3 key3 { get; private set; }

    public Tuple(TKey1 key1, TKey2 key2, TKey3 key3)
    {
        this.key1 = key1;
        this.key2 = key2;
        this.key3 = key3;
    }
}