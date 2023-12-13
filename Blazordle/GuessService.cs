namespace Blazordle;

public class GuessService
{
   
    public List<Letter> Guesses = [];

    public bool HasPerfectMatch
    {
        get
        {
            return Guesses.Any(x => x.State == Letter.LetterState.Right);
        }
    }
    
    public bool HasWronguns
    {
        get
        {
            return Guesses.Any(x => x.State == Letter.LetterState.Wrong);
        }
    }
    
    public bool HasImperfections
    {
        get
        {
            return Guesses.Any(x => x.State == Letter.LetterState.RightWrong);
        }
    }

    public event Action? OnChange;

    public void AddUpdateGuess(Letter letter)
    {
        if (Guesses.Any(x => x.Value == letter.Value && x.Position == letter.Position))
        {
            Guesses.Remove(Guesses.First(x => x.Value == letter.Value && x.Position == letter.Position));
        }

        if (letter.State != Letter.LetterState.None)
        {
            Guesses.Add(letter);
        }
        NotifyStateChanged();
    }
    
    private void NotifyStateChanged() => OnChange?.Invoke();
}

public static class Extensions
{
    private static Random rng = new Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        var n = list.Count;
        while (n > 1)
        {
            n--;
            var k = rng.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}