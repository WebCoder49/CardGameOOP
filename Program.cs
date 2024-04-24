using System;
using On20240313OOPCardGame.Trump;

namespace On20240313OOPCardGame;

public class Program
{
    public static void Main()
    {
        TrumpGame game = new TrumpGame(new List<TrumpPlayer>{new TrumpPlayer("Alice"), new TrumpPlayer("Bob"), new TrumpPlayer("Clare"), new TrumpPlayer("Douglas")}, 7);
        // TODO Finish Polymorphism to make work
        // game.Play();
    }
}