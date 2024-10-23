public class PlayedCards{

public List<IUnoCard> playedCards;

public PlayedCards(){
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
        IUnoCard firstCard = playedCards[0]; // Hämtar första kortet
        firstCard.DisplayCard(); // Visar det första kortet
    }
    else
    {
        Console.WriteLine("No cards have been played yet.");
    }
}

}

