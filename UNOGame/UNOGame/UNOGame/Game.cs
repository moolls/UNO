public class Game 
{
private Deck deck;
private HumanPlayer humanPlayer;
private ComputerPlayer computerPlayer;
public PlayedCards playedCards;

public Game ()
{
Play();    
}

    public void Play()
    {
        Console.WriteLine("Welcome to UNO!");
        Console.WriteLine("What is your name?");
        string name = Console.ReadLine();
        Console.WriteLine($"Welcome {name}! You will play against a computer.");
        deck = new Deck();
        playedCards = new PlayedCards();
        humanPlayer = new HumanPlayer(name, deck); 
        computerPlayer = new ComputerPlayer(deck);
        Console.WriteLine("Choose your opponments intelligence:");
        string choice = Console.ReadLine();
        ICardChooser cardChooserHuman = new InteractiveCardChooser(deck, humanPlayer.hand, playedCards);
        ICardChooser cardChooserComputer = (new CardChooserFactory()).CreateCardChooser(choice, deck, computerPlayer.hand, playedCards);


       
        Console.WriteLine($"Let the game begin!");
        IUnoCard card = deck.DrawCard();
        Console.WriteLine();
        Console.WriteLine($"{name} starts!");
        Console.Write($"The game starts with: ");
        playedCards.AddCardToFront(card);
        card.DisplayCard();

       
       
        Console.WriteLine();
        Console.WriteLine($"{name}s hand:");
        humanPlayer.hand.ShowHand();
        playedCards.DisplayFirstCard();
     

        CardStrategy cardStrategy = new CardStrategy(deck, computerPlayer.hand, playedCards);
        cardStrategy.GetSpeciality();

        bool isWinner = false;



    

 while (!isWinner)
{
    // Kontrollera om datorn har kort kvar att spela
    if (!computerPlayer.hand.checkEmpty())
    {
         
        cardChooserHuman.ChooseCard();
    }
    else
    {
        isWinner = true;
        Console.WriteLine("Computer won!");
        break; // Avsluta loopen eftersom vi har en vinnare
    }

    // Kontrollera om människan har kort kvar att spela (efter datorns drag)
    if (!humanPlayer.hand.checkEmpty())
    {
        
        cardChooserComputer.ChooseCard();
    }
    else
    {
        isWinner = true;
        Console.WriteLine("Human won!");
        break; // Avsluta loopen eftersom vi har en vinnare
    }
}



}
}
