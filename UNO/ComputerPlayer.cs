public class ComputerPlayer : Player
{
    public ComputerPlayer(Deck deck, ICardChooser cardChooser) : base(GenerateName(), deck, cardChooser)
    {
    }

    public void Introduction()
    {
        if (cardChooser is SmartCardChooser)
        {
            Console.WriteLine($"I am {name}, your worst nightmare!!");
        }
        else if (cardChooser is StupidCardChooser)
        {
            Console.WriteLine($"I am {name}, I'll try my best and I just play cards randomly... Good luck!");
        }
        else Console.WriteLine($"I am {name}, lets play UNO! Good luck :)");
        
    }
    public override void ShowHand()
    {
        foreach (IUnoCard card in hand)
        {
            Console.Write("[] ");
        }
        Console.WriteLine();
    }

    public static string GenerateName()
    {
        var textHandler = new TextFileHandler("computer_names.txt");
        var readTextNames = textHandler.Read();
        var random = new Random();
        return readTextNames[random.Next(readTextNames.Count)];
    }

    
}
