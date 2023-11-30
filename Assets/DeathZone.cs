using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using Kayak;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterManager>() != null)
        {
            // print("touche t mort");
            other.GetComponent<CharacterManager>().KillPlayer();
            Manager.Instance.CheckIfAllPlayerDead();
            // gameObject.SetActive(false);
        }
    }
}
