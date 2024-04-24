using System.Diagnostics.CodeAnalysis;

namespace On20240313OOPCardGame.Abstract;

public abstract class Game<TGamePlayer>
    where TGamePlayer : Player
{
    // Attributes
    private readonly Deck _deck;
    private List<TGamePlayer> _players;
    
    // Constructor
    protected Game(List<TGamePlayer> players)
    {
        _deck = new Deck();
        _deck.Shuffle();
        _players = players;
    }
    
    // Public - can be accessed externally
    public void Play()
    {
        for (int i = 0; i < GetNumRounds(); i++)
        {
            PlayRound(i);
        }
        
        // Show winner
        Console.Clear();
        ShowGameStats(true, 0, players, _firstPlayerIndexThisTrick, new List<Card>());

        if (winnerPlayersI.Count == 0)
        {
            Console.Write("Nobody");
        } 
        else if (winnerPlayersI.Count == 1)
        {
            Console.Write($"**{players[winnerPlayersI[0]].GetName()}**");
        }
        else
        {
            for (int i = 0; i < winnerPlayersI.Count-2; i++)
            {
                Console.Write($"**{players[i].GetName()}**, ");
            }
            Console.Write($"**{players[winnerPlayersI.Count-2].GetName()}** and **{players[winnerPlayersI.Last()].GetName()}**");
        }
        Console.WriteLine(" won the most tricks, and therefore won the game!");
        Console.WriteLine("Press any key.");
        Console.ReadKey();
    }
    
    // Protected - for subclasses to use
    protected Deck GetDeck()
    {
        return _deck;
    }
    
    protected List<TGamePlayer> GetPlayers()
    {
        return _players;
    }
    
    // Abstract - overriden in subclasses via polymorphism
    protected abstract void PlayRound(int roundIndex);
    protected abstract int GetNumRounds();
    protected abstract List<TGamePlayer> GetWinners();
}