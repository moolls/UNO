
// public class HandItemsNavigator<T>
// {
//     private List<T> handItems; //gör att vi kan ha både kort men också annat innehåll
//     private int currentIndex;

//     public HandItemsNavigator(List<T> hand)
//     {
//         this.handItems = hand;
//         this.currentIndex = 0;
//     }

// public T GetNext()
// {
//     currentIndex = (currentIndex + 1) % handItems.Count;
//     return handItems[currentIndex];
// }

// public T GetPrevious()
// {
//     currentIndex = (currentIndex - 1 + handItems.Count) % handItems.Count; // Säkerställ att vi inte får negativ index
//     return handItems[currentIndex];
// }

// public T GetCurrent()
//     {
        
//         return handItems[currentIndex];
//     }

// public CardColor GetCurrentColor()
// {
//     T unoCard = GetCurrent();


// }

// public CardValue GetCurrentColor()
// {
    
// }

//     public IUnoCard NavigateAndSelectCard()
//     {
//         ConsoleKey key;
//         do
//         {
//             Console.Clear();
//             Console.WriteLine($"Current card: {GetCurrent()}"); // Visa nuvarande kort

//             Console.WriteLine("Use Left/Right arrows to navigate, Enter to select card.");
//             key = Console.ReadKey().Key;

//             if (key == ConsoleKey.RightArrow)
//             {
//                 GetNext(); // Bläddra till nästa kort
//             }
//             else if (key == ConsoleKey.LeftArrow)
//             {
//                 GetPrevious(); // Bläddra till föregående kort
//             }

//         } while (key != ConsoleKey.Enter); // Avbryt loop när spelaren trycker på Enter

//         return GetCurrent(); // Returnera valt kort
//     }
// }
using System;
using System.Collections;
using System.Collections.Generic;

public class Hand 
{
    private List<IUnoCard> hand;
    int indexCurrentCard;

    public Hand()
    {
        this.hand = new List<IUnoCard>();
        indexCurrentCard = 0;
    }

    public void GenerateHand(Deck deck)
    {
        for(int i = 0; i < 7; i++)
            hand.Add(deck.DrawCard());
    }

    public void AddCardToHand(IUnoCard unoCard)
    {
        hand.Add(unoCard);
    }



    public void ShowHand()
    {
            foreach(var card in hand)
    {
        card.DisplayCard();
    }
    }
    public IEnumerable<IUnoCard> InfiniteIterator()
    {
        while (true)  // Infinite loop
        {
            yield return hand[indexCurrentCard];
            indexCurrentCard = (indexCurrentCard + 1) % hand.Count; // Circular navigation
        }
    }

    public IEnumerable<IUnoCard> InfiniteReverseIterator()
    {
        indexCurrentCard--;
        while (true)
        {
            yield return hand[indexCurrentCard];
            indexCurrentCard = (indexCurrentCard - 1 + hand.Count) % hand.Count; // Circular navigation backwards
        }
    }

public IUnoCard IterateHand(Deck deck)
{
    IEnumerator<IUnoCard> forwardIterator = InfiniteIterator().GetEnumerator();
    IEnumerator<IUnoCard> backwardIterator = InfiniteReverseIterator().GetEnumerator();
    bool usingForward = true;
    IUnoCard currentItem = null; // Variabel för att hålla det aktuella kortet

    ConsoleKey key;
    int counter = 0;
    
    do
    {
        ShowHand();
        // Få aktuellt kort
        if (usingForward)
        {
            // Flytta till nästa kort
            if (!forwardIterator.MoveNext())
            {
                forwardIterator = InfiniteIterator().GetEnumerator(); // Återställ iterator
                forwardIterator.MoveNext(); // Flytta till första kortet
            }
            currentItem = forwardIterator.Current; // Hämta aktuellt kort
        }
        else
        {
            // Flytta till nästa kort
            if (!backwardIterator.MoveNext())
            {
                backwardIterator = InfiniteReverseIterator().GetEnumerator(); // Återställ iterator
                backwardIterator.MoveNext(); // Flytta till sista kortet
            }
            currentItem = backwardIterator.Current; // Hämta aktuellt kort
        }

        Console.WriteLine($"Current item: {currentItem.GetColor()} {currentItem.GetValue()}");
        Console.WriteLine("Use Left/Right arrows to navigate, Enter to select item or press D to draw a new card.");
        key = Console.ReadKey().Key;

        // Scrolla höger
        if (key == ConsoleKey.RightArrow)
        {
            usingForward = true; // Använd framåt iterator
        }
        // Scrolla vänster
        else if (key == ConsoleKey.LeftArrow)
        {
            usingForward = false; // Använd bakåt iterator
        }
        else if (key == ConsoleKey.D && counter <= 2)
        {
            DrawCardToHand(deck);
            counter++;
            
        }
        else 
        {
            
        }

    } while (key != ConsoleKey.Enter); // Avsluta när Enter trycks

    // Skriv ut det valda kortet
    
    return currentItem;
}

    public void RemoveCard(IUnoCard card)
    {
        hand = hand.Where(x => x != card).ToList();
        ShowHand();
    
    }
    


public void DrawCardToHand(Deck deck)
{
    IUnoCard card = deck.DrawCard();
    AddCardToHand(card);
}
}


