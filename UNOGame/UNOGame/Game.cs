public class Game 
{
private Deck deck;
private HumanPlayer humanPlayer;
private ComputerPlayer computerPlayer;

public List<IUnoCard> playedCards = new List<IUnoCard>();

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
    //choose your difficulty
    deck = new Deck();
    humanPlayer = new HumanPlayer(name, deck); // Kontrollera så att name inte är tom eller tilldela default name
    computerPlayer = new ComputerPlayer(deck);
    Console.WriteLine($"Let the game begin!");
    
 
    // Console.WriteLine($"Computers hand:");
    // computerPlayer.ShowHand();

   Console.WriteLine($"First card");
   IUnoCard card = deck.DrawCard();
    playedCards.Add(card);
    

    Console.WriteLine($"{name} starts");
    Console.WriteLine($"{name}s hand:");
    humanPlayer.hand.ShowHand();
    
    Hand hand = humanPlayer.hand;
    IUnoCard choosenCard;

    
    while (true)
    {  
    
    // Låt användaren välja ett kort
    Console.Write($"Last drawn card: ");
    DisplayFirstCard();
    choosenCard = hand.IterateHand(deck);

    // Kontrollera om kortet kan spelas
    if (CanPlayCard(choosenCard))
    {
        // Kortet kan spelas, bryt ut ur loopen
          Console.Clear();
          Console.WriteLine($"You selected: {choosenCard.GetColor()} {choosenCard.GetValue()}");
          
        break;
    }
    else
    {
        Console.Clear();
        // Kortet kan inte spelas, ge feedback och fortsätt loopen
        Console.WriteLine($"You can not play {choosenCard.GetColor()} {choosenCard.GetValue()}. Try again.");
        
    }
}

// Nu har ett spelbart kort valts, fortsätt med spelets logik
Console.WriteLine("Your current hand: ");
hand.RemoveCard(choosenCard); // Ta bort kortet från handen
AddCardToFront(choosenCard);  // Lägg till kortet i början av spelade kort
Console.Write("Last played card: ");
DisplayFirstCard(); // Visa det senaste spelade kortet

}

public void AddCardToFront(IUnoCard choosenCard)
    {
        playedCards.Insert(0, choosenCard);
    }

public void DisplayFirstCard()
{
    if (playedCards.Count > 0)
    {
        IUnoCard firstCard = playedCards[0]; // Hämtar första kortet
        firstCard.DisplayCard(); // Visar det första kortet
    }
    else
    {
        Console.WriteLine("No cards have been played yet.");
    }
}

public bool CanPlayCard(IUnoCard card)
{
    IUnoCard lastPlayedCard = playedCards.Last();
    CardColor LastCardColor = lastPlayedCard.GetColor();
    CardColor playedCardColor = card.GetColor();

    CardValue LastCardValue = lastPlayedCard.GetValue();
    CardValue playedCardValue = card.GetValue();


    if(LastCardColor == playedCardColor)
    {
        return true;
    }

    if (LastCardValue == playedCardValue)
    {
        return true;
    }

    return false;
}




}