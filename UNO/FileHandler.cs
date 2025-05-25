
//KRAV 1:
//1: Generics

//2: Vi använder generics för att skapa återanvändbara och typ-säkra filhanteringsklasser Genom
// att definiera en abstrakt klass FileHandler<T>, där T är en generisk typ kan vi hantera olika typer av
// data (t.ex List<string> och Dictionary<string, GameHistoryEntry>) i samma struktur.
// FileHandler<T> är den generiska bas-klassen som definierar abstrakta metoder för att skriva och läsa data där typen T
// representerar den specifika datatypen som vi arbetar med. Detta gör att klassen kan anpassas för olika typer av objekt.
// JsonFileHandler<T> och TextFileHandler är konkreta implemetationer av FileHandler<T> som tillhandahåller filhantering för specifika datatyper
// Json-filer respektiva textfiler), där typen T kan vara vilken som helst som är kompatibel med den specfiika filhanteraren.

//3: Vi använder detta för att i TextFileHandler som konstrueras i ComputerPlayer vill vi bara kunna lagra namn medans i JsonFileHandler vill vi
// lagra key-value pairs som innehåller information om spelhistorik. Metoderna för att läsa av och skriva av har samma beteende men har olika implementationer
// och därför har vi valt att använda generics här. Varför vi använder det här är för att koden blir mer flexibel och återanvändbar och ger möjligheten 
// att lägga till ytterligare fler datatyper som ska kunna hanteras på samma sätt i framtiden. Anledningen till varför vi använder en text-fil och inte
// en lista med slumpmässiga chars, är för att spara computer names permanent och att användaren ska kunna lägga till nya namn som alltid finns sparade kvar i spelet oavsett.


public abstract class FileHandler<T>
{
    public string FilePath { get; }

    protected FileHandler(string filePath)
    {
        FilePath = filePath;
    }

    public abstract void Write(T data);
    public abstract T Read();
}