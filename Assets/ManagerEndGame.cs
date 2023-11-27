using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerEndGame : MonoBehaviour
{
    [Header("Mesh")]
    [SerializeField] private KayakMeshRenderer _kayakWinner;
    [SerializeField] private KayakMeshRenderer[] _kayakLoser;
    
    [Header("Mat")]
    [SerializeField] private Material[] _kayakMat;
    [SerializeField] private Material[] _bodyMat;

    private void Start()
    {
        if (PermanentColorWinner.Instance != null)
        {
            var colorWinner = (int)PermanentColorWinner.Instance.KayakColor;
            _kayakWinner.KayakMesh.material = _kayakMat[colorWinner];
            _kayakWinner.CoatMesh.material = _bodyMat[colorWinner];
            _kayakWinner.HoodMesh.material = _bodyMat[colorWinner];

            var count = 0;
            for (int i = 0; i < _kayakLoser.Length; i++)
            {
                if (count == colorWinner)
                    count++;
                
                _kayakLoser[i].KayakMesh.material = _kayakMat[count];
                _kayakLoser[i].CoatMesh.material = _bodyMat[count];
                _kayakLoser[i].HoodMesh.material = _bodyMat[count];
                
                count++;
            }
        }
    }
}

[Serializable]
public class KayakMeshRenderer
{
    public MeshRenderer KayakMesh;
    public SkinnedMeshRenderer CoatMesh;
    public SkinnedMeshRenderer HoodMesh;
}
