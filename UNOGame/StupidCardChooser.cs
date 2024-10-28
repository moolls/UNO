public class StupidCardChooser : ICardChooser
{

    Deck deck;
    PlayedCards playedCards;
    IUnoCard choosenCard;
    int indexCurrentCard;
    int drawCounter = 0;

    bool maxCardsDrawn;
    
    public StupidCardChooser(Deck deck, PlayedCards playedCards)
    {
        this.playedCards = playedCards;
        this.deck = deck;

    }
    public void ChooseCard(Player player)
    {
        while (true)
        {
            Thread.Sleep(5000);
            DisplayLastDrawnCard();
            player.ShowHand();

            choosenCard = RandomCard(player.hand);

            if (choosenCard == null)
            {
                if (!TryDrawCard(player))
                {
                    Console.WriteLine("Robotic has drawn the maximum number of cards and cannot play. Next player's turn.");
                    ResetDraw();
                    break; // Switch to the next player
                }
            }
            else
            {
                PlayCard(player);
                break; // Exit loop after playing a valid card
            }
        }
    }

public IUnoCard RandomCard(Hand hand)
{
    Random random = new Random();
    int randomCardIndex = random.Next(0, hand.Count());
    return hand.CurrentCard(randomCardIndex);
}

private void DisplayLastDrawnCard()
    {
        Console.Write($"Last drawn card: ");
        playedCards.DisplayFirstCard();
    }

private void PlayCard(Player player)
    {
        Console.WriteLine($"Robotic selected: {choosenCard.GetColor()} {choosenCard.GetValue()}");
        playedCards.AddCardToFront(choosenCard);
        player.hand.RemoveCard(choosenCard);
        ResetDraw();
    }

private bool TryDrawCard(Player player)
    {
        if (drawCounter < 3)
        {
            player.hand.DrawCardToHand(deck); // Draw a card and add it to the hand
            drawCounter++;
            Console.WriteLine($"Robotic drew a new card. Cards drawn: {drawCounter}/3");
            return true; // Card was drawn
        }
        else
        {
            maxCardsDrawn = true; // Set flag when maximum cards have been drawn
            return false; // Cannot draw a card
        }
    }

    public void ResetDraw()
    {
        drawCounter = 0;
    }
}
