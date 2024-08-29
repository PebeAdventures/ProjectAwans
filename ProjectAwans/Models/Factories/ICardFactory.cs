using ProjectAwans.Models.Enums;
using ProjektAwans.Models;

namespace ProjectAwans.Models.Factories
{
   public interface ICardFactory
   {
      Card CreateCard(
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
          string cardNumber);
   }
}

