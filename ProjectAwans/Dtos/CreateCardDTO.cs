using ProjectAwans.Models.Enums;
using ProjektAwans.Models;

namespace ProjectAwans.Dtos
{
   public class CreateCardDto
   {

      public string Name { get; set; }
      public int Cost { get; set; }
      public int Power { get; set; }
      public List<AttackTypeEnum> AttackType { get; set; }
      public CardTypeEnum CardType { get; set; }
      public string Description { get; set; }
      public List<AbilityActivationEnum> Ability { get; set; }
      public CardColorDto CardColor { get; set; } // DTO dla CardColor
      public int Counter { get; set; }
      public List<TraitEnum> Traits { get; set; }
      public string CardNumber { get; set; }
   }

}
