public class Game
{
    private Deck deck;
    private HumanPlayer humanPlayer;
    private ComputerPlayer computerPlayer;
    public PlayedCards playedCards;

    public Game()
    {
        Play();
    }

    public void Play()
    {

        // Initialisation of game
        Console.WriteLine("Welcome to UNO!");
        Deck deck = new Deck();
        PlayedCards playedCards = new PlayedCards();
        
        
        // Creation of player
        Console.WriteLine("What is your name?");
        string name = Console.ReadLine();
        Console.WriteLine($"Welcome {name}! You will play against a computer.");
        Player humanPlayer = new HumanPlayer(name, deck, new InteractiveCardChooser(deck, playedCards));

        // Creation of computer
        Console.WriteLine("Choose your opponent's intelligence:");
        string choice = Console.ReadLine();
        ICardChooser cardChooserComputer = (new CardChooserFactory()).CreateCardChooser(choice, deck, playedCards);
        Player computerPlayer = new ComputerPlayer(deck, cardChooserComputer);

        // en annan sak som christopher sa i hans video är att man bör göra så sättet man hämtar ett värde bör inte vara så mycket annorlunda från att kalla på ehm metod
        // så typ att man skapar metoder som returnerar det värde man vill komma åt som t.ex hand som vi använder många gånger i vårt fall 


        Console.WriteLine($"Let the game begin!");
        IUnoCard firstCard = deck.DrawCard();
        Console.WriteLine();
        Console.WriteLine($"{name} starts!");
        playedCards.AddCardToFront(firstCard);
        Console.WriteLine();


        bool isWinner = false;
        bool skipTurn = false;
        
        while (!isWinner)
        {
            // Tur för människan
            if (!skipTurn)
            {
                Console.WriteLine($"{name}'s turn:");
                humanPlayer.ShowHand();
                humanPlayer.cardChooser.ChooseCard(humanPlayer);
            }
            else
            {
                Console.WriteLine($"{name}'s turn is skipped.");
                skipTurn = false; // Återställ
                continue;
            }

            // Kontrollera om människan har vunnit
            if (humanPlayer.hand.checkEmpty())
            {
                Console.WriteLine($"{name} won!");
                break;
            }

            // Anropa CardStrategy för att kontrollera om specialkort spelades
            var lastCard = playedCards.First();
            var humanStrategy = new CardStrategy(deck, computerPlayer.hand, playedCards);
            humanStrategy.ApplySpecialEffect(lastCard, ref skipTurn);

            // Tur för datorn om människan inte vann
            if (!skipTurn && !isWinner)
            {
                Console.WriteLine("Computer's turn:");
                 computerPlayer.cardChooser.ChooseCard(computerPlayer);
            }
            else
            {
                Console.WriteLine("Computer's turn is skipped.");
                skipTurn = false; // Återställ
                continue;
            }

            // Kontrollera om datorn har vunnit
            if (computerPlayer.hand.checkEmpty())
            {
                Console.WriteLine("Computer won!");
                isWinner = true;
            }

            // Anropa CardStrategy igen för att kontrollera om specialkort spelades
            lastCard = playedCards.First();
            var computerStrategy = new CardStrategy(deck, humanPlayer.hand, playedCards);
            computerStrategy.ApplySpecialEffect(lastCard, ref skipTurn);
        }
    }
}
