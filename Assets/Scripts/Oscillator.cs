using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 moveVector;
    [SerializeField] [Range(0,1)] float moveFactor;
    [SerializeField] float period = 2f;

    Vector3 startingPos;
    // Start is called before the first frame update
    void Start() {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (period <= Mathf.Epsilon) { return; } //ALMOST ZERO
        float cycles = Time.time / period; // CYCLE OF THE SINE WAVE

        //moveFactor = Mathf.Abs(Mathf.Sin(cycles * Mathf.PI * 2)); <----- CURVE TOO ABRUPT, DO NOT RECOMMEND.
        /* APPLYING THE SINE WAVE */moveFactor = (Mathf.Sin(cycles * Mathf.PI * 2) + 1) / 2; // <---- WAY SMOOTHIER MOVEMENT

        Vector3 offset = moveVector * moveFactor;
        transform.position = startingPos + offset;
    }
}
