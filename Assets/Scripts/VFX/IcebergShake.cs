using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcebergShake : MonoBehaviour
{ 
    [SerializeField] float speed; //how fast it shakes
    [SerializeField] float amountMin; //how much it shakes
    [SerializeField] float amountMax; //how much it shakes
    [HideInInspector] public bool hasFallen = false;

    private void Update()
    {
        if (!hasFallen)
        {
            float rngX = Random.Range(amountMin * 0.01f, amountMax * 0.01f);
            float rngY = Random.Range(amountMin * 0.01f, amountMax * 0.01f);
            float rngZ = Random.Range(amountMin * 0.01f, amountMax * 0.01f);

            transform.position = new Vector3
                (transform.position.x + Mathf.Sin(Time.time * speed) * rngX, 
                 transform.position.y + Mathf.Sin(Time.time * speed) * rngY,
                 transform.position.z + Mathf.Sin(Time.time * speed) * rngZ);

        }
    }
}
