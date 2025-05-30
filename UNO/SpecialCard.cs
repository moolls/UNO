﻿public class SpecialCard : IUnoCard
{
    CardColor color;
    CardValue speciality;


    public SpecialCard(CardColor color, CardValue speciality)
    {
        this.color = color;
        this.speciality = speciality;
    }

    //REFERENS: 
    //ChatGPT har hjälpt oss skapa funktionalitet för att representera kortet med rätt färg i konsolen.
    public void DisplayCard()
    {
        if (GetColor() == CardColor.Red)
        {
            Console.BackgroundColor = ConsoleColor.Red;
        }
        else if (GetColor() == CardColor.Green)
        {
            Console.BackgroundColor = ConsoleColor.Green;
        }
        else if (GetColor() == CardColor.Blue)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
        }
        else if (GetColor() == CardColor.Yellow)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
        }
        else
        {
            Console.BackgroundColor = ConsoleColor.Black;
        }

        Console.WriteLine($"{color} {speciality}");
        Console.ResetColor();
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