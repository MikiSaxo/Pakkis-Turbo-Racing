using System;
using System.Collections;
using System.Collections.Generic;
using Tools.SingletonClassBase;
using UnityEngine;

public class Manager : Singleton<Manager>
{
    public int CurrentPlayerNumbers { get; set; }

    [SerializeField, Header("Parameters")]
    private Transform[] _startPos;


    private int _currentPosPlayer;

    public Vector3 GetStartPos()
    {
        _currentPosPlayer++;
        CurrentPlayerNumbers++;

        if (_currentPosPlayer >= _startPos.Length)
            return Vector3.zero;
        
        return _startPos[_currentPosPlayer].position;
    }
}
