using PiszczekSzpotek.BookCatalogue.Core.Exceptions;

namespace PiszczekSzpotek.BookCatalogue.Core.Enums
{
    public static class BookCategoryExtensions
    {
        public static string GetString(this BookCategory category)
        {
            switch (category)
            {
                case BookCategory.Fantastyka:
                    return "Fantastyka";
                case BookCategory.Kryminal_Sensacja:
                    return "Kryminał/Sensacja";
                case BookCategory.Literatura_Piekna:
                    return "Literatura piękna";
                case BookCategory.Literatura_Faktu:
                    return "Literatura faktu";
                case BookCategory.Historia:
                    return "Historia";
                case BookCategory.Kultura_I_Sztuka:
                    return "Kultura i sztuka";
                case BookCategory.Dla_Mlodziezy:
                    return "Dla młodzieży";
                default:
                    throw new InvalidBookCategoryException();
            }
        }

        public static BookCategory SetFromString(string categoryString)
        {
            switch (categoryString)
            {
                case "Fantastyka":
                    return BookCategory.Fantastyka;
                case "Kryminał/Sensacja":
                    return BookCategory.Kryminal_Sensacja; 
                case "Literatura piękna":
                    return BookCategory.Literatura_Piekna; 
                case "Literatura faktu":
                    return BookCategory.Literatura_Faktu;
                case "Historia":
                    return BookCategory.Historia;
                case "Kultura i sztuka":
                    return BookCategory.Kultura_I_Sztuka; 
                case "Dla młodzieży":
                    return BookCategory.Dla_Mlodziezy;
                default:
                    throw new InvalidBookCategoryException();
            }
        }
    }
}
