/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using System.IO;
using AudioSynthesis;
using AudioSynthesis.Bank;
using AudioSynthesis.Synthesis;
using AudioSynthesis.Sequencer;
using AudioSynthesis.Midi;
using System.Threading;
using UnityMidi;
using System;
using AudioSynthesis.Midi.Event;
using static AudioSynthesis.Midi.MidiFile;

public class pianoNote
{
    public pianoNote()
    {
        //Debug.Log("note created");
    }

    public float width;
    public float length;
    public float depth = 0f;

    public int key;
    public float heightPositionOnScreen;
    public float zIndex = 1f;

    public int velocity;
}

public delegate void MidiTempoChangedEvent(object sender, TempoChangedEventArgs e);

public class TempoChangedEventArgs : EventArgs
{
    public float TimingScalar;
    public float BPM;

    public TempoChangedEventArgs(float bpm, float timingScalar)
    {
        BPM = bpm;
        TimingScalar = timingScalar;
    }
}

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject note;
    [SerializeField] StreamingAssetResouce midiSource;
    [SerializeField] float userLengthMultiplier = 1;


    MidiMessage[] midiMessages;
    List<pianoNote> currentlyDrawing = new List<pianoNote>();

    float camWidth;
    float camHeight;
    float screenUnit;
    public static float magnitude;
    public static int division;
    MidiFile midi;

    public static event MidiTempoChangedEvent MidiLoaded;

    private void Awake()
    {
        Debug.Log("Spawner woke", gameObject);

        //set constant values
        camHeight = Camera.main.orthographicSize * 2f;
        camWidth = camHeight * Screen.width / Screen.height;
        screenUnit = (camWidth / 88f);
        magnitude = .5f * userLengthMultiplier / 98f;
    }

    void Start()
    {
        //import MIDI file
        midi = new MidiFile(midiSource);
        LoadMidi(midi);
    }

    public void LoadMidi(MidiFile file)
    {
        MidiFileSequencer sequencer = new MidiFileSequencer(new Synthesizer(44100, 2, 1024, 1)); //temporary needs changed
        sequencer.Stop();
        sequencer.UnloadMidi();
        sequencer.LoadMidi(file);

        midiMessages = sequencer.getData();
        division = file.Division;

        //-------------------parsing midi
        var currentPosition = 0f;
        var previousDelta = 0f;

        foreach (var message in midiMessages)
        {
            //var delta = (message.delta - previousDelta);
            //currentPosition += delta;
            //previousDelta += delta;

            var delta = message.originalDelta;
            currentPosition += delta;

            for (int i = 0; i < currentlyDrawing.Count; i++)
            {
                currentlyDrawing[i].length += delta;
            }
            if ((int)message.command == 0x80 || ((int)message.command == 0x90 && message.data2 == 0))
            {
                var noteToStop = currentlyDrawing.Find(item => item.key == (message.data1 - 21));
                if (noteToStop != null)
                {
                    Draw(noteToStop, magnitude);
                    currentlyDrawing.Remove(noteToStop);
                }
            }
            else if ((int)message.command == 0x90)
            {
                var toAdd = new pianoNote()
                {
                    length = 0f,
                    velocity = message.data2,
                    key = (int)message.data1 - 21,
                    heightPositionOnScreen = currentPosition
                };
                currentlyDrawing.Add(toAdd);
            }
            if ((int)message.command == 0xFF && message.data1 == 0x51)
            {
                //throw TEMPO CHANGED EVENT
                Debug.Log($"BPM: {message.BPM}");
            }
        }
        //-------------------------------


        //trigger midi loaded event so that any listeners can have access to the midi file division
        TempoChangedEventArgs args = new TempoChangedEventArgs(120, magnitude);
        MidiLoaded?.Invoke(this, args);
    }

    private void Draw(pianoNote toDraw, float sizeScalar)
    {
        toDraw.heightPositionOnScreen *= sizeScalar;
        toDraw.length *= sizeScalar;
        toDraw.heightPositionOnScreen += (camHeight/2f) + (toDraw.length / 2f);

        Vector3 size = new Vector3(screenUnit, toDraw.length, toDraw.depth);
        Vector3 position = new Vector3(
            (toDraw.key * screenUnit) - (camWidth / 2f),
            toDraw.heightPositionOnScreen,
            toDraw.zIndex);

        note.transform.localScale = size;
        Instantiate(note, position, Quaternion.identity);
    }

    private float xToWorldPoint(float x)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(x, 0, 0)).x;
    }

    private float yToWorldPoint(float y)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(0, y, 0)).y;
    }

    private float getPulsesPerQuarterNote()
    {
        return 44100f * (60f / (150 * midi.Division));
    }
}*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using System.IO;
using AudioSynthesis;
using AudioSynthesis.Bank;
using AudioSynthesis.Synthesis;
using AudioSynthesis.Sequencer;
using AudioSynthesis.Midi;
using System.Threading;
using UnityMidi;
using System;
using AudioSynthesis.Midi.Event;
using static AudioSynthesis.Midi.MidiFile;

public class pianoNote
{
    public pianoNote()
    {
        //Debug.Log("note created");
    }

    public float width;
    public float length;
    public float depth = 0f;

    public int key;
    public float heightPositionOnScreen;
    public float zIndex = 1f;

