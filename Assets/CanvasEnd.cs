using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CanvasEnd : MonoBehaviour
{
    [SerializeField] private Image _bg;
    void Start()
    {
        _bg.DOFade(0, 1f);
    }
}
