using System;

/// <summary>
/// Represents a score by recording a name and a value.
/// </summary>
[Serializable]
public class Score
{
    /// <summary>
    /// Name of the person who achieved this score.
    /// </summary>
    public string Name;

    /// <summary>
    /// Score value achieved.
    /// </summary>
    public int Value;
}
