public class CardStrategy {

    Deck deck;
    Hand hand;
    PlayedCards playedCards;


    public CardStrategy(Deck deck, Hand hand, PlayedCards playedCards)
    {
        this.deck = deck;
        this.hand = hand;
        this.playedCards = playedCards;
    }

    public void GetSpeciality()
    {
       IUnoCard lastCard = playedCards.First();
       
       if(lastCard.GetValue() == CardValue.plusFour)
       {

       }
       if (lastCard.GetValue() == CardValue.plusTwo)
       {

       }
       if (lastCard.GetValue() == CardValue.Block)
       {

       }
       else
       {

       }
    } 

    public void plusFour()
    {
    
    }
}