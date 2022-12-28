using System.ComponentModel;

namespace PiszczekSzpotek.BookCatalogue.Core.Enums
{
    public enum BookCategory
    {
        [Description("Dla dzieci")]
        DlaDzieci = 0,
        [Description("Dla młodzieży")]
        DlaMlodziezy = 1,
        [Description("Fantastyka")]
        Fantastyka = 2,
        [Description("Historia")]
        Historia = 3,
        [Description("Horror")]
        Horror = 4,
        [Description("Kryminał/Sensacja")]
        KryminalSensacja = 5,
        [Description("Kultura i sztuka")]
        KulturaISztuka = 6,
        [Description("Literatura faktu")]
        LiteraturaFaktu = 7,
        [Description("Literatura piękna")]
        LiteraturaPiekna = 8
    }
}
