public class InteractiveCardChooser : ICardChooser
{
    Deck deck;
    PlayedCards playedCards;
    IUnoCard choosenCard;
    int indexCurrentCard;
    
    
    public InteractiveCardChooser(Deck deck, PlayedCards playedCards)
    {
        this.playedCards = playedCards;
        this.deck = deck;
        indexCurrentCard = 0;

    }
    
    public void ChooseCard(Player player)
    {
        
      while (true)
        {
            // Visa senaste spelade kort
            Console.Write($"Last drawn card: ");
            playedCards.DisplayFirstCard();

            // Låt spelaren välja ett kort
            bool maxCardsDrawn;
            choosenCard = IterateHand(deck, player, out maxCardsDrawn);

            // Kontrollera om det valda kortet kan spelas
            if (playedCards.CanPlayCard(choosenCard))
            {
                // Kortet kan spelas, bryt ut ur loopen och spela kortet
                
                Console.WriteLine($"You selected: {choosenCard.GetColor()} {choosenCard.GetValue()}");
                playedCards.AddCardToFront(choosenCard);
                player.hand.RemoveCard(choosenCard);
                break;
            }
            else
            {
                // Kortet kan inte spelas, ge feedback till spelaren
                
                Console.WriteLine($"You cannot play {choosenCard.GetColor()} {choosenCard.GetValue()}. Try again or draw a card.");

                // Om spelaren har dragit tre kort och inte kan lägga något kort, växla till nästa spelare
                if (maxCardsDrawn)
                {
                    Console.WriteLine("You have drawn the maximum number of cards and cannot play. Next player's turn.");
                    break; // Byt till nästa spelare
                }
            }
        }
    }

public IUnoCard IterateHand(Deck deck, Player player, out bool maxCardsDrawn)
{
    IEnumerator<IUnoCard> forwardIterator = InfiniteIterator(player.hand).GetEnumerator();
    IEnumerator<IUnoCard> backwardIterator = InfiniteReverseIterator(player.hand).GetEnumerator();

    bool usingForward = true;
    IUnoCard currentItem = null;
    ConsoleKey key;
    int drawCounter = 0;
    bool invalidKeyMessageShown = false;

    maxCardsDrawn = false; 
    player.ShowHand();

    // Visa navigeringsinstruktioner en gång i början
    Console.WriteLine("Use Left/Right arrows to navigate, Enter to select item, or press D to draw a new card.");
    
    // Spara startpositionen för att skriva över aktuellt kort varje gång
    int cursorTop = Console.CursorTop;

    do
    {
        // Hämta aktuellt kort baserat på riktningen
        if (usingForward)
        {
            if (!forwardIterator.MoveNext())
            {
                forwardIterator = InfiniteIterator(player.hand).GetEnumerator();
                forwardIterator.MoveNext();
            }
            currentItem = forwardIterator.Current;
        }
        else
        {
            if (!backwardIterator.MoveNext())
            {
                backwardIterator = InfiniteReverseIterator(player.hand).GetEnumerator();
                backwardIterator.MoveNext();
            }
            currentItem = backwardIterator.Current;
        }

        // Gå till startpositionen och skriv över tidigare kortinformation
        Console.SetCursorPosition(0, cursorTop);
        Console.Write($"Current item: {currentItem.GetColor()} {currentItem.GetValue()}     "); // Extra blanksteg för att rensa eventuellt tidigare text

        // Läs tangenttryckning och kontrollera giltighet
        do
        {
            key = Console.ReadKey(true).Key;

            // Kontrollera om tangenten är ogiltig och visa meddelande bara en gång
            if (key != ConsoleKey.RightArrow && key != ConsoleKey.LeftArrow && key != ConsoleKey.Enter && key != ConsoleKey.D)
            {
                if (!invalidKeyMessageShown)
                {
                    Console.WriteLine("Invalid key. Please use Left/Right arrows, Enter, or D.");
                    invalidKeyMessageShown = true;
                }
            }
            else
            {
                invalidKeyMessageShown = false; // Återställ flaggan om en giltig tangent trycks ned
            }

        } while (key != ConsoleKey.RightArrow && key != ConsoleKey.LeftArrow && key != ConsoleKey.Enter && key != ConsoleKey.D);

        // Hantera navigering och kortdragning
        if (key == ConsoleKey.RightArrow)
        {
            usingForward = true;
        }
        else if (key == ConsoleKey.LeftArrow)
        {
            usingForward = false;
        }
        else if (key == ConsoleKey.D)
        {
            if (drawCounter < 3)
            {
                player.hand.DrawCardToHand(deck);
                drawCounter++;
                Console.WriteLine($"You drew a new card. Cards drawn: {drawCounter}/3");
                player.ShowHand();
            }
            else
            {
                maxCardsDrawn = true;
                Console.WriteLine("You have reached the maximum number of draws.");
            }
        }

    } while (key != ConsoleKey.Enter && !maxCardsDrawn);

    return currentItem;
}

 public IEnumerable<IUnoCard> InfiniteIterator(Hand hand)
    {
        while (true)  // Infinite loop
        {
            
            indexCurrentCard = (indexCurrentCard + 1) % hand.Count(); // Circular navigation
            yield return hand.CurrentCard(indexCurrentCard);
        }
    }

    public IEnumerable<IUnoCard> InfiniteReverseIterator(Hand hand)
    {
        while (true)
        {
            indexCurrentCard = (indexCurrentCard - 1 + hand.Count()) % hand.Count(); // Circular navigation backwards
            yield return hand.CurrentCard(indexCurrentCard);
        }
    }

    public bool HasMaxDrawnCards(int drawCounter)
    {
        return drawCounter >= 3;
    }

}