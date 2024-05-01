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
        bool gameEnded = false;
        int roundIndex = 0;
        while (!gameEnded)
        {
            gameEnded = PlayRoundAndReturnIfGameEnded(roundIndex);
            roundIndex++;
        }
        AfterGameLoop();

        List<TGamePlayer> winners = GetWinners();
        
        if (winners.Count == 0)
        {
            Console.Write("Nobody");
        } 
        else if (winners.Count == 1)
        {
            Console.Write($"**{winners[0].GetName()}**");
        }
        else
        {
            for (int i = 0; i < winners.Count-2; i++)
            {
                Console.Write($"**{winners[i].GetName()}**, ");
            }
            Console.Write($"**{winners[winners.Count-2].GetName()}** and **{winners.Last().GetName()}**");
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
    
    /**
     * Play one round in the main game loop and return true if this is the last round, and false if it is not.
     */
    protected abstract bool PlayRoundAndReturnIfGameEnded(int roundIndex);
    
    /**
     * Run some code after the main game loop, often to show the final game standings. There's no need to show the
     * winner here - the Game class will handle it.
     */
    protected abstract void AfterGameLoop();
    
    /**
     * Return the list of winners of the game, run after PlayRoundAndReturnIfGameEnded returns true.
     */
    protected abstract List<TGamePlayer> GetWinners();
}