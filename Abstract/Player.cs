using On20240313OOPCardGame.Interfaces;

namespace On20240313OOPCardGame;

public abstract class Player : IDealReceiver
{
    // Attributes
    private String _name;
    private Hand _hand;
    
    // Constructor
    protected Player(String name)
    {
        _hand = new Hand();
        _name = name;
    }
    
    // Getters
    public String GetName()
    {
        return _name;
    }
    
    // Setters
    public void AddCard(Card card)
    {
        _hand.AddCard(card);
    }
    
    // Protected - for subclasses to use
    protected Hand GetHand()
    {
        return _hand;
    }

    protected int ChooseCardIndex()
    {
        Console.WriteLine("=== Choose A Card ===");
        List<Card> cards = _hand.GetCards();
        for (int i = 0; i < cards.Count; i++)
        {
            Console.WriteLine($"{i}\t{cards[i].ToTabbedString()}");
        }
        int chosenCardI = -1;
        while (chosenCardI < 0 || chosenCardI >= cards.Count)
        {
            Console.Write($"(0-{cards.Count - 1})\tPlease enter the card's number: ");
            try
            {
                chosenCardI = int.Parse(Console.ReadLine());
            }
            catch(FormatException)
            {
                // Non-number given; make invalid
                chosenCardI = -1;
            }
        }
        return chosenCardI;
    }

    public void StartConsole()
    {
        // Prepare the console for the player to input data
        Console.Clear();
        Console.WriteLine($"Pass the console to *{_name}* then press any key.");
        Console.ReadKey();
        Console.Clear();
        Console.WriteLine($"Private for {_name}");
        Console.WriteLine();
    }
}