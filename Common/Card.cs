namespace On20240313OOPCardGame;

public class Card
{
    //  Attributes
    private Suit _suit;
    private CardValue _value;
    
    // Methods
    // Constructor
    public Card(Suit suit, CardValue value)
    {
        _suit = suit;
        _value = value;
    }
    
    // String Represntation
    public new string ToString()
    {
        return $"{_value.ToString()} of {_suit.ToString()}";
    }
    public new string ToTabbedString()
    {
        return $"{_value.ToString()}\tof\t{_suit.ToString()}";
    }
    public new string ToShortString()
    {
        string shortValue;
        string shortSuit;
        switch (_value)
        {
            case CardValue.Two:
                shortValue = "2";
                break;
            case CardValue.Three:
                shortValue = "3";
                break;
            case CardValue.Four:
                shortValue = "4";
                break;
            case CardValue.Five:
                shortValue = "5";
                break;
            case CardValue.Six:
                shortValue = "6";
                break;
            case CardValue.Seven:
                shortValue = "6";
                break;
            case CardValue.Eight:
                shortValue = "8";
                break;
            case CardValue.Nine:
                shortValue = "9";
                break;
            case CardValue.Ten:
                shortValue = "10";
                break;
            case CardValue.Jack:
                shortValue = "J";
                break;
            case CardValue.Queen:
                shortValue = "Q";
                break;
            case CardValue.King:
                shortValue = "K";
                break;
            case CardValue.Ace:
                shortValue = "A";
                break;
            default:
                shortValue = "⚠";
                break;
        }
        switch (_suit)
        {
            case Suit.Clubs:
                shortSuit = "♣";
                break;
            case Suit.Diamonds:
                shortSuit = "♦";
                break;
            case Suit.Hearts:
                shortSuit = "♥";
                break;
            case Suit.Spades:
                shortSuit = "♠";
                break;
            default:
                shortSuit = "⚠";
                break;
        }

        return shortValue + shortSuit;
    }
    
    // Getters
    public Suit GetSuit()
    {
        return _suit;
    }
    
    public CardValue GetValue()
    {
        return _value;
    }
    
    // Setters
}