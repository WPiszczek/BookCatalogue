using System.ComponentModel;

namespace PiszczekSzpotek.BookCatalogue.Core.Enums
{
    public enum BookCategory
    {
        [Description("Fantastyka")]
        Fantastyka = 0,
        [Description("Horror")]
        Horror = 1,
        [Description("Kryminał/Sensacja")]
        Kryminal_Sensacja = 2,
        [Description("Literatura piękna")]
        Literatura_Piekna = 3,
        [Description("Literatura faktu")]
        Literatura_Faktu = 4,
        [Description("Historia")]
        Historia = 5,
        [Description("Kultura i sztuka")]
        Kultura_I_Sztuka = 6,
        [Description("Dla młodzieży")]
        Dla_Mlodziezy = 7
    }
}
