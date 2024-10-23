public class CardChooserFactory
{

    public ICardChooser CreateCardChooser(string choice, Deck deck, Hand hand, PlayedCards playedCards)
    {
        return choice switch
        {
            "1" => new StupidCardChooser(deck, hand, playedCards), 
            "2" => new SmartCardChooser(deck, hand, playedCards),
            _ => throw new ArgumentException("Invalid choice")
        };
    }

}