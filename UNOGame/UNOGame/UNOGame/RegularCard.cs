public class RegularCard : IUnoCard
{
    CardColor color;
    CardValue number;

    public RegularCard(CardColor color, CardValue number)
    {
        this.color = color;
        this.number = number;
    }

    public void Play()
    {
        //implementation
    }

    public void DisplayCard()
    {
        Console.WriteLine($"{color} {number}");
    }

   
    public CardColor GetColor()
    {
        return color;
    }
       public CardValue GetValue()
    {
        return number;
    }
}