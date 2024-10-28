public class Filter<T> where T : IUnoCard
{
List<T> cards;

public Filter(List<T> cards)
{
    this.cards = cards;
}

public List<T> FilterByColor(CardColor color)
{
    return cards.Where(card => card.GetColor() == color).ToList();
}

}