// See https://aka.ms/new-console-template for more information
public class HumanPlayer : Player
{
    public HumanPlayer(string name, Deck deck, ICardChooser cardChooser) : base(name, deck, cardChooser){
    }

    public override void ShowHand()
    {
        foreach (var card in hand)
        {
            card.DisplayCard();
        }
    }
    
    
}
