using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Data;

// Ét sted hvor alt hard-coded data ligger.
public static class DataStore
{
    // To hard-codede Brugere
    public static List<Bruger> Brugere = new()
    {
        new Bruger
        {
            Brugerid = 1, Navn = "Simon", tlfnr = "12345678", Mail = "simon@example.com", IsAdmin = true,
            opretelse = new DateOnly(2024, 1, 10)
        },
        new Bruger
        {
            Brugerid = 2, Navn = "Oskar", tlfnr = "87654321", Mail = "oskar@example.com", IsAdmin = false,
            opretelse = new DateOnly(2024, 2, 5)
        },
    };
    // Hard-coded lokaliteter
    public static List<Lokalitet> Lokationer = new()
    {
        new Lokalitet
        {
            LokationId = 1, LokationNavn = "Bar lager"
        },
        new Lokalitet
        {
            LokationId = 2, LokationNavn = "Sodavands lager"
        },
        new Lokalitet
        {
            LokationId = 3, LokationNavn = "Side lager"
        },
        new Lokalitet
        {
            LokationId = 4, LokationNavn = "Bag lager"
        },
    };

    public static List<Kategorier> Kategorier = new()
    {
        new Kategorier()
        {
            kategoriNavn = "Øl"
        },
        new Kategorier()
        {
            kategoriNavn = "Spiritus"
        },
        new Kategorier()
        {
            kategoriNavn = "Cider"
        },
        new Kategorier()
        {
            kategoriNavn = "Sodavand"
        },
        new Kategorier()
        {
            kategoriNavn = "Redbull"
        },
        new Kategorier()
        {
            kategoriNavn = "Juice"
        },
        new Kategorier()
        {
            kategoriNavn = "Konfekture"
        }
    };

    public static List<Varer> Varer = new()
    {
        new Varer()
        {
            Varerid = 1, Navn = "Carlsberg", Enhed = "Kasse", Udløbsdato = new DateOnly(2025, 12, 24), Beskrivelse = "Kasse med 24 Carlsberg dåseøl", Billede = "no data", Kategorier = "Øl"
        },
        new Varer()
        {
        Varerid = 2, Navn = "Smirnoff Vodka", Enhed = "Flaske, 1 L", Udløbsdato = new DateOnly(2026, 10, 10), Beskrivelse = "Flaske Smirnoff vodka", Billede = "no data", Kategorier = "Spiritus" 
        },
        new Varer()
        {
            Varerid = 3, Navn = "Redbull", Enhed = "Kasse", Udløbsdato = new DateOnly(2025, 12, 15), Beskrivelse = "Kasse med 24 Redbull dåseøl", Billede = "no data", Kategorier = "Redbull"
        },
        new Varer()
        {
            Varerid = 4, Navn = "Appelsinjuice", Enhed = "Karton, 1 L", Udløbsdato = new DateOnly(2026, 01, 20), Beskrivelse = "Karton appelsinjuice fra Rynkeby,", Billede = "no data", Kategorier = "Juice"
        }
    };

    public static List<VarerBeholdning> VarerBeholdning = new()
    {
        new VarerBeholdning()
        {
                VarerbeholdId = 1,
                VarerId = 1,                       // reference
                VarerNavn = "Carlsberg",           // denormaliseret kopi
                Mængde = 24,
                Lokalitet = new Lokalitet          // embedded
                {
                    LokationId = 4,
                    LokationNavn = "Bag lager"
                }
            },
            // Smirnoff Vodka i Side lager
            new VarerBeholdning
            {
                VarerbeholdId = 2,
                VarerId = 2,
                VarerNavn = "Smirnoff Vodka",
                Mængde = 10,
                Lokalitet = new Lokalitet
                {
                    LokationId = 3,
                    LokationNavn = "Side lager"
                }
            },
            // Redbull i Sodavands lager
            new VarerBeholdning
            {
                VarerbeholdId = 3,
                VarerId = 3,
                VarerNavn = "Redbull",
                Mængde = 24,
                Lokalitet = new Lokalitet
                {
                    LokationId = 2,
                    LokationNavn = "Sodavands lager"
                }
            },
            // Appelsinjuice i Bar lager
            new VarerBeholdning
            {
                VarerbeholdId = 4,
                VarerId = 4,
                VarerNavn = "Appelsinjuice",
                Mængde = 10,
                Lokalitet = new Lokalitet
                {
                    LokationId = 1,
                    LokationNavn = "Bar lager"
                }
            }
    };

}


