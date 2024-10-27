public class StupidCardChooser : ICardChooser
{

    Deck deck;
    Hand hand;
    PlayedCards playedCards;
    IUnoCard choosenCard;
    int indexCurrentCard;
    int drawCounter = 0;

    bool maxCardsDrawn;
    
    public StupidCardChooser(Deck deck, Hand hand, PlayedCards playedCards)
    {
        this.hand = hand;
        this.playedCards = playedCards;
        this.deck = deck;

    }
 public void ChooseCard(){
    
    while(true)
    {
            Thread.Sleep(5000);
            Console.Write($"Last drawn card: ");
            playedCards.DisplayFirstCard();

            Console.WriteLine("Robotics hand: ");
            hand.ShowAnonym();

            // Låt spelaren välja ett kort
            bool maxCardsDrawn =false;
            choosenCard = RandomCard();

            // Kontrollera om det valda kortet kan spelas
            if (CanPlayCard(choosenCard))
            {
                // Kortet kan spelas, bryt ut ur loopen och spela kortet
                Console.WriteLine($"Robotic selected: {choosenCard.GetColor()} {choosenCard.GetValue()}");
                playedCards.AddCardToFront(choosenCard);
                hand.RemoveCard(choosenCard);
                break;
            }
            else
            {
                // Kortet kan inte spelas, ge feedback till spelaren
                
                Console.WriteLine($"Robotic cannot play {choosenCard.GetColor()} {choosenCard.GetValue()}. Robotic tries again");
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
                // Om spelaren har dragit tre kort och inte kan lägga något kort, växla till nästa spelare
            
            
     }
    
 }

public IUnoCard RandomCard()
{
    Random random = new Random();
    int randomCardIndex = random.Next(0, hand.Count());
    return hand.CurrentCard(randomCardIndex);
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
