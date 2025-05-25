public class InteractiveCardChooser : ICardChooser
{
    private readonly Deck deck;
    private PlayedCards playedCards;
    private int drawCounter = 0;
    private bool maxDrawReached = false;

    public IUnoCard currentCard;
    public InteractiveCardChooser(Deck deck, PlayedCards playedCards)
    {
        this.deck = deck;
        this.playedCards = playedCards;
    }

    public void ChooseCard(Player player)
    {

        InfiniteIterator<IUnoCard> iterator = new InfiniteIterator<IUnoCard>(player.hand);

        currentCard = iterator.Current;



        DisplayHand(currentCard, player);


        //REFERENS: 
        //ChatGPT har hjälpt oss med att få till rätt logik med villkoren samt rätt logik med readKey.
        while (true)
        {

            ConsoleKey key = Console.ReadKey(intercept: true).Key;

            switch (key)
            {
                case ConsoleKey.RightArrow:
                    iterator.MoveNext();
                    Console.Write(" ");
                    DisplayHand(iterator.Current, player);

                    break;

                case ConsoleKey.LeftArrow:
                    iterator.MovePrevious();
                    Console.Write(" ");
                    DisplayHand(iterator.Current, player);
                    break;

                case ConsoleKey.D:
                    if (TryDrawCard(player, ref iterator))
                    {
                        Console.WriteLine($"You drew a new card. Cards drawn: {drawCounter}/3");
                        DisplayHand(iterator.Current, player);
                    }
                    else
                    {
                        Console.WriteLine("Max draw attempts reached for this turn.");
                    }
                    break;

                case ConsoleKey.Enter:
                    IUnoCard chosenCard = iterator.Current;
                    if (playedCards.CanPlayCard(chosenCard))
                    {
                        PlayCard(player, chosenCard);
                        playedCards.AddCardToFront(chosenCard);
                        Console.WriteLine("Card played successfully.");
                        ResetDrawCounter();
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Card cannot be played. Choose a different card.");
                        continue;
                    }

            }

            if (key == ConsoleKey.Enter || maxDrawReached)
            {
                ResetDrawCounter();
                break;
            }
        }
    }

    //REFERENS: 
    //ChatGPT har hjälpt oss skapa funktionalitet för att representera kortet med rätt färg i konsolen.
    public void DisplayHand(IUnoCard currentCard, Player player)
    {
        Console.Clear();
        Console.WriteLine("Last drawn card");
        playedCards.DisplayFirstCard();
        Console.WriteLine();
        Console.WriteLine();

        player.hand.SortHand();
        player.ShowHand();

        for (int i = 0; i < player.hand.Count(); i++)
        {
            IUnoCard card = player.hand.CurrentCard(i);

            if (card.GetColor() == CardColor.Red)
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }
            else if (card.GetColor() == CardColor.Green)
            {
                Console.BackgroundColor = ConsoleColor.Green;
            }
            else if (card.GetColor() == CardColor.Blue)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
            }
            else if (card.GetColor() == CardColor.Yellow)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Black;
            }

            if (card == currentCard)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write($">>{card.GetColor()} {card.GetValue()}<<");
            }
            else
            {
                Console.Write($"  {card.GetColor()} {card.GetValue()}  ");
            }
            Console.ResetColor();
        }

        Console.WriteLine();
        Console.WriteLine("Use -> and <- to scroll through your cards");
        Console.WriteLine("Press D to draw a card.");
        Console.WriteLine("Press ENTER to play card");
    }


    private bool TryDrawCard(Player player, ref InfiniteIterator<IUnoCard> iterator)
    {
        if (drawCounter < 3)
        {
            player.hand.DrawCardToHand(deck);
            drawCounter++;

            iterator = new InfiniteIterator<IUnoCard>(player.hand);
            iterator.MovePrevious();

            return true;
        }
        else
        {
            maxDrawReached = true;
            return false;
        }
    }

    private void PlayCard(Player player, IUnoCard chosenCard)
    {
        Console.WriteLine($"Player selected: ");
        chosenCard.DisplayCard();
        playedCards.AddCardToFront(chosenCard);
        player.hand.RemoveCard(chosenCard);
        ResetDrawCounter();
    }

    private void ResetDrawCounter()
    {
        drawCounter = 0;
        maxDrawReached = false;
    }
}