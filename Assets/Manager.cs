using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using DG.Tweening;
using TMPro;
using Tools.SingletonClassBase;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : Singleton<Manager>
{
    [field: SerializeField] public int CurrentPlayerNumbers { get; set; }
    
    [field: SerializeField] public bool IsTesting { get; set; }

    public bool IsGameStarted { get; set; }
    public bool IsGameEnded { get; set; }

    [HideInInspector] public List<UIPlayer> UIPlayers = new List<UIPlayer>();
    [HideInInspector] public List<CharacterManager> Players = new List<CharacterManager>();

    [SerializeField, Header("Parameters")] private Transform[] _startPos;
    [SerializeField] private GameObject _textCooldown;
    [SerializeField] private GameObject _raceCam;

    [Header("UI")] [SerializeField] private GameObject _loadingsScreen;
    [SerializeField] private Image _loadingsStep;
    [SerializeField] private Image _loadingsBG;
    [SerializeField] private Sprite[] _loadingsStepSprite;
    [SerializeField] private float _timeBetweenTwoScreen = 2f;
    [SerializeField] private Image _bg;
    [SerializeField] private TMP_Text _startText;
    [SerializeField] private float _timeFadeStart = 2f;
    [SerializeField] private GameObject _startPanel;

    private bool _canLaunchGame;
    private int _currentPosPlayer;
    private int _currentPlayerReady;
    private float _currentNumber;
    private int _currentLoadingStep;

    private void Start()
    {
        _currentNumber = 4;

        if (IsTesting)
            StartFaster();
        
        FadeInTextStart();
    }

    public Vector3 GetStartPos()
    {
        _currentPosPlayer++;
        CurrentPlayerNumbers++;
        CheckIfAllPlayersReady();

        _startPanel.SetActive(false);
        _startText.DOKill();
        _startText.DOFade(0, 0);
        _bg.DOFade(0, .5f);

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
            _canLaunchGame = true;

            foreach (var player in Players)
            {
                player.CanPaddle = false;
            }

            StartCoroutine(WaitLaunchLoadingScreen());
        }
        // else
        // {
        //     if (_canLaunchGame)
        //         _textCooldown.transform.DOKill();
        //     
        //     foreach (var player in Players)
        //     {
        //         player.CanPaddle = true;
        //     }
        //     
        //     ResetCooldownNumber();
        // }
    }

    private void ResetCooldownNumber()
    {
        _textCooldown.SetActive(false);
        _currentNumber = 4;
    }

    private IEnumerator WaitLaunchLoadingScreen()
    {
        yield return new WaitForSeconds(1.5f);
        LaunchLoadingScreen();
    }

    private void LaunchLoadingScreen()
    {
        _loadingsScreen.SetActive(true);

        if (_currentLoadingStep == _loadingsStepSprite.Length)
        {
            LaunchNumber();
            _loadingsScreen.SetActive(false);


            return;
        }

        // _loadingsStep.DOFade(0, 0);
        // _loadingsBG.DOFade(0, 0);

        _loadingsStep.sprite = _loadingsStepSprite[_currentLoadingStep];
        _loadingsBG.DOFade(.8f, _timeBetweenTwoScreen * .25f);
        _loadingsStep.DOFade(1, _timeBetweenTwoScreen * .25f).OnComplete(LoadingWaitFullFade);
    }

    private void LoadingWaitFullFade()
    {
        _loadingsStep.DOFade(1, _timeBetweenTwoScreen * .5f).OnComplete(LoadingFadeOff);
        _raceCam.SetActive(true);
        foreach (var uiPlayer in UIPlayers)
        {
            // Deactivate Canvas of players
            uiPlayer.GoGoGo();
        }
    }

    private void LoadingFadeOff()
    {
        _loadingsStep.DOFade(0, _timeBetweenTwoScreen * .25f).OnComplete(LaunchLoadingScreen);
        _currentLoadingStep++;
    }

    private void LaunchNumber()
    {
        _currentNumber--;
        _textCooldown.SetActive(true);

        if (_currentNumber == 0)
        {
            LaunchGame();
            _textCooldown.GetComponent<TMP_Text>().color = Color.white;
            _textCooldown.transform.DOKill();
            return;
        }

        _textCooldown.transform.DORotate(new Vector3(0, 0, 0), 0);
        _textCooldown.transform.DOScale(Vector3.one, 0);

        if (_currentNumber == 1)
            _textCooldown.GetComponent<TMP_Text>().color = new Color(1f, 0.82f, 0.22f);
        _textCooldown.GetComponent<TMP_Text>().text = $"{_currentNumber}";

        _textCooldown.transform.DOPunchScale(Vector3.one, .2f).OnComplete(WaitNumber);
    }

    private void WaitNumber()
    {
        _textCooldown.transform.DOMove(_textCooldown.transform.position, .5f).OnComplete(RotateText);
    }

    private void RotateText()
    {
        _textCooldown.transform.DORotate(new Vector3(0, 0, -360), .3f, RotateMode.FastBeyond360).SetEase(Ease.OutSine);
        _textCooldown.transform.DOScale(Vector3.zero, .3f).SetEase(Ease.OutSine).OnComplete(LaunchNumber);
    }

    private void LaunchGame()
    {
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
        foreach (var player in Players)
        {
            player.IsDead = false;
        }

        ResetCooldownNumber();
        _raceCam.SetActive(true);
    }

    public void CheckIfAllPlayerDead()
    {
        var count = 0;
        ColorKayak color = ColorKayak.Red;

        foreach (var player in Players)
        {
            if (player.IsDead)
                count++;
            else
                color = player.KayakColor;
        }

        if (count >= Players.Count - 1)
        {
            EndGame(color);
        }
    }

    private void EndGame(ColorKayak color)
    {
        IsGameStarted = false;
        IsGameEnded = true;
        PermanentColorWinner.Instance.KayakColor = color;
        print($"fini {color} win");
        _bg.DOFade(1, 3).OnComplete(ChangeSceneVictory);
    }

    private void ChangeSceneVictory()
    {
        SceneManager.LoadScene(1);
    }

    private void FadeInTextStart()
    {
        _startText.DOFade(1, _timeFadeStart * .25f).OnComplete(WaitTextStart);
    }

    private void WaitTextStart()
    {
        _startText.DOFade(1, _timeFadeStart * .5f).OnComplete(FadeOutTextStart);
    }

    private void FadeOutTextStart()
    {
        _startText.DOFade(0, _timeFadeStart * .25f).OnComplete(FadeInTextStart);
    }

    private void StartFaster()
    {
        _timeBetweenTwoScreen = .1f;
    }
}