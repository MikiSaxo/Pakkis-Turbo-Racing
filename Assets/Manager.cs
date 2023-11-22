using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using DG.Tweening;
using TMPro;
using Tools.SingletonClassBase;
using UnityEngine;

public class Manager : Singleton<Manager>
{
    [field:SerializeField] public int CurrentPlayerNumbers { get; set; }
    public bool IsGameStarted { get; set; }
    
     public List<UIPlayer> UIPlayers = new List<UIPlayer>();
     public List<CharacterManager> Players = new List<CharacterManager>();

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
        CheckIfAllPlayersReady();

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
            
            foreach (var player in Players)
            {
                player.CanPaddle = false;
            }
            
            LaunchNumber();
        }
        else
        {
            if (_canLaunchGame)
                _textCooldown.transform.DOKill();
            
            foreach (var player in Players)
            {
                player.CanPaddle = true;
            }
            
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
            LaunchGame();
            _textCooldown.GetComponent<TMP_Text>().color = Color.white;
            _textCooldown.transform.DOKill();
            return;
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

    private void LaunchGame()
    {
        print("launch game");
        foreach (var uiPlayer in UIPlayers)
        {
            uiPlayer.GoGoGo();
        }

        foreach (var player in Players)
        {
            player.CanGo = true;
            player.CanPaddle = true;

            player.KayakControllerProperty.CanGo = true;
            player.KayakControllerProperty.Rb.velocity = Vector3.zero;

            player.ResetPos();
        }
        _canLaunchGame = false;
        IsGameStarted = true;
        Reset();
        _raceCam.SetActive(true);
    }
}