## UNO – Console-Based Card Game in C#

This is a terminal-based version of the classic UNO game, developed in C# as a project in the course **Object-Oriented Programming 2** at Uppsala University.

Play against a computer using arrow keys to navigate your hand, draw cards, and play color-coded UNO cards directly in the console.

---

### Gameplay Features

- Play against a computer with different card-choosing strategies (stupid / smart)
- Navigate your hand using ← and → arrow keys
- Draw up to 3 cards per turn
- Color-coded cards shown in the console
- Supports both regular and special UNO cards (e.g. +2, Change Color)
- Visual highlighting of the selected card
- Game history is logged and saved to file

---

### Project requirements for Object Oriented Programming II at Uppsala University

This project fulfills all conceptual requirements specified in the course:

#### Generics
- `InfiniteIterator<T>` is a custom generic type that enables circular navigation over any list, such as a player's hand.

#### Strategy Pattern
- The computer's behavior is implemented using the strategy pattern via `ICardChooser`:
  - `SmartCardChooser` chooses playable cards using logic.
  - `StupidCardChooser` picks the first available card.

#### Bridge Pattern
- Two abstraction hierarchies:
  - `IUnoCard` with subtypes: `RegularCard`, `SpecialCard`
  - `IFileHandler` with subtypes: `TextFileHandler`, `JsonFileHandler`
- The `GameHistoryManager` composes a `IFileHandler`, forming the bridge.

#### Iterator Pattern
- `InfiniteIterator<T>` is a custom iterator that wraps around a collection and allows endless forward/backward iteration – used to browse through cards with arrow keys.

#### LINQ Method Syntax
- LINQ is used to simplify logic, e.g. filtering playable cards:
  ```csharp
  return hand.Where(card => playedCards.CanPlayCard(card)).FirstOrDefault();
  ```

---

### How to Run

Requires [.NET SDK 8+](https://dotnet.microsoft.com/en-us/download/dotnet).

From the root of the project, run:

```bash
dotnet run --project UNO/UNO.csproj
```

---

### Project Structure

```
UNO/
├── UNO/
│   ├── Program.cs             # Entry point
│   ├── Game.cs                # Game controller
│   ├── CardChooserFactory.cs  # Strategy injection
│   ├── InfiniteIterator.cs    # Generic iterator
│   ├── RegularCard.cs / SpecialCard.cs
│   ├── JsonFileHandler.cs / TextFileHandler.cs  # Bridge pattern
│   └── ...
├── UNO.sln
└── README.md
```

---