    public int velocity;
}

public delegate void MidiTempoChangedEvent(object sender, TempoChangedEventArgs e);

public class TempoChangedEventArgs : EventArgs
{
    public float TimingScalar;
    public float BPM;

    public TempoChangedEventArgs(float bpm, float timingScalar)
    {
        BPM = bpm;
        TimingScalar = timingScalar;
    }
}

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject note;
    [SerializeField] StreamingAssetResouce midiSource;
    [SerializeField] float userLengthMultiplier = 1;


    MidiMessage[] midiMessages;
    List<pianoNote> currentlyDrawing = new List<pianoNote>();

    float camWidth;
    float camHeight;
    float keyUnit;
    float octaveUnit;
    public static float magnitude;
    public static int division;
    MidiFile midi;

    public static event MidiTempoChangedEvent MidiLoaded;

    private void Awake()
    {
        //Debug.Log("Spawner woke", gameObject);

        //set constant values
        magnitude = .5f * userLengthMultiplier / 98f;
        camHeight = Camera.main.orthographicSize * 2f;
        camWidth = camHeight * Screen.width / Screen.height;
        keyUnit = (camWidth / 52f); //screenUnit is the size of one WHITE key on a piano.
        octaveUnit = keyUnit * 7f; //number of white keys before pattern repeats
    }

    void Start()
    {
        //import MIDI file
        midi = new MidiFile(midiSource);
        LoadMidi(midi);
    }

    public void LoadMidi(MidiFile file)
    {
        MidiFileSequencer sequencer = new MidiFileSequencer(new Synthesizer(44100, 2, 1024, 1)); //temporary needs changed
        sequencer.Stop();
        sequencer.UnloadMidi();
        sequencer.LoadMidi(file);

        midiMessages = sequencer.getData();
        division = file.Division;

        //-------------------parsing midi
        var currentPosition = 0f;
        var previousDelta = 0f;

        foreach (var message in midiMessages)
        {
            /*var delta = (message.delta - previousDelta);
            currentPosition += delta;
            previousDelta += delta;*/

            var delta = message.originalDelta;
            currentPosition += delta;

            for (int i = 0; i < currentlyDrawing.Count; i++)
            {
                currentlyDrawing[i].length += delta;
            }
            if ((int)message.command == 0x80 || ((int)message.command == 0x90 && message.data2 == 0))
            {
                var noteToStop = currentlyDrawing.Find(item => item.key == (message.data1 - 21));
                if (noteToStop != null)
                {
                    Draw(noteToStop, magnitude);
                    currentlyDrawing.Remove(noteToStop);
                }
            }
            else if ((int)message.command == 0x90)
            {
                var toAdd = new pianoNote()
                {
                    length = 0f,
                    velocity = message.data2,
                    key = (int)message.data1 - 21,
                    heightPositionOnScreen = currentPosition
                };
                currentlyDrawing.Add(toAdd);
            }
            if ((int)message.command == 0xFF && message.data1 == 0x51)
            {
                //throw TEMPO CHANGED EVENT
                //Debug.Log($"BPM: {message.BPM}");
            }
        }
        //-------------------------------


        //trigger midi loaded event so that any listeners can have access to the midi file division
        TempoChangedEventArgs args = new TempoChangedEventArgs(120, magnitude);
        MidiLoaded?.Invoke(this, args);
    }

    private void Draw(pianoNote toDraw, float sizeScalar)
    {
        //Defines whether the key is white or black.
        int[] whiteKeyMods = new int[7] { 0, 2, 3, 5, 7, 8, 10 };


        toDraw.heightPositionOnScreen *= sizeScalar;
        toDraw.length *= sizeScalar;
        toDraw.heightPositionOnScreen += (camHeight / 2f) + (toDraw.length / 2f);

        //get the octave a key resides in
        //casting a float as an int will truncate off the decimal places. 
        //effect is equivalent to Math.Floor as long as initial value is positive
        int octave = (int)(toDraw.key / 12f);

        bool keyIsBlack = true;
        float whiteBaseValue = .5f; //keys are off by one half white key width starting at 0 for some reason. use .5f to fix

        for (int i = 0; i < whiteKeyMods.Length; i++)
        {
            if (toDraw.key % 12 == whiteKeyMods[i])
            {
                whiteBaseValue += i;
                keyIsBlack = false;
                break;
            }
        }
        if (keyIsBlack)
        {
            for (int i = 0; i < whiteKeyMods.Length; i++)
            {
                if (((toDraw.key - 1) % 12) == whiteKeyMods[i])
                {
                    whiteBaseValue += i + .5f;//because no two black keys touch, we add half to 
                                              //get the position of the black key relative to the next white key down. 
                                              //(assuming each black key is one half a white key width up)
                    break;
                }
            }
        }

        //defines the size and position
        Vector3 size = new Vector3(keyUnit + .2f, toDraw.length, toDraw.depth);
        Vector3 position = new Vector3(
            (octave * octaveUnit) + (whiteBaseValue * keyUnit) - (camWidth / 2f),
            toDraw.heightPositionOnScreen,
            toDraw.zIndex + 100);

        //draw note to screen
        note.transform.localScale = size;
        Instantiate(note, position, Quaternion.identity);
    }
}

