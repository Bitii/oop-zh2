using System.Text;

namespace ZH2;

public interface Rendelheto
{
    public void Rendel(int db);
}
public abstract class Konyv
{
    protected string szerzője;
    protected string címe;
    public abstract bool DedikalASzerzo();
}
public class Peldatar : Konyv
{
    public int feladatokSzama;
    public Peldatar(string szerzője, string címe, int feladatokSzama)
    {
        this.szerzője = szerzője;
        this.címe = címe;
        this.feladatokSzama = feladatokSzama;
    }
    public override bool DedikalASzerzo()
    {
        return false;
    }
    public override string ToString()
    {
        return $"A könyv címe: {címe}, szerzője: {szerzője}, feladatok száma: {feladatokSzama}";
    }
}

public class Regeny : Konyv, Rendelheto
{
    public string osszefoglalo;
    public Regeny(string szerzője, string címe, string osszefoglalo)
    {
        this.szerzője = szerzője;
        this.címe = címe;
        this.osszefoglalo = osszefoglalo;
    }
    public override bool DedikalASzerzo()
    {
        return true;
    }
    public void Rendel(int db) 
    {
        Console.WriteLine($"Rendelni kell {db} regényt az alábbiból: {címe}");
    }
    public override string ToString()
    {
        return $"A könyv címe: {címe}, szerzője: {szerzője}, összefoglaló: {osszefoglalo}";
    }
}
public class CD : Rendelheto
{
    public string eloado;
    public string albumCíme;
    public CD(string eloado, string albumCíme)
    {
        this.eloado = eloado;
        this.albumCíme = albumCíme;
    }
    public void Rendel(int db) 
    { 
        Console.WriteLine($"Rendelni kell {db} CD-t az alábbiból: {eloado} - {albumCíme}");
    }
    public override string ToString()
    {
        return $"Az album címe: {albumCíme}, előadója: {eloado}";
    }
}
class Program
{
    public static List<Konyv> konyvek = new List<Konyv>();
    public static List<CD> cd = new List<CD>();
    public static void RendelesFelvesz(string file)
    {
        try 
        { 
            StreamReader sr = new StreamReader(file, Encoding.Default);
            string sor;
            while ((sor = sr.ReadLine()) != null)
            {
                string[] adatok = sor.Split(";");
                if (adatok[0] == "cd")
                {
                    cd.Add(new CD(adatok[1], adatok[2]));   
                }
                if (adatok[0] == "peldatar"){
                    konyvek.Add(new Peldatar(adatok[1], adatok[2], int.Parse(adatok[3])));
                }
                if (adatok[0] == "regeny")
                {
                    konyvek.Add(new Regeny(adatok[1], adatok[2], adatok[3]));
                }
            }
            sr.Close();
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine("A fájl nem található");
        }
        catch (IOException e)
        {
            Console.WriteLine("Hiba a fájl olvasásakor");
        }
        catch (Exception e)
        {
            Console.WriteLine("Valami hiba történt..");
        }
    }
    
    
    public static void Rendel()
    {
        int szam = new Random().Next(5, 21);
        foreach (Konyv k in konyvek)
        {
            if (k is Rendelheto)
            {
                ((Rendelheto)k).Rendel(szam);
            }
        }
    }
    static void Main(string[] args)
    {
        Console.WriteLine("Adja meg a fájl helyét: ");
        string file = Console.ReadLine();
        RendelesFelvesz(file);
        Rendel();
    }
}