// See https://aka.ms/new-console-template for more information
public abstract class Player
{
    public string name;
    
    public Hand hand;

    public ICardChooser cardChooser;
    
    // public Player(string name, Deck deck)
    // {
    //   this.name = name;
    //   hand.GenerateHand(deck);
    // }
   

}