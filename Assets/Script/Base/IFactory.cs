using Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFactory
{
    T CreateObject<T>() where T : MyObject;
    T CreateObject<T>(Transform parent) where T : MyObject;

    T CreateObject<T>(Vector2 pos) where T : MyObject;
}
