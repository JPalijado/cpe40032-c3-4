using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBackRotor : MonoBehaviour
{
    private float rotationSpeed = 1000;

    void Update()
    {
        // Rotates the back rotor
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
}
