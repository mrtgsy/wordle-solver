namespace Blazordle;

public class LetterGuess
{

    public LetterGuess(int pos, string val, GuessService.LetterState letterState)
    {
        position = pos;
        value = val;
        state = letterState;
    }

    public int position;
    public string value;
    private GuessService.LetterState state;

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

    public List<LetterGuess> guesses = new();

    public event Action? OnChange;

    public void AddUpdateGuess(int pos, string val, LetterState letterState)
    {
        if (guesses.Any(x => x.value == val))
        {
            guesses.Remove(guesses.First(x => x.value == val));
        }

        if (letterState != LetterState.None)
        {
            guesses.Add(new LetterGuess(pos, val, letterState));
        }
        NotifyStateChanged();
    }
    
    private void NotifyStateChanged() => OnChange?.Invoke();
}