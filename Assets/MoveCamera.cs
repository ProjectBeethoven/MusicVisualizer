using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    public float speedMultiplier = 1f;
    private float tempo = 150f; //in beats per minute
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
        //transform.position += Vector3.up * (Time.deltaTime*camSpeed);	
        transform.position += Vector3.up * (Time.deltaTime * camSpeed);
    }

    float BPMtoUPS(float BPM)
    {
        return (BPM * Spawner.magnitude * Spawner.division * speedMultiplier) / 60f;
    }

    public void UpdateTempo(float BPM)
    {
        this.camSpeed = BPMtoUPS(BPM);
    }
}