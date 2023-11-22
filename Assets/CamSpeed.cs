using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Kayak;
using UnityEngine;

public class CamSpeed : MonoBehaviour
{
   [SerializeField] private CinemachineDollyCart _camCart;
   [SerializeField] private float _minSpeed = 3f;
   [SerializeField] private float _maxSpeed = 20f;

   private bool _hasKayak;
   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.GetComponent<KayakController>())
      {
         print("touch kayak");
         _hasKayak = true;
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.gameObject.GetComponent<KayakController>())
      {
         print("quit kayak");
         _hasKayak = false;
      }
   }

   private void Update()
   {
      if (!Manager.Instance.IsGameStarted)
         return;
      
      if (_hasKayak)
      {
         if(_camCart.m_Speed <= _maxSpeed)
            _camCart.m_Speed += Time.deltaTime*5;
      }
      else
      {
         if(_camCart.m_Speed >= _minSpeed)
            _camCart.m_Speed -= Time.deltaTime;
      }
   }
}
