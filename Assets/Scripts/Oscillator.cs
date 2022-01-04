using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    Vector3 startingScale;

    float scaleFactor;

    [SerializeField] float period = 5f;

    const float tau = Mathf.PI * 2; //constant value 6.283

    void Start()
    {
        startingScale = transform.localScale;
    }

    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }

        var cycles = Time.time / Mathf.Clamp(period, 0.1f, float.MaxValue); //constantly growing over time
        var rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1

        scaleFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1

        var randomScale = Random.Range(0.4f, 0.6f);

        Vector3 offset = new Vector3(randomScale, randomScale, randomScale) * scaleFactor;
        transform.localScale = startingScale + offset;
    }
}
