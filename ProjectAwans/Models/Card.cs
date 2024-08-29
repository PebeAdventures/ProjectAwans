using ProjectAwans.Models.Enums;
using ProjektAwans.Models;

namespace ProjectAwans.Models
{
   

   public class Card
   {
      public Guid Id { get; set; }                            // Unique identifier for the card
      public string Name { get; set; }                       // Name of the card
      public int Cost { get; set; }                          // Cost of playing the card
      public int Power { get; set; }                         // Power level of the card
      public List<AttackTypeEnum> AttackTypes { get; set; }      // List of attack types
      public CardTypeEnum Type { get; set; }                     // Type of the card (Character, Stage, Event)
      public string Description { get; set; }                // Description of the card
      public List<AbilityActivationEnum> Abilities { get; set; } // List of abilities and when they activate
      public int Counter { get; set; }                       // Counter value of the card
      public List<TraitEnum> Traits { get; set; }                // List of traits associated with the card
      public string CardNumber { get; set; }                 // Card number

      public int CardColorId { get; set; }        // foreign key
      public CardColor CardColor { get; set; }    // color source

      // Constructor
      public Card()
      {
         AttackTypes = new List<AttackTypeEnum>();
         Abilities = new List<AbilityActivationEnum>();
         Traits = new List<TraitEnum>();
      }
   }
}

