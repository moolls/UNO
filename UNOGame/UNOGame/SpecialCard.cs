public class SpecialCard : IUnoCard
{
    CardColor color;
    CardValue speciality;

    public SpecialCard(CardColor color, CardValue speciality)
    {
        this.color = color;
        this.speciality = speciality;
    }

    public void Play()
    {
        //implementation
    }

    public void DisplayCard()
    {
        Console.WriteLine($"{color} {speciality}");
    }

    public CardColor GetColor()
    {
        return color;
    }
       public CardValue GetValue()
    {
        return speciality;
    }

}
