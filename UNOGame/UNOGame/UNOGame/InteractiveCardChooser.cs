public class InteractiveCardChooser : ICardChooser
{
    Deck deck;
    Hand hand;
    PlayedCards playedCards;
    IUnoCard choosenCard;
    int indexCurrentCard;
    int drawCounter = 0;

    bool maxCardsDrawn;
    
    public InteractiveCardChooser(Deck deck, Hand hand, PlayedCards playedCards)
    {
        this.hand = hand;
        this.playedCards = playedCards;
        this.deck = deck;
        indexCurrentCard = 0;

    }
    
    public void ChooseCard()
    {
      while (true)
        {
            // Visa senaste spelade kort
            Console.Write($"Last drawn card: ");
            playedCards.DisplayFirstCard();

            // Låt spelaren välja ett kort
            bool maxCardsDrawn;
            choosenCard = IterateHand(deck, out maxCardsDrawn);

            // Kontrollera om det valda kortet kan spelas
            if (CanPlayCard(choosenCard))
            {
                // Kortet kan spelas, bryt ut ur loopen och spela kortet
                
                Console.WriteLine($"You selected: {choosenCard.GetColor()} {choosenCard.GetValue()}");
                playedCards.AddCardToFront(choosenCard);
                hand.RemoveCard(choosenCard);
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


     public IUnoCard IterateHand(Deck deck, out bool maxCardsDrawn)
    {
        IEnumerator<IUnoCard> forwardIterator = InfiniteIterator().GetEnumerator();
        IEnumerator<IUnoCard> backwardIterator = InfiniteReverseIterator().GetEnumerator();

        bool usingForward = true;
        IUnoCard currentItem = null;
        ConsoleKey key;
        
        maxCardsDrawn = false; // Flagga för att hålla reda på om tre kort dragits

        do
        {
            
            hand.ShowHand(); // Visa nuvarande hand

            // Hämta aktuellt kort baserat på riktningen
            if (usingForward)
            {
                if (!forwardIterator.MoveNext())
                {
                    forwardIterator = InfiniteIterator().GetEnumerator(); // Återställ om vi når slutet
                    forwardIterator.MoveNext(); // Flytta till första kortet
                }
                currentItem = forwardIterator.Current;
            }
            else
            {
                if (!backwardIterator.MoveNext())
                {
                    backwardIterator = InfiniteReverseIterator().GetEnumerator(); // Återställ om vi når början
                    backwardIterator.MoveNext(); // Flytta till sista kortet
                }
                currentItem = backwardIterator.Current;
            }

            // Visa aktuellt kort och alternativen till spelaren
            Console.WriteLine($"Current item: {currentItem.GetColor()} {currentItem.GetValue()}");
            Console.WriteLine("Use Left/Right arrows to navigate, Enter to select item, or press D to draw a new card.");

            key = Console.ReadKey().Key;

            // Hantera navigering och kortdragning
            if (key == ConsoleKey.RightArrow)
            {
                usingForward = true; // Bläddra framåt
            }
            else if (key == ConsoleKey.LeftArrow)
            {
                usingForward = false; // Bläddra bakåt
            }
            else if (key == ConsoleKey.D)
            {
                if (drawCounter < 3)
                {
                    hand.DrawCardToHand(deck); // Dra ett kort och lägg till i handen
                    drawCounter++;
                    Console.WriteLine($"You drew a new card. Cards drawn: {drawCounter}/3");
                    //break;
                }
                else{
                    
                    maxCardsDrawn = true; // Flagga sätts till true när max kort har dragits
                }
            }

        } while (key != ConsoleKey.Enter && !maxCardsDrawn); // Avsluta när Enter trycks eller tre kort dragits
        
        return currentItem;
    }


   public bool CanPlayCard(IUnoCard card)
    {
    IUnoCard lastPlayedCard = playedCards.First();
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

 public IEnumerable<IUnoCard> InfiniteIterator()
    {
        while (true)  // Infinite loop
        {
            yield return hand.CurrentCard(indexCurrentCard);
            indexCurrentCard = (indexCurrentCard + 1) % hand.Count(); // Circular navigation
        }
    }

    public IEnumerable<IUnoCard> InfiniteReverseIterator()
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