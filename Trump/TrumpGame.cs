using On20240313OOPCardGame.Abstract;

namespace On20240313OOPCardGame.Trump;

/**
 * https://www.ameerbacchus.com/trump/trumprules.html, but the Trump suit is chosen randomly.
 */
public class TrumpGame : Game<TrumpPlayer>
{
    private Suit _trumps;
    private int _numTricks;
    private int _firstPlayerIndexThisTrick; // Index in GetPlayers() of player with first turn for this trick.
    
    // Constructor
    public TrumpGame(List<TrumpPlayer> players, int numTricks) : base(players)
    {
        // Number of cards per hand = number of tricks to be played
        _numTricks = numTricks;
        
        // Deal deck
        GetDeck().DealTo(GetPlayers(), _numTricks);
        
        // Set trumps suit
        Random rand = new Random();
        int trumpsIndex = rand.Next(0, 4);
        switch (trumpsIndex)
        {
            case 0:
                _trumps = Suit.Clubs;
                break;
            case 1:
                _trumps = Suit.Diamonds;
                break;
            case 2:
                _trumps = Suit.Hearts;
                break;
            case 3:
                _trumps = Suit.Spades;
                break;
        }
    }
    
    protected override bool PlayRoundAndReturnIfGameEnded(int roundIndex)
    {
        // Run a round of the game.
        
        // Play
        List<TrumpPlayer> players = GetPlayers();
            
        // For each player, starting with the first player index this turn and returning to the start of the list
        // from the end.
            
        // First player
        players[_firstPlayerIndexThisTrick].StartConsole();
        ShowTrumpStats(false, roundIndex, players, _firstPlayerIndexThisTrick, new List<Card>());
        List<Card> cardsPlayedThisTrick = new List<Card> { players[_firstPlayerIndexThisTrick].RunFirstTurn() };
        Suit suitThisTrick = cardsPlayedThisTrick[0].GetSuit();
            
        // Later players
        for(int playerI = (_firstPlayerIndexThisTrick + 1) % players.Count; playerI != _firstPlayerIndexThisTrick; playerI = (playerI+1)%players.Count)
        {
            players[playerI].StartConsole();
            ShowTrumpStats(false, roundIndex, players, _firstPlayerIndexThisTrick, cardsPlayedThisTrick);
                
            cardsPlayedThisTrick.Add(players[playerI].RunTurn(suitThisTrick, cardsPlayedThisTrick));
        }
            
        // Find winner of trick, then convert it to the original player order
        int winnerPlayerI = (GetWinnerIndex(cardsPlayedThisTrick, suitThisTrick) + _firstPlayerIndexThisTrick) % players.Count;
        players[winnerPlayerI].AddTrick();
            
        Console.Clear();
        ShowTrumpStats(false, roundIndex, players, _firstPlayerIndexThisTrick, cardsPlayedThisTrick);
        Console.WriteLine($"**{players[winnerPlayerI].GetName()}** won this trick - Let everyone know, then press any key.");
        Console.ReadKey();

        if (roundIndex >= _numTricks)
        {
            return true;
        }
            
        // Winner starts next trick
        _firstPlayerIndexThisTrick = winnerPlayerI;

        return false;
    }

    protected override void AfterGameLoop()
    {
        // Show final game stats
        Console.Clear();
        ShowTrumpStats(true, 0, this.GetPlayers(), _firstPlayerIndexThisTrick, new List<Card>());
    }

    private int GetWinnerIndex(List<Card> cardsPlayedThisTrick, Suit suitThisTrick)
    {
        // Get the index of the winner of the trick in cardsPlayed
        int maxCardScore = GetCardScore(cardsPlayedThisTrick[0], suitThisTrick);
        int winningCardIndex = 0;
        
        for (int i = 1; i < cardsPlayedThisTrick.Count; i++)
        {
            int cardScore = GetCardScore(cardsPlayedThisTrick[i], suitThisTrick);
            if (cardScore > maxCardScore)
            {
                maxCardScore = cardScore;
                winningCardIndex = i;
            }
        }
        
        return winningCardIndex;
    }

    protected override List<TrumpPlayer> GetWinners()
    {
        List<TrumpPlayer> players = GetPlayers();
        
        // Find winners with most tricks
        int maxNumTricks = players[0].GetNumTricks();
        List<int> winnerPlayersI = new List<int> { 0 };
        for (int i = 1; i < players.Count; i++)
        {
            int numTricks = players[i].GetNumTricks();
            if (numTricks == maxNumTricks)
            {
                winnerPlayersI.Add(i);
            }
            else if(numTricks > maxNumTricks)
            {
                winnerPlayersI = new List<int> { i };
            }
        }

        return players;
    }

    private void ShowTrumpStats(bool endedGame, int trickI, List<TrumpPlayer> players, int _firstPlayerIndexThisTrick, List<Card> cardsPlayedThisTrick)
    {
        if (endedGame)
        {
            Console.WriteLine($"=== Game Ended / Trick {trickI+1} of {_numTricks} ===");
        }
        else
        {
            Console.WriteLine($"=== Trumps are {_trumps.ToString()} / Trick {trickI+1} of {_numTricks} ===");
        }
                
        Console.Write("Players   \t");
        Console.Write($"{players[_firstPlayerIndexThisTrick].GetName()}\t");
        for (int j = (_firstPlayerIndexThisTrick+1)%players.Count; j != _firstPlayerIndexThisTrick; j = (j+1)%players.Count)
        {
            Console.Write($"{players[j].GetName()}\t");
        }
        Console.WriteLine();
                
        Console.Write("Won tricks\t");
        Console.Write($"{players[_firstPlayerIndexThisTrick].GetNumTricks()}\t");
        for (int j = (_firstPlayerIndexThisTrick+1)%players.Count; j != _firstPlayerIndexThisTrick; j = (j+1)%players.Count)
        {
            Console.Write($"{players[j].GetNumTricks()}\t");
        }
        Console.WriteLine();

        if (!endedGame)
        {
            Console.Write("This trick\t");
            for (int j = 0; j < cardsPlayedThisTrick.Count; j++)
            {
                Console.Write($"{cardsPlayedThisTrick[j].ToShortString()}\t");
            }
            Console.WriteLine();
        }
                
        Console.WriteLine();
    }

    private int GetCardScore(Card card, Suit suitToFollow)
    {
        if (card.GetSuit() == suitToFollow)
        {
            return (int)card.GetValue();
        }
        if (card.GetSuit() == _trumps)
        {
            // Trumps beat other suits
            return (int)card.GetValue() + 13;
        }
        // Not following suit gets no points
        return 0;
    }
}