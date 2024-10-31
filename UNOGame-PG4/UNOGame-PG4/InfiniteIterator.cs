using System;
using System.Collections.Generic;

//KRAV 1:
//1: Generics
//2: Klassen tar emot något av en itererbar generisk typ (en kollektion av data) i sin konstruktor och sparar datan i en generisk lista items. 
// Sedan finns det olika metoder som används för att iterera över den generiska lista.
//3: Vi använder generics här för att vi ska kunna iterera över objekt av olika datatyper.
//Detta eftersom att vi vill att användaren ska kunna iterera över sina kort på handen, 
//samt iterera över objekt av datatypen List<string> för att representera vilka alternativ som användaren kan välja för att skapa datorn och dens strategi
//Istället för att behöva duplicera kod för respektive itererbar datatyp, så har vi löst det med att göra denna klass generisk för att undvika att duplicera 
// kod samt att koden blir mer återanvändbar och mer möjlig att förändra eller utveckla i framtiden. 

//KRAV 4:
//1; Iterator pattern
//2: Vi har skapat en infinite ierator genom att skapa en klass som implementerar IEnumerable<T>. MoveNext() och MovePrevious() metoderna ser till
//  att iterationen aldrig tar slut även om antalet element är en ändlig mängd, alltså att iteratorn börjar om från början när den har nått slutet.
//3: Vi valde att använda infinite iterator pattern för att kunna itererera oändligt mellan korten på handen men också mellan datorns strategier.

public class InfiniteIterator<T>
{
    private readonly List<T> items;
    private int currentIndex;

    public InfiniteIterator(IEnumerable<T> items)
    {
        this.items = new List<T>(items);
        currentIndex = 0;
    }

    public T Current => items[currentIndex];

    public void MoveNext()
    {
        currentIndex = (currentIndex + 1) % items.Count;
    }

    public void MovePrevious()
    {
        currentIndex = (currentIndex - 1 + items.Count) % items.Count;
    }

}
