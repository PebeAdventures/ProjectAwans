using ProjectAwans.Models.Enums;
using ProjektAwans.Models;

namespace ProjectAwans.Models.Factories
{
   public class CardFactory : ICardFactory
   {
      public Card CreateCard(string name, int cost, int power, List<AttackTypeEnum> attackType, CardTypeEnum cardType, string description, List<AbilityActivationEnum> ability, CardColor cardColor, int counter, List<TraitEnum> traits, string cardNumber)
      {
         return new Card
         {
            Id = Guid.NewGuid(),  // auto generated new ID
            Name = name,
            Cost = cost,
            Power = power,
            AttackTypes = attackType,
            Type = cardType,
            Description = description,
            Abilities = ability,
            CardColorId = cardColor.Id,  // assign cardID
            CardColor = cardColor,       // assign color object
            Counter = counter,
            Traits = new List<TraitEnum>(traits),
            CardNumber = cardNumber
         };
      }
   }
}
