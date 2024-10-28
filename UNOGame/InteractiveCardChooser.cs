public class InteractiveCardChooser : ICardChooser
{
    private Deck deck;
    private PlayedCards playedCards;
    private int indexCurrentCard;
    private int drawCounter = 0;
    private bool maxDrawReached = false; // Flag to indicate max draws reached

    public InteractiveCardChooser(Deck deck, PlayedCards playedCards)
    {
        this.playedCards = playedCards;
        this.deck = deck;
        indexCurrentCard = 0;
    }

    public void ChooseCard(Player player)
    {
        Console.WriteLine("Select a card to play.");
        bool shouldDrawCard = false;
        IUnoCard chosenCard;

        while (true)
        {
            Console.Write("Last drawn card: ");
            playedCards.DisplayFirstCard();

            // Use IterateHand to select a card or detect a draw attempt
            chosenCard = IterateHand(player, out shouldDrawCard);

            // Check if the chosen card can be played
            if (playedCards.CanPlayCard(chosenCard))
            {
                Console.WriteLine($"You selected: {chosenCard.GetColor()} {chosenCard.GetValue()}");
                playedCards.AddCardToFront(chosenCard);
                player.hand.RemoveCard(chosenCard);
                ResetDrawCounter();
                break; // Exit the loop after playing a valid card
            }

            // Handle card drawing, only if max draws haven't been reached
            if (shouldDrawCard && !maxDrawReached)
            {
                DrawCard(player);
                
                // If maximum number of draws has been reached, set the flag
                if (drawCounter >= 3)
                {
                    maxDrawReached = true;
                    Console.WriteLine("You have reached the maximum number of draws.");
                }
                continue; // Restart the loop after drawing a card
            }

            // Inform the player if the chosen card cannot be played
            Console.WriteLine($"You cannot play {chosenCard.GetColor()} {chosenCard.GetValue()}. Try again or draw a card.");
        }
    }

    public IUnoCard IterateHand(Player player, out bool shouldDrawCard)
    {
        IEnumerator<IUnoCard> forwardIterator = InfiniteIterator(player.hand).GetEnumerator();
        IEnumerator<IUnoCard> backwardIterator = InfiniteReverseIterator(player.hand).GetEnumerator();
        bool usingForward = true;
        IUnoCard currentItem = null;
        shouldDrawCard = false;
        bool invalidKeyMessageShown = false;

        player.ShowHand();
        do
        {
            currentItem = NavigateHand(forwardIterator, backwardIterator, usingForward);
            player.ShowHand();
            DisplayCardOptions(currentItem);
            ConsoleKey key = GetValidKey(ref invalidKeyMessageShown);

            usingForward = GetNavigationDirection(key, ref shouldDrawCard);


            if (key == ConsoleKey.Enter || shouldDrawCard) break;
        } while (true);

        return currentItem;
    }

    private IUnoCard NavigateHand(IEnumerator<IUnoCard> forwardIterator, IEnumerator<IUnoCard> backwardIterator, bool usingForward)
    {
        if (usingForward) forwardIterator.MoveNext();
        else backwardIterator.MoveNext();
        return usingForward ? forwardIterator.Current : backwardIterator.Current;
    }

    private void DisplayCardOptions(IUnoCard currentItem)
    {
        Console.WriteLine($"Current item: {currentItem.GetColor()} {currentItem.GetValue()}");
        Console.WriteLine("Use Left/Right arrows to navigate, Enter to select item, or press D to draw a new card.");
    }

    private ConsoleKey GetValidKey(ref bool invalidKeyMessageShown)
    {
        ConsoleKey key;
        do
        {
            key = Console.ReadKey(true).Key;
            if (!IsNavigationKey(key))
            {
                if (!invalidKeyMessageShown)
                {
                    Console.WriteLine("Invalid key. Please use Left/Right arrows, Enter, or D.");
                    invalidKeyMessageShown = true;
                }
            }
            else invalidKeyMessageShown = false;
        } while (!IsNavigationKey(key));

        return key;
    }

    private bool GetNavigationDirection(ConsoleKey key, ref bool shouldDrawCard)
    {
        switch (key)
        {
            case ConsoleKey.RightArrow: return true;
            case ConsoleKey.LeftArrow: return false;
            default: return true;
        }
    }

    private void DrawCard(Player player)
    {
        if (drawCounter < 3)
        {
            player.hand.DrawCardToHand(deck);
            drawCounter++;
            Console.WriteLine($"You drew a new card. Cards drawn: {drawCounter}/3");
        }
    }

    private bool IsNavigationKey(ConsoleKey key) =>
        key == ConsoleKey.RightArrow || key == ConsoleKey.LeftArrow || key == ConsoleKey.Enter || key == ConsoleKey.D;

    public IEnumerable<IUnoCard> InfiniteIterator(Hand hand)
    {
        while (true)
        {
            indexCurrentCard = (indexCurrentCard + 1) % hand.Count();
            yield return hand.CurrentCard(indexCurrentCard);
        }
    }

    public IEnumerable<IUnoCard> InfiniteReverseIterator(Hand hand)
    {
        while (true)
        {
            indexCurrentCard = (indexCurrentCard - 1 + hand.Count()) % hand.Count();
            yield return hand.CurrentCard(indexCurrentCard);
        }
    }

    private void ResetDrawCounter()
    {
        drawCounter = 0;
        maxDrawReached = false;
    }
}
