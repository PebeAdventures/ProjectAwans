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
          List<AttackTypeEnum> attackType,
          CardTypeEnum cardType,
          string description,
          List<AbilityActivationEnum> ability,
          CardColor cardColor,
          int counter,
          List<TraitEnum> traits,
          string cardNumber);
   }
}

