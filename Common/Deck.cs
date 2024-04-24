using On20240313OOPCardGame.Interfaces;

namespace On20240313OOPCardGame;

public class Deck
{
    // Attributes
    private List<Card> _cards;
    
    // Methods
    // Constructor
    public Deck()
    {
        _cards = new List<Card>();
        foreach(Suit suit in Enum.GetValues(typeof(Suit))) {
            foreach(CardValue value in Enum.GetValues(typeof(CardValue))) {
                _cards.Add(new Card(suit, value));
            }
        }
    }
    
    // Getters
    public bool IsEmpty()
    {
        return _cards.Count == 0;
    }

    public Card TakeCard()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Cannot take a Card from an empty Deck.");
        }

        Card card = _cards.Last();
        _cards.RemoveAt(_cards.Count - 1);
        return card;
    }
    
    // Setters
    public void Shuffle()
    {
        Random rnd = new Random();
        for (int firstCardI = 0; firstCardI < _cards.Count-1; firstCardI++)
        {
            int secondCardI = rnd.Next(firstCardI + 1, _cards.Count - 1);
            
            // Swap cards at firstCardI, secondCardI
            Card oldSecondCard = _cards[secondCardI];
            _cards[secondCardI] = _cards[firstCardI];
            _cards[firstCardI] = oldSecondCard;
        }
    }

    public void DealTo<TDealReceiver>(List<TDealReceiver> receivers, int cardsPerHand)
        where TDealReceiver : IDealReceiver
    {
        foreach (TDealReceiver receiver in receivers)
        {
            for (int i = 0; i < cardsPerHand; i++)
            {
                try
                {
                    receiver.AddCard(this.TakeCard());
                }
                catch (InvalidOperationException)
                {
                    throw new InvalidOperationException("Not enough Cards to deal from this Deck.");
                }
            }
        }
    }
}