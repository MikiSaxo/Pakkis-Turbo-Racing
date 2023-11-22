using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Tools.SingletonClassBase;
using UnityEngine;

public class Manager : Singleton<Manager>
{
    [field:SerializeField] public int CurrentPlayerNumbers { get; set; }
    
     public List<UIPlayer> UIPlayers = new List<UIPlayer>();

    [SerializeField, Header("Parameters")] private Transform[] _startPos;
    [SerializeField] private GameObject _textCooldown;
    [SerializeField] private GameObject _raceCam;

    private bool _canLaunchGame;
    private int _currentPosPlayer;
    private int _currentPlayerReady;
    private float _currentNumber;

    private void Start()
    {
        _currentNumber = 4;
    }

    public Vector3 GetStartPos()
    {
        _currentPosPlayer++;
        CurrentPlayerNumbers++;

        if (_currentPosPlayer >= _startPos.Length)
            return Vector3.zero;

        return _startPos[_currentPosPlayer].position;
    }

    public void CheckIfAllPlayersReady()
    {
        var count = 0;
        foreach (var player in UIPlayers)
        {
            if (player.IsReady)
                count++;
        }
        
        _currentPlayerReady = count;

        if (CurrentPlayerNumbers == _currentPlayerReady)
        {
            print("can launch game");
            _canLaunchGame = true;
            _textCooldown.SetActive(true);
            LaunchNumber();
        }
        else
        {
            if (_canLaunchGame)
                _textCooldown.transform.DOKill();
            
            Reset();
        }
    }

    private void Reset()
    {
        _textCooldown.SetActive(false);
        _currentNumber = 4;
    }

    private void LaunchNumber()
    {
        _currentNumber--;

        if (_currentNumber == 0)
        {
            _canLaunchGame = false;
            Reset();
            _raceCam.SetActive(true);
        }
        
        _textCooldown.transform.DORotate(new Vector3(0,0 ,0), 0);
        _textCooldown.transform.DOScale(Vector3.one, 0);
            
        if(_currentNumber == 1)
            _textCooldown.GetComponent<TMP_Text>().color = new Color(1f, 0.82f, 0.22f);
        _textCooldown.GetComponent<TMP_Text>().text = $"{_currentNumber}";
        
        _textCooldown.transform.DOPunchScale(Vector3.one, .2f).OnComplete(Wait);
    }

    private void Wait()
    {
        _textCooldown.transform.DOMove(_textCooldown.transform.position, .5f).OnComplete(RotateText);
    }

    private void RotateText()
    {
        _textCooldown.transform.DORotate(new Vector3(0,0, -360), .3f, RotateMode.FastBeyond360).SetEase(Ease.OutSine);
        _textCooldown.transform.DOScale(Vector3.zero, .3f).SetEase(Ease.OutSine).OnComplete(LaunchNumber);
    }
    // private void Update()
    // {
    //     if (!_canLaunchGame)
    //     {
    //         _currentCooldown = _cooldown;
    //         return;
    //     }
    //
    //     _currentCooldown -= Time.deltaTime;
    // }
    
}