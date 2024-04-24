namespace On20240313OOPCardGame.Trump;

public class TrumpPlayer : Player
{
    // Attributes
    private int _numTricks;
    
    // Constructor
    public TrumpPlayer(String name) : base(name)
    {
        _numTricks = 0;
    }
    
    // Getters
    public int GetNumTricks()
    {
        return _numTricks;
    }
    
    // Setters
    public void AddTrick()
    {
        _numTricks++;
    }
    
    // Game
    public Card RunFirstTurn()
    {
        // Run the first turn for this trick and return the card played.
        Console.WriteLine("Your turn is the first for this trick. Choose any suit.");
        Console.WriteLine();
        return GetHand().TakeCardAtIndex(ChooseCardIndex());
    }
    
    public Card RunTurn(Suit suitThisTrick, List<Card> cardsPlayedThisTrick)
    {
        // Run a later turn for this trick and return the card played.
        
        // Must play suitThisTrick if have a card of that suit.

        if (GetHand().HasSuit(suitThisTrick))
        {
            int chosenCardIndex;
            do
            {
                // TODO: Allow if no card in suit
                Console.WriteLine($"You must choose a card from {suitThisTrick.ToString()}.");
                Console.WriteLine();
                chosenCardIndex = ChooseCardIndex();
            } while (GetHand().GetCards()[chosenCardIndex].GetSuit() != suitThisTrick);
        
            return GetHand().TakeCardAtIndex(chosenCardIndex);   
        }
        else
        {
            Console.WriteLine($"You don't have any {suitThisTrick.ToString()} so don't need to follow suit.");
            Console.WriteLine();
            return GetHand().TakeCardAtIndex(ChooseCardIndex());
        }
    }
}