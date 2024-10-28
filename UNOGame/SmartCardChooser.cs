public class SmartCardChooser : ICardChooser
{

    Deck deck;
    PlayedCards playedCards;
    IUnoCard choosenCard;
    int indexCurrentCard;
    int drawCounter = 0;

    bool maxCardsDrawn;
    
    public SmartCardChooser(Deck deck, PlayedCards playedCards)
    {
        this.playedCards = playedCards;
        this.deck = deck;

    }
 public void ChooseCard(Player player)
 {
     while(true)
    {
            Thread.Sleep(5000);
            Console.Write($"Last drawn card: ");
            playedCards.DisplayFirstCard();

            Console.WriteLine("Robotics hand: ");
            player.ShowHand();

            // Låt spelaren välja ett kort
            bool maxCardsDrawn =false;
            choosenCard = FindCard(player.hand);

            // Kontrollera om det valda kortet kan spelas
            if(choosenCard == null)
            {
                // Finns inte ett enda kort med samma färg ell
                
                Console.WriteLine("Did not have a matching card");
                if (drawCounter < 3)
                {
                    player.hand.DrawCardToHand(deck); // Dra ett kort och lägg till i handen
                    drawCounter++;
                    Console.WriteLine($"Robotic drew a new card. Cards drawn: {drawCounter}/3");
                    
                }
                else {
                    
                    maxCardsDrawn = true; // Flagga sätts till true när max kort har dragits
                }

                if (maxCardsDrawn)
                {
                Console.WriteLine("Robotic have drawn the maximum number of cards and cannot play. Next player's turn.");
                ResetDraw();
                break; // Byt till nästa spelare
                }
            }
            else if (playedCards.CanPlayCard(choosenCard))
            {
                // Kortet kan spelas, bryt ut ur loopen och spela kortet
                Console.WriteLine($"Robotic selected: {choosenCard.GetColor()} {choosenCard.GetValue()}");
                playedCards.AddCardToFront(choosenCard);
                player.hand.RemoveCard(choosenCard);
                ResetDraw();
                break;
            }
        
        }
               
     }

public IUnoCard FindCard(Hand hand)
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



    public void ResetDraw()
    {
        drawCounter = 0;
    }
}
 