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
   [SerializeField] private float _acceleration = 5f;
   [SerializeField] private float _deceleration = 5f;

   private bool _hasKayak;
   private int _kayakNb;
   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.GetComponent<KayakController>())
      {
          // print("touch kayak");
          _kayakNb++;
          if (_kayakNb > 4)
             _kayakNb = 4;
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.gameObject.GetComponent<KayakController>())
      {
         // print("quit kayak");
         _kayakNb--;
         if (_kayakNb < 0)
            _kayakNb = 0;
      }
   }

   private void Update()
   {
      if (!Manager.Instance.IsGameStarted)
         return;

      if (_camCart.m_Speed < _minSpeed)
         _camCart.m_Speed = _minSpeed;
      
      if (_kayakNb > 0)
      {
         if(_camCart.m_Speed <= _maxSpeed)
            _camCart.m_Speed += Time.deltaTime * _acceleration;
      }
      else
      {
         if(_camCart.m_Speed >= _minSpeed)
            _camCart.m_Speed -= Time.deltaTime * _deceleration;
      }
   }
}
