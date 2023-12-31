using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PermanentColorWinner : MonoBehaviour
{
    public static PermanentColorWinner Instance;

    public ColorKayak KayakColor { get; set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

public enum ColorKayak
{
    Red = 0,
    Green = 1,
    Yellow = 2,
    Purple = 3
}
