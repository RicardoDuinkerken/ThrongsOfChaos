using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    [SerializeField] private Vector3 rotation;
    [SerializeField] private float turnSpeed;
    [SerializeField] private Transform transformToRotate;

    // Update is called once per frame
    void Update()
    {
        transformToRotate.Rotate(rotation * (turnSpeed * Time.deltaTime));
    }
}
