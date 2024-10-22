// See https://aka.ms/new-console-template for more information
public abstract class Player
{
    public string name {get; private set;}
    public List<IUnoCard> hand2 {get; set;}
    public Hand hand;
    
    public Player(string name, Deck deck)
    {
      this.name = name;
      this.hand2 = new List<IUnoCard>(); 
      this.hand = new Hand();
      hand.GenerateHand(deck);
    }

 
    public void PlaceCard()
    {
        
    }

    

}