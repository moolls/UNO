public class SmartCardChooser : ICardChooser
{
    private Deck deck;
    private PlayedCards playedCards;
    private IUnoCard choosenCard;
    private int drawCounter = 0;
    private bool maxCardsDrawn;

    public SmartCardChooser(Deck deck, PlayedCards playedCards)
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

            choosenCard = FindPlayableCard(player.hand);

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

    private void DisplayLastDrawnCard()
    {
        Console.Write($"Last drawn card: ");
        playedCards.DisplayFirstCard();
    }

    private IUnoCard FindPlayableCard(Hand hand)
    {
        IUnoCard lastPlayedCard = playedCards.First();
        choosenCard = null;

        foreach (var card in hand)
        {
            if (card.GetColor() == lastPlayedCard.GetColor() || card.GetValue() == lastPlayedCard.GetValue())
            {
                choosenCard = card; // Found a playable card
                return choosenCard;
            }
        }

        return null; // No playable card found
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

    private void PlayCard(Player player)
    {
        Console.WriteLine($"Robotic selected: {choosenCard.GetColor()} {choosenCard.GetValue()}");
        playedCards.AddCardToFront(choosenCard);
        player.hand.RemoveCard(choosenCard);
        ResetDraw();
    }

    public void ResetDraw()
    {
        drawCounter = 0;
    }
}

 