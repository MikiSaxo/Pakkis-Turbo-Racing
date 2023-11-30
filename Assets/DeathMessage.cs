using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DeathMessage : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private const string Message = " Player Died!";

    public void Init(ColorKayak color)
    {
        switch (color)
        {
            case ColorKayak.Green:
                _text.text = $"Green {Message}";
                _text.color = new Color(0f, 0.53f, 0f);
                break;
            case ColorKayak.Purple:
                _text.text = $"Purple {Message}";
                _text.color = new Color(0.51f, 0.15f, 0.53f);
                break;
            case ColorKayak.Yellow:
                _text.text = $"Yellow {Message}";
                _text.color = new Color(0.53f, 0.53f, 0.04f);
                break;
            case ColorKayak.Red:
                _text.text = $"Red {Message}";
                _text.color = new Color(0.53f, 0f, 0f);
                break;
        }
    }

    void Start()
    {
        gameObject.transform.DOPunchScale(Vector3.one, .5f);
        _text.DOFade(0, 3f).OnComplete(Death);
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}