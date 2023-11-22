using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayer : MonoBehaviour
{
    [SerializeField] private TMP_Text _textPressBtn;
    [SerializeField] private TMP_Text _textIsReady;
    public bool HasClicked { get; set; }
    public bool IsRunning { get; set; }
    public bool IsReady { get; private set; }

    private bool _hasClickedAgain;
    private float _cooldownCanClick;

    private void Start()
    {
        _cooldownCanClick = 1;
    }

    void Update()
    {
        if (_cooldownCanClick > 0)
        {
            _cooldownCanClick -= Time.deltaTime;
            return;
        }
        
        
        if (IsRunning)
            return;

        if (HasClicked)
        {
            if (_hasClickedAgain)
                return;

            _hasClickedAgain = true;

            if (!IsReady)
            {
                IsReady = true;
                _textIsReady.gameObject.SetActive(true);
                _textPressBtn.enabled = false;
            }
            else
            {
                IsReady = false;
                _textIsReady.gameObject.SetActive(false);
                _textPressBtn.enabled = true;
            }

            Manager.Instance.CheckIfAllPlayersReady();
        }
        else
        {
            _hasClickedAgain = false;
        }
    }

    public void GoGoGo()
    {
        IsRunning = true;
        _textIsReady.gameObject.SetActive(false);
        _textPressBtn.enabled = false;
    }
}