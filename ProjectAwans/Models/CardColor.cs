using ProjectAwans.Models;

namespace ProjektAwans.Models
{
   public class CardColor
   {
      public int Id { get; set; }          // Klucz główny
      public string Name { get; set; }     // Nazwa koloru

      public ICollection<Card> Cards { get; set; }  // Relacja z kartami
   }
}
