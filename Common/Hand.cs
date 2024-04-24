using On20240313OOPCardGame.Interfaces;

namespace On20240313OOPCardGame;

public class Hand : IDealReceiver
{
    // Attributes
    private List<Card> _cards;
    
    // Methods
    // Constructor
    public Hand()
    {
        _cards = new List<Card>();
    }
    
    // Getters
    public int CountCards()
    {
        return _cards.Count;
    }

    public List<Card> GetCards()
    {
        return _cards;
    }

    public bool HasSuit(Suit suit)
    {
        foreach (Card card in _cards)
        {
            if (card.GetSuit() == suit)
            {
                return true;
            }
        }

        return false;
    }
    
    // Setters
    public void AddCard(Card card)
    {
        _cards.Add(card);
    }

    public Card TakeCardAtIndex(int index)
    {
        if (index >= 0 || index < _cards.Count)
        {
            Card card = _cards[index];
            _cards.RemoveAt(index);
            return card;
        }
        else
        {
            throw new IndexOutOfRangeException($"There is no card in this Hand at index {index}");
        }
    }
}