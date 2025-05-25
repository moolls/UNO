public class SmartCardChooser : ICardChooser
{

    Deck deck;
    PlayedCards playedCards;
    IUnoCard choosenCard;
    int drawCounter = 0;

    bool maxCardsDrawn;

    public SmartCardChooser(Deck deck, PlayedCards playedCards)
    {
        this.playedCards = playedCards;
        this.deck = deck;

    }

    //REFERENS: 
    //ChatGPT har hjälpt oss med att skapa logiken för en spelrunda så att villkor tillämpas på rätt ställen.
    public void ChooseCard(Player player)
    {
        while (true)
        {
            Thread.Sleep(5000);
            DisplayLastDrawnCard();
            player.ShowHand();

            choosenCard = FindPlayableCard(player.hand);

            if (playedCards.CanPlayCard(choosenCard))
            {
                PlayCard(player);
                break;

            }
            else if (CanDrawCard())
            {

                Console.WriteLine($"{player.name} could not play ");
                Thread.Sleep(1000);
                Console.WriteLine("He tries again");
                DrawCard(player);
                continue;

            }
            else if (maxCardsDrawn)
            {
                Console.WriteLine($"{player.name}  has drawn the maximum number of cards.");
                Console.WriteLine("Next players turn!");
                break;
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
        Console.WriteLine($"{player.name} selected: ");
        choosenCard.DisplayCard();
        playedCards.AddCardToFront(choosenCard);
        player.hand.RemoveCard(choosenCard);
        ResetDraw();
    }

    private void DrawCard(Player player)
    {
        player.hand.DrawCardToHand(deck);
        drawCounter++;
        Console.WriteLine($"{player.name} drew a new card. Cards drawn: {drawCounter}/3");

    }

    private bool CanDrawCard()
    {
        if (drawCounter < 3)
        {
            return true;

        }
        else
        {
            maxCardsDrawn = true;
            return false;
        }
    }

    public void ResetDraw()
    {
        drawCounter = 0;
    }

    private IUnoCard FindPlayableCard(Hand hand)
    {
        IUnoCard lastPlayedCard = playedCards.First();

        foreach (var card in hand)
        {
            if (card.GetColor() == lastPlayedCard.GetColor() || card.GetValue() == lastPlayedCard.GetValue())
            {
                choosenCard = card;
                return choosenCard;
            }
        }

        return hand.ReturnCard();
    }
}