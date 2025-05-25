public class PlayedCards
{

    public List<IUnoCard> playedCards;

    public PlayedCards()
    {
        playedCards = new List<IUnoCard>();
    }

    public IUnoCard First()
    {
        return playedCards.First();
    }

    public void AddCardToFront(IUnoCard choosenCard)
    {
        playedCards.Insert(0, choosenCard);
    }
    public void DisplayFirstCard()
    {

        if (playedCards.Count > 0)
        {
            IUnoCard firstCard = playedCards[0];

            firstCard.DisplayCard();
        }
        else
        {
            Console.WriteLine("No cards have been played yet.");
        }
    }

    public bool CanPlayCard(IUnoCard card)
    {
        IUnoCard lastPlayedCard = playedCards.First();
        CardColor LastCardColor = lastPlayedCard.GetColor();
        CardColor playedCardColor = card.GetColor();

        CardValue LastCardValue = lastPlayedCard.GetValue();
        CardValue playedCardValue = card.GetValue();


        if (LastCardColor == playedCardColor)
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