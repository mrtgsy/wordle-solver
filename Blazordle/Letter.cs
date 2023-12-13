using Microsoft.AspNetCore.Components.Forms;

namespace Blazordle;

public class Letter(int position)
{
    public enum LetterState
    {
        None,
        Right,
        RightWrong,
        Wrong
    }
    
    public string Name => $"letter{Position}";
    public InputText? Input { get; set; }
    public int Position { get; } = position;
    public string? Value { get; set; }
    public LetterState State { get; private set; } = LetterState.None;

    public void SetState()
    {
        State = State switch
        {
            LetterState.None => LetterState.Right,
            LetterState.Right => LetterState.RightWrong,
            LetterState.RightWrong => LetterState.Wrong,
            _ => LetterState.None
        };
    }
    
    public override string ToString()
    {
        return $"{Value}:{Position}:{State}";
    }
}