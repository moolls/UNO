// See https://aka.ms/new-console-template for more information

public class ComputerPlayer : Player
{
     public ComputerPlayer(Deck deck, ICardChooser cardChooser) : base("Robotic", deck, cardChooser){ //ta emot en cardchooser som spelare väljer under runtime. 
     //Under runtime väljer man idén av vad en "computer" ska vara dvs smart eller stupid

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
