public class ComputerPlayer : Player
{
     public ComputerPlayer(Deck deck, ICardChooser cardChooser) : base("Robotic", deck, cardChooser){ 
    }


    public override void ShowHand()
    {
        foreach(IUnoCard card in hand)
        {
            Console.Write("[] ");
        }
        Console.WriteLine();
    }
}
