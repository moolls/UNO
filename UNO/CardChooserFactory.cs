public class CardChooserFactory
{
    public ICardChooser CreateCardChooser(string choice, Deck deck, PlayedCards playedCards)
    {
        return choice switch
        {
            "Stupid" => new StupidCardChooser(deck, playedCards),
            "Smart" => new SmartCardChooser(deck, playedCards),
            _ => throw new ArgumentException("Invalid choice")
        };
    }

}