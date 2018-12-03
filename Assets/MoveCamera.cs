using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    public float speedMultiplier = 1f;
    private float tempo = 120f; //in beats per minute. 120 is default
    private float camSpeed; // in untiy units per second

    private void Awake()
    {
        Spawner.MidiLoaded += MidiLoaded;
    }

    private void Start()
    {
        UpdateTempo(this.tempo);
    }

    private void MidiLoaded(object sender, TempoChangedEventArgs e)
    {
    }


    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * (Time.deltaTime * camSpeed);
    }

    float BPMtoUPS(float BPM)
    {
        //return (BPM * Spawner.magnitude * Spawner.division * speedMultiplier) / 60f; //use if you are defining notes based on the original delta times.
        return ((float)Spawner.synthSampleRate * Spawner.magnitude); //use if note lengths are defined based on absolute delta length.
    }

    public void UpdateTempo(float BPM)
    {
        this.camSpeed = BPMtoUPS(BPM);
    }
}