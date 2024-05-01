using System;
using On20240313OOPCardGame.Trump;

namespace On20240313OOPCardGame;

public class Program
{
    public static void Main()
    {
        // This is an implementation of the Trump card game via inheritance and polymorphism. The classes in "Abstract"
        // can be extended and used alongside the "Common" classes to add another card game, like they are in "Trump".

        bool anotherGame = true;

        while (anotherGame)
        {
            List<TrumpPlayer> players = new List<TrumpPlayer>();

            // Get players
            Console.WriteLine("Enter all player names on separate lines, and leave a blank line at the end.");
            string nextPlayerName = Console.ReadLine();
            while (nextPlayerName.Length > 0)
            {
                players.Add(new TrumpPlayer(nextPlayerName));
                nextPlayerName = Console.ReadLine();
            }

            if (players.Count <= 1)
            {
                Console.WriteLine("You must have at least 2 players.");
                continue;
            }
            if (players.Count > 52)
            {
                Console.WriteLine("There aren't enough cards in the deck of 52 to even play 1 card per player.");
                continue;
            }
            
            int numTricks = 52 / players.Count;
            if (numTricks > 7)
            {
                numTricks = 7;
            }
            
            TrumpGame game = new TrumpGame(players, numTricks);
            game.Play();

            Console.Write("Would you like to play again? (Y)es/(N)o ");
            anotherGame = Console.ReadLine().Trim().ToUpper() == "Y";
        }
    }
}