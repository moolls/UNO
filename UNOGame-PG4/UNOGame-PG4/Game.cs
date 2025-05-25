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
        Console.WriteLine("Welcome to UNO!");
        deck = new Deck();
        playedCards = new PlayedCards();
        
        Console.WriteLine("What is your name?");
        string name = Console.ReadLine();
        if (name == "") 
        {
            name = "Human player";
        }
        Console.WriteLine($"Welcome {name}! You will play against a computer.");
        humanPlayer = new HumanPlayer(name, deck, new InteractiveCardChooser(deck, playedCards));

        List<string> difficultyLevels = new List<string> { "Stupid", "Smart" };
        InfiniteIterator<string> difficultyIterator = new InfiniteIterator<string>(difficultyLevels);
 
        bool optionBool = true;
        while (optionBool)
        {
            //REFERENS: 
             //ChatGPT har hjälpt oss med att få till rätt logik med villkoren samt rätt logik med readKey.
            DisplayOptions(difficultyLevels, difficultyIterator.Current);
            ConsoleKey choice = Console.ReadKey(intercept: true).Key;
              
            switch (choice)
            {
                case ConsoleKey.RightArrow:
                    difficultyIterator.MoveNext();
                    Console.Write($"{difficultyIterator.Current}");
                    break;

                case ConsoleKey.LeftArrow:
                    difficultyIterator.MovePrevious();
                    Console.Write($"{difficultyIterator.Current}");
                    break;

                case ConsoleKey.Enter:
                    string option = difficultyIterator.Current;
                    //KRAV 2
                    //1: Strategy pattern
                    //2: Strategy patterns används genom att användaren får välja vilken CardChooser strategi som datorn ska ha. Under run-time så får användaren 
                    //ta beslut kring vilken ICardChooser strategi som ska injiceras vid skapandet av datorspelaren. Själva skapandet av datorspelaren görs i CardChooserFactory
                    // som har olika cases som beror på vad användaren väljer som i sin tur i leder till instansieringen av ICardChooser konkretion.
                    //3. Abstrakta klassen ICardChooser implementerar olika konkreta klasser för strategier som en spelare kan ha. Dessa startegier
                    // skiljer sig i implemenation. InteractiveCardChooser tilldelas till människan medan SmartCardChooser och StupidCardChooser kan väljas under
                    // runtime av användaren och tilldelas till datorn vid skapande. Abstrakta klassen Player har något av typen ICardChooser som sätts vid skapande
                    // av spelare. Abstrakta klassen Player har i sin tur två konkreta subklasser HumanPlayer och ComputerPlayer
                    ICardChooser cardChooserComputer = (new CardChooserFactory()).CreateCardChooser(option, deck, playedCards);
                    computerPlayer = new ComputerPlayer(deck, cardChooserComputer);
                    optionBool = false;
                    break;
            }
        }

        Console.WriteLine("Let the game begin!");
        IUnoCard firstCard = deck.DrawCard();
        Console.WriteLine($"{name} starts!");
        playedCards.AddCardToFront(firstCard);
        Console.WriteLine();

        bool isWinner = false;
        bool skipTurn = false;
        bool humanTurn = true; 

   //REFERENS: 
   //ChatGPT har hjälpt oss med att skapa logiken för en spelrunda så att villkor tillämpas på rätt ställen.
    while (!isWinner)
    {
        if (humanTurn)
        {
            if (!skipTurn)
            {
                Console.WriteLine($"{name}'s turn:");
                humanPlayer.GetHint(playedCards);
                Thread.Sleep(3000);
                humanPlayer.cardChooser.ChooseCard(humanPlayer);

                if (humanPlayer.hand.checkEmpty())
                {
                    Console.WriteLine($"{name} won!");
                    break;
                }

                IUnoCard lastCard = playedCards.First();
                CardStrategy humanStrategy = new CardStrategy(deck, computerPlayer.hand, playedCards);
                humanStrategy.ApplySpecialEffect(lastCard, ref skipTurn);
            }
            else
            {
                Console.WriteLine($"{name}'s turn is skipped.");
                skipTurn = false;
            }
        }
        else
        {
            if (!skipTurn)
            {
                Thread.Sleep(3000);
                Console.Clear();
                Console.WriteLine($"{computerPlayer.name}  turn:");
                computerPlayer.cardChooser.ChooseCard(computerPlayer);

                if (computerPlayer.hand.checkEmpty())
                {
                    Console.WriteLine($"{computerPlayer.name} won!");
                    isWinner = true;
                    break;
                }

                IUnoCard lastCard = playedCards.First();
                CardStrategy computerStrategy = new CardStrategy(deck, humanPlayer.hand, playedCards);
                computerStrategy.ApplySpecialEffect(lastCard, ref skipTurn);
            }
            else
            {
                Console.WriteLine($"{computerPlayer.name} turn is skipped.");
                skipTurn = false;
            }
        }

        humanTurn = !humanTurn;
    }
}

    static void DisplayOptions(List<string> options, string currentOption)
    {
        Console.Clear(); 
        Console.WriteLine("Choose your opponent's intelligence:");

        for (int i = 0; i < options.Count; i++)
        {
            if (options[i] == currentOption)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            Console.Write(options[i]); Console.Write("   ");
            Console.ResetColor();
        }
        Console.SetCursorPosition(0, 0);
    }
}


