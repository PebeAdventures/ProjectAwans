using ProjectAwans.Models.Enums;
using ProjektAwans.Models;

namespace ProjectAwans.Models.Factories
{
   public class CardFactory : ICardFactory
   {
      public Card CreateCard(
          string name,
          int cost,
          int power,
          AttackTypeEnum attackType,
          CardTypeEnum cardType,
          string description,
          AbilityActivationEnum ability,
          CardColor cardColor,
          int counter,
          TraitEnum[] traits,
          string cardNumber)
      {
         return new Card
         {
            Id = Guid.NewGuid(),  // auto generated new ID
            Name = name,
            Cost = cost,
            Power = power,
            AttackTypes = new List<AttackTypeEnum> { attackType }, 
            Type = cardType,
            Description = description,
            Abilities = new List<AbilityActivationEnum> { ability },
            CardColorId = cardColor.Id,  // assign cardID
            CardColor = cardColor,       // assign color object
            Counter = counter,
            Traits = new List<TraitEnum>(traits),
            CardNumber = cardNumber
         };
      }
   }
}
