using UnityEngine;
using UnityEngine.UI;

// Struct to hold debug information
public struct DebugInfo
{
    public string BGMName;
    public double SongPosition;
    public double LastBeat;
    public double Crotchet;
    public double DSPTime;
}
public class DebugDisplay : MonoBehaviour
{
    [SerializeField] public Text debugText;

    // Update the debug display with information from the Conductor
    public void UpdateDebugInfo(DebugInfo debugInfo)
    {

        debugText.text = $"Song: {debugInfo.BGMName}\n" +
                         $"Song Position: {debugInfo.SongPosition:F2} seconds\n" +
                         $"DSP Time: {debugInfo.DSPTime:F2} seconds\n" +
                         $"Last Beat Duration: {debugInfo.LastBeat:F2} seconds\n" +
                         $"Crotchet Duration: {debugInfo.Crotchet:F2} seconds";

    }
}