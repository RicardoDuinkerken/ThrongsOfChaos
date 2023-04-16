using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    [SerializeField] private float amplitude, frequency;
    [SerializeField] private Transform transformToHover;
    private Vector3 _initialPosition;

    private void Awake()
    {
        _initialPosition = transformToHover.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the new position based on a sine wave
        float newY = _initialPosition.y + amplitude * Mathf.Sin(frequency * Time.time);
        Vector3 newPosition = new Vector3(_initialPosition.x, newY, _initialPosition.z);

        // Smoothly move the object to the new position
        transformToHover.position = Vector3.Lerp(transformToHover.position, newPosition, Time.deltaTime);
    }
}
