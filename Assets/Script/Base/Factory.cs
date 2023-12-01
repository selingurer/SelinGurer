using Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class Factory : IFactory
{
    List<MyObject> _prefabs = new();
    public static IFactory Instance { get; private set; } = new Factory();

    private Factory()
    {
        MyObject[] allPrefabs = Resources.LoadAll<MyObject>("");
        foreach (MyObject obj in allPrefabs)
        {
            if (!_prefabs.Contains(obj))
                _prefabs.Add(obj);
        }
    }
    public T CreateObject<T>() where T : MyObject
    {
        return CreateObject<T>(null);
    }
    public T CreateObject<T>(Transform parent) where T : MyObject
    {
        Type type = typeof(T);
        MyObject? res = _prefabs.Find(x => x.GetType() == type);
        if (res == null)
        {
            Debug.LogError("Object not found: " + type.FullName);
            return default;
        }

        return (T)Object.Instantiate(res, parent);
    }
    public T CreateObject<T>(Vector2 pos) where T : MyObject
    {
        Type type = typeof(T);
        MyObject? res = _prefabs.Find(x => x.GetType() == type);


        return (T)Object.Instantiate(res, pos, Quaternion.identity);
    }
}
