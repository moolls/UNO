// See https://aka.ms/new-console-template for more information
public abstract class Player
{
    public string name {get; private set;}
    
    public Hand hand = new Hand();

    public ICardChooser cardChooser;
    
    public Player(string name, Deck deck, ICardChooser cardChooser)
    {
      this.name = name;
      this.cardChooser = cardChooser;
      hand.GenerateHand(deck);
    }
   
      public abstract void ShowHand();

}