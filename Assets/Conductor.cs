using System;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    public float BPM { get; private set; } = 140f;
    public float Crotchet { get; private set; } //Duration of a crotchet in seconds
    public double SongPosition { get; private set; } //Position in the song in seconds


    // Reference to the currently playing BGM
    private AudioClasses.Audio _currentBGM;

    // Event to notify subscribers when the beat changes
    public event Action OnBeat;

    // Moving reference point for beat detection
    private double lastBeat;
    private double dsptimesong; // Initial dspTime when the song starts
    private double offset; // Offset to fine-tune synchronization




    void Awake()
    {
        AudioManager.instance.Load();
    }

    void Start()
    {
        dsptimesong = AudioSettings.dspTime;
        offset = 0; // You can fine-tune this offset as needed
        lastBeat = 0;

        CalculateCrotchet();
        _currentBGM = AudioManager.instance.PlayBGM("feel_good");
    }

    void Update()
    {
        // Update the song position based on the elapsed time
        UpdateSongPosition();
        UpdateDebugDisplay();
    }

    private void CalculateCrotchet()
    {
        Crotchet = 60f / BPM;
    }


    private void UpdateSongPosition()
    {
        // Increment the song position based on the elapsed time
        SongPosition = AudioSettings.dspTime - dsptimesong - offset;
        CheckForBeat();
    }



    // Function to get the current song position
    public double GetSongPosition()
    {
        return SongPosition;
    }


    // Function to check for a beat and invoke the event
    private void CheckForBeat()
    {
        bool newBeatOccured = SongPosition > lastBeat + Crotchet;
        // Determine when a new beat occurs based on your game's logic
        if (newBeatOccured)
        {
            OnBeat?.Invoke();
            lastBeat += Crotchet;
        }
    }

    [SerializeField] public DebugDisplay debugDisplay;


    private void UpdateDebugDisplay()
    {
        DebugInfo debugInfo = CreateDebugInfo();
        debugDisplay?.UpdateDebugInfo(debugInfo);
    }

    private DebugInfo CreateDebugInfo()
    {
        return new DebugInfo
        {
            BGMName = _currentBGM.Name,
            SongPosition = SongPosition,
            LastBeat = lastBeat,
            Crotchet = Crotchet,
            DSPTime = AudioSettings.dspTime
        };
    }
}
