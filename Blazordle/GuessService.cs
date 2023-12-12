namespace Blazordle;

public class LetterGuess
{
    public LetterGuess(int pos, string val, GuessService.LetterState letterState)
    {
        position = pos;
        value = val;
        state = letterState;
    }

    public int position { get; }
    public string value { get; }
    public GuessService.LetterState state { get; }

    public override string ToString()
    {
        return $"{value}:{position}:{state}";
    }
}

public class GuessService
{
    public enum LetterState
    {
        None,
        Right,
        RightWrong,
        Wrong
    }

    public List<LetterGuess> guesses = [];

    public bool HasPerfectMatch
    {
        get
        {
            return guesses.Any(x => x.state == LetterState.Right);
        }
    }
    
    public bool HasWronguns
    {
        get
        {
            return guesses.Any(x => x.state == LetterState.Wrong);
        }
    }
    
    public bool HasImperfections
    {
        get
        {
            return guesses.Any(x => x.state == LetterState.RightWrong);
        }
    }

    public event Action? OnChange;

    public void AddUpdateGuess(int pos, string val, LetterState letterState)
    {
        if (guesses.Any(x => x.value == val && x.position == pos))
        {
            guesses.Remove(guesses.First(x => x.value == val && x.position == pos));
        }

        if (letterState != LetterState.None)
        {
            guesses.Add(new LetterGuess(pos, val, letterState));
        }
        NotifyStateChanged();
    }
    
    private void NotifyStateChanged() => OnChange?.Invoke();
}