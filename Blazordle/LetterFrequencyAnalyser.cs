namespace Blazordle;

public class LetterFrequencyAnalyser
{
    private readonly Dictionary<char, double> LetterFrequencies;

    public LetterFrequencyAnalyser()
    {
        LetterFrequencies = DefaultLetterFrequencies;
    }

    public LetterFrequencyAnalyser(List<string> wordlist)
    {
        LetterFrequencies = CalculateLetterFrequencies(wordlist);
    }
    
    /// <summary>
    ///  Letter frequencies from across the English language
    /// </summary>
    private static readonly Dictionary<char, double> DefaultLetterFrequencies = new()
    {
        {'a', 8.167}, {'b', 1.492}, {'c', 2.782}, {'d', 4.253}, {'e', 12.702},
        {'f', 2.228}, {'g', 2.015}, {'h', 6.094}, {'i', 6.966}, {'j', 0.153},
        {'k', 0.772}, {'l', 4.025}, {'m', 2.406}, {'n', 6.749}, {'o', 7.507},
        {'p', 1.929}, {'q', 0.095}, {'r', 5.987}, {'s', 6.327}, {'t', 9.056},
        {'u', 2.758}, {'v', 0.978}, {'w', 2.360}, {'x', 0.150}, {'y', 1.974}, {'z', 0.074}
    };
    
    public double CalculateWordWeight(string word)
    {
        return word.ToLower()
            .Where(char.IsLetter)
            .Sum(c => LetterFrequencies.GetValueOrDefault(c, 0));
    }

    private Dictionary<char, double> CalculateLetterFrequencies(List<string> wordList)
    {
        // Initialize dictionary
        var letterCounts = new Dictionary<char, int>();

        // Count all letters
        foreach (var word in wordList)
        {
            foreach (var letter in word.ToLower())
            {
                if (char.IsLetter(letter)) // Ignore non-letter chars
                {
                    if (letterCounts.ContainsKey(letter))
                    {
                        letterCounts[letter]++;
                    }
                    else
                    {
                        letterCounts.Add(letter, 1);
                    }
                }
            }
        }

        // Calculate frequency
        var totalLetters = letterCounts.Values.Sum();

        return letterCounts.ToDictionary(entry => entry.Key, entry => 100.0 * entry.Value / totalLetters);
    }
}