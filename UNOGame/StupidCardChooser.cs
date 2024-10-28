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
 public void ChooseCard(Player player){
    
    while(true)
    {
            
            Thread.Sleep(5000);
            Console.Write($"Last drawn card: ");
            playedCards.DisplayFirstCard();

            Console.WriteLine("Robotics hand: ");
            player.ShowHand();

            // Låt spelaren välja ett kort
            bool maxCardsDrawn =false;
            choosenCard = RandomCard(player.hand);

            // Kontrollera om det valda kortet kan spelas
            if (playedCards.CanPlayCard(choosenCard))
            {
                // Kortet kan spelas, bryt ut ur loopen och spela kortet
                Console.WriteLine($"Robotic selected: {choosenCard.GetColor()} {choosenCard.GetValue()}");
                playedCards.AddCardToFront(choosenCard);
                player.hand.RemoveCard(choosenCard);
                ResetDraw();
                break;
            }
            else
            {
                // Kortet kan inte spelas, ge feedback till spelaren
                
                Console.WriteLine($"Robotic cannot play {choosenCard.GetColor()} {choosenCard.GetValue()}. Robotic tries again");
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
                // Om spelaren har dragit tre kort och inte kan lägga något kort, växla till nästa spelare
            
            
     }
    
 }

public IUnoCard RandomCard(Hand hand)
{
    Random random = new Random();
    int randomCardIndex = random.Next(0, hand.Count());
    return hand.CurrentCard(randomCardIndex);
}

    public void ResetDraw()
    {
        drawCounter = 0;
    }
}
