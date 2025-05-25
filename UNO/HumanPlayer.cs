public class HumanPlayer : Player
{

    public HumanPlayer(string name, Deck deck, ICardChooser cardChooser) : base(name, deck, cardChooser)
    {
    }

    public override void ShowHand()
    {
        Console.WriteLine($"{name}s hand");
    }

    //REFERENS: 
    //Vi fick inspiration från ChatGPT kring denna idé och funktionalitet för att utöka HumanPlayer klassen och urskilja den mer från ComputerPlayer
    public void GetHint(PlayedCards playedCards)
    {
        bool playableHint = false;
        IUnoCard topCard = playedCards.First();
        foreach (var card in hand)
        {
            if (card.GetColor() == topCard.GetColor() || card.GetValue() == topCard.GetValue())
            {
                Console.WriteLine($"Hint: Play {card.GetColor()} {card.GetValue()}");
                playableHint = true;
                break;
            }

        }
        if (!playableHint)
        {
            Console.WriteLine("Hint: You need to draw a new card");
        }
    }

}
