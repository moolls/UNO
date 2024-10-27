public class SmartCardChooser : ICardChooser
{

    Deck deck;
    Hand hand;
    PlayedCards playedCards;
    IUnoCard choosenCard;
    int indexCurrentCard;
    int drawCounter = 0;

    bool maxCardsDrawn;
    
    public SmartCardChooser(Deck deck, Hand hand, PlayedCards playedCards)
    {
        this.hand = hand;
        this.playedCards = playedCards;
        this.deck = deck;

    }
 public void ChooseCard()
 {
     while(true)
    {
            Thread.Sleep(5000);
            Console.Write($"Last drawn card: ");
            playedCards.DisplayFirstCard();

            Console.WriteLine("Robotics hand: ");
            hand.ShowAnonym();

            // Låt spelaren välja ett kort
            bool maxCardsDrawn =false;
            choosenCard = FindCard();

            // Kontrollera om det valda kortet kan spelas
            if(choosenCard == null)
            {
                // Finns inte ett enda kort med samma färg ell
                
                Console.WriteLine("Did not have a matching card");
                if (drawCounter < 3)
                {
                    hand.DrawCardToHand(deck); // Dra ett kort och lägg till i handen
                    drawCounter++;
                    Console.WriteLine($"Robotic drew a new card. Cards drawn: {drawCounter}/3");
                    
                }
                else {
                    
                    maxCardsDrawn = true; // Flagga sätts till true när max kort har dragits
                }

                if (maxCardsDrawn)
                {
                Console.WriteLine("Robotic have drawn the maximum number of cards and cannot play. Next player's turn.");
                break; // Byt till nästa spelare
                }
            }
            else if (CanPlayCard(choosenCard))
            {
                // Kortet kan spelas, bryt ut ur loopen och spela kortet
                Console.WriteLine($"Robotic selected: {choosenCard.GetColor()} {choosenCard.GetValue()}");
                playedCards.AddCardToFront(choosenCard);
                hand.RemoveCard(choosenCard);
                break;
            }
        
        }
               
     }

public IUnoCard FindCard()
{
    IUnoCard lastPlayedCard = playedCards.First(); // Första kortet från spelade kort
  
        choosenCard = null; // Nollställ vald kort

    foreach (var card in hand)
    {
        // Kontrollera om kortet matchar färgen eller värdet, eller om båda är specialkort
        if (card.GetColor() == lastPlayedCard.GetColor() || card.GetValue() == lastPlayedCard.GetValue())
        {
            choosenCard = card; // Hitta ett spelbart kort
            return choosenCard;
        }
    }

return null;

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
}
 