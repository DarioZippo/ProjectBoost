using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    
    [SerializeField] Vector3 movementVector;
    //[SerializeField] [Range(0, 1)] 
    [SerializeField] float period = 2f;
    [SerializeField] bool isMoving = false;

    Vector3 startingPosition;
    Vector3 currentPosition;
    float movementFactor;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        currentPosition = startingPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //Mathf.Epsilon è il minimo rappresentabile da un float
        if(isMoving && period > Mathf.Epsilon) //period != 0f)
        {
            PeriodicMovement();
        }
    }

    private void PeriodicMovement()
    {
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        //L'intervallo così è tra 0 ed 1
        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
