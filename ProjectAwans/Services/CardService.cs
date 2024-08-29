using ProjectAwans.Models.Enums;
using ProjectAwans.Models.Factories;
using ProjectAwans.Models;
using ProjectAwans.Services;
using Microsoft.EntityFrameworkCore;
using ProjektAwans.Data;
using System;
using ProjektAwans.Models;
using ProjectAwans.Dtos;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using System.Diagnostics.Metrics;

namespace ProjectAwans.Services
{

   public class CardService : ICardService
   {
      private readonly AppDbContext _context;
      private readonly ICardFactory _cardFactory;
      private readonly Random _random;

      public CardService(AppDbContext context, ICardFactory cardFactory)
      {
         _context = context;
         _cardFactory = cardFactory;
         _random = new Random();
      }

      public Card CreateCard(CreateCardDto createCardDto)
      {
         // Sprawdź, czy CardColor istnieje w bazie danych
         var cardColor = _context.CardColors
             .FirstOrDefault(cc => cc.Id == createCardDto.CardColor.Id);

         // Jeśli CardColor nie istnieje, możesz go dodać do bazy danych
         if (cardColor == null)
         {
            cardColor = new CardColor
            {
               Id = createCardDto.CardColor.Id,
               Name = createCardDto.CardColor.Name
            };
            _context.CardColors.Add(cardColor);
            _context.SaveChanges(); // Zapisz, aby mieć pewność, że CardColor jest zapisany przed użyciem
         }

         var newCard = _cardFactory.CreateCard(
        createCardDto.Name,
        createCardDto.Cost,
        createCardDto.Power,
        createCardDto.AttackType,
        createCardDto.CardType,
        createCardDto.Description,
        createCardDto.Ability,
        cardColor,
        createCardDto.Counter,
        createCardDto.Traits,
        createCardDto.CardNumber);

         _context.Cards.Add(newCard);
         _context.SaveChanges();

         return newCard;
      }
      public async Task<CreateCardDto> GenerateRandomCardAsync()
      {
         // Lista nazw do wyboru
         var names = GetCardNames();

         // Losowa nazwa
         var name = names[_random.Next(names.Count)];

         // Losowy koszt od 1 do 10
         var cost = _random.Next(1, 11);

         // Losowy typ ataku (1 lub 2 typy)
         var attackTypes = Enum.GetValues(typeof(AttackTypeEnum))
             .Cast<AttackTypeEnum>()
             .OrderBy(x => _random.Next())
             .Take(_random.Next(1, 3))
             .ToArray();

         // Losowy typ karty
         CardTypeEnum cardType;
         if (cost > 2)
         {
            cardType = CardTypeEnum.Character;
         }
         else
         {
            cardType = (CardTypeEnum)_random.Next(0, Enum.GetValues(typeof(CardTypeEnum)).Length);
         }

         // Losowy opis
         var description = $"{name} performs a powerful randomly generated attack called Jasiu will conquer the world";

         // Losowa aktywacja zdolności
         AbilityActivationEnum[] abilities;
         if (cardType == CardTypeEnum.Stage)
         {
            abilities = new[] { AbilityActivationEnum.Main };
         }
         else if (cardType == CardTypeEnum.Event)
         {
            abilities = new[] { AbilityActivationEnum.Main, AbilityActivationEnum.Counter }
                .OrderBy(x => _random.Next())
                .Take(1)
                .ToArray();
         }
         else
         {
            abilities = Enum.GetValues(typeof(AbilityActivationEnum))
                .Cast<AbilityActivationEnum>()
                .OrderBy(x => _random.Next())
                .Take(1)
                .ToArray();
         }

         // Moc karty, jeśli typ to Character, w przeciwnym razie 0
         int power = 0;
         if (cardType == CardTypeEnum.Character)
         {
            // Losowa moc (co 1000) zależna od kosztu
            var maxPower = cost * 1000;
            var powerOptions = Enumerable.Range(0, maxPower / 1000 + 1).Select(i => i * 1000).ToArray();
            power = powerOptions[_random.Next(powerOptions.Length)];
         }

         // Losowy kolor
         // Pobieranie losowego koloru z bazy danych
         var cardColors = await _context.CardColors.ToListAsync();
         var cardColor = cardColors[_random.Next(cardColors.Count)];

         // Losowy counter
         int counter;
         if (cardType == CardTypeEnum.Character)
         {
            counter = new[] { 0, 1000, 2000 }[_random.Next(3)];
         }
         else
         {
            counter = 0;
         }

         // Losowe cechy
         var traits = Enum.GetValues(typeof(TraitEnum))
             .Cast<TraitEnum>()
             .OrderBy(x => _random.Next())
             .Take(_random.Next(1, 3))
             .ToArray();

         // Losowy numer karty
         var cardNumber = $"OP0{_random.Next(10, 100):D2}.{_random.Next(1000, 10000):D4}";

         // Tworzenie obiektu karty
         var card = new Card
         {
            Name = name,
            Cost = cost,
            Power = power,
            AttackTypes = attackTypes.ToList(),
            Type = cardType,
            Description = description,
            Abilities = abilities.ToList(),
            CardColorId = cardColor.Id,
            Counter = counter,
            Traits = traits.ToList(),
            CardNumber = cardNumber
         };

         // Zapis do bazy danych
         _context.Cards.Add(card);
         await _context.SaveChangesAsync();

         // Mapowanie karty na DTO
         var cardDto = new CreateCardDto
         {
            //Id = card.Id,
            Name = card.Name,
            Cost = card.Cost,
            Power = card.Power,
            CardColor = new CardColorDto
            {
               Id = card.CardColor.Id,
               Name = card.CardColor.Name
            },
            Description = card.Description,
            AttackType = card.AttackTypes,
            CardType = card.Type,
            Ability = card.Abilities,
            Counter = card.Counter,
            Traits = card.Traits,
            CardNumber = card.CardNumber
         };

         return cardDto;
      }

      private List<string> GetCardNames()
      {
         return new List<string>
            {
                "\"Hawk-Eye\" Mihawk",
                "\"Red Haired\" Shanks",
                "\"Red-Hair\" Shanks",
                "\"Red-Haired\" Shanks",
                "A",
                "Absalom (One Piece)",
                "Ace (One Piece)",
                "Admiral Akainu",
                "Admiral Aokiji",
                "Admiral Garp",
                "Akainu",
                "Alvida (One Piece)",
                "Ao Kiji",
                "Aokiji",
                "Aokiji (One Piece)",
                "Arlong",
                "Ax-Hand Morgan",
                "Axe Hand Morgan",
                "Axe-Hand Morgan",
                "Bartholomew Kuma",
                "Bartolomeo (One Piece)",
                "Basil Hawkins",
                "Bellamy (One Piece)",
                "Bellamy the Hyena",
                "Ben Beckman",
                "Bentham (One Piece)",
                "Bepo (One Piece)",
                "Big Mom",
                "Big Mom (One Piece)",
                "Big Mom Pirates",
                "Blackbeard (Marshall D. Teach)",
                "Blackbeard (One Piece)",
                "Blueno",
                "Bon Clay",
                "Borsalino (One Piece)",
                "Braham (One Piece)",
                "Brogy (One Piece)",
                "Brook (One Piece)",
                "Brooke (One Piece)",
                "Brooke, the gentlemen skeleton",
                "Brownbeard",
                "Brownbeard (One Piece)",
                "Buchi (One Piece)",
                "Buggy (One Piece)",
                "Buggy the Clown",
                "Buggy The Clown",
                "Burgess (One Piece)",
                "Burukku",
                "Butchi",
                "Cabaji",
                "Cabaji the Acrobat",
                "Caesar Clown",
                "Calgara",
                "Capone Bege",
                "Captain Arlong",
                "Captain Buggy",
                "Captain Chaser",
                "Captain Don Krieg",
                "Captain Hina",
                "Captain Krieg",
                "Captain Kuro",
                "Captain Morgan (One Piece)",
                "Captain Smoker",
                "Captain Wapol",
                "Captian Kuro",
                "Carrot (One Piece)",
                "Cavendish (One Piece)",
                "Chaka (One Piece)",
                "Charlotte Katakuri",
                "Charlotte Linlin",
                "Charlotte Smoothie",
                "Chaser (One Piece)",
                "Chess (One Piece)",
                "Chief Spandam",
                "Chu (One Piece)",
                "Chuu (One Piece)",
                "Cipher Pol No. 9",
                "Clahador",
                "Coby (One Piece)",
                "Commodore Smoker",
                "Conis (One Piece)",
                "Crocodile (character)",
                "Crocodile (fictional character)",
                "Crocodile (One Piece)",
                "Curly Dadan",
                "Cutty Flam",
                "Dalton (One Piece)",
                "Django (One Piece)",
                "Doc Q",
                "Doctor Hiluluk",
                "Doctor Hiruluk",
                "Doctor Hogback",
                "Doctor Vegapunk",
                "Doflamingo",
                "Don Krieg",
                "Donquixote Doflamingo",
                "Donquixote Rocinante",
                "Dorry (One Piece)",
                "Dr. Hiluluk",
                "Dr. Hiruluk",
                "Dr. Kureha",
                "Dr. Vegapunk",
                "Dracule \"Hawk Eye\" Mihawk",
                "Dracule \"Hawk-Eye\" Mihawk",
                "Dracule \"Hawkeye\" Mihawk",
                "Dracule Mihawk",
                "Dragon (One Piece)",
                "Drake (One Piece)",
                "Edward Newgate",
                "Emporio Ivankov",
                "Enel (One Piece)",
                "Ener",
                "Ener (One Piece)",
                "Eneru",
                "Ensign Tashigi",
                "Eustass Kid",
                "Eyelashes (One Piece)",
                "Fire Fist Ace",
                "Firefist Ace",
                "Fleet Admiral Sengoku",
                "Flying Fish Riders",
                "Four Emperors (One Piece)",
                "Foxy (One Piece)",
                "Foxy the Silver Fox",
                "Foxy the Sliver Fox",
                "Fujitora (One Piece)",
                "Fullbody (One Piece)",
                "Gaimon",
                "Gaimon (One Piece)",
                "Galdino (One Piece)",
                "Gan Fall",
                "Garp (One Piece)",
                "Gecko Moria",
                "Gecko Moriah",
                "Gedatsu",
                "Gedatsu (One Piece)",
                "Genzo (One Piece)",
                "Ghin (One Piece)",
                "Gin (One Piece)",
                "Gin the Man-Demon",
                "God Enel",
                "Gol D Roger",
                "Gold Roger",
                "Hachi (One Piece)",
                "Hamburg (One Piece)",
                "Hatchan",
                "Hawk-Eye Mihawk",
                "Hawkeye Mihawk",
                "Helmeppo",
                "Helmeppo (One Piece)",
                "Hiluluk",
                "Hina (One Piece)",
                "Hiruluk",
                "Hody Jones",
                "Holy (One Piece)",
                "Hotori (One Piece)",
                "Iceburg (One Piece)",
                "Igaram",
                "Igaram (One Piece)",
                "Inuarashi",
                "Issho (One Piece)",
                "Jabura",
                "Jack (One Piece character)",
                "Jaguar D. Saulo",
                "Jango (One Piece)",
                "Jango the hypnotist",
                "Jewelry Bonney",
                "Jimbei (One Piece)",
                "Johnny (One Piece)",
                "Johnny and Yosaku",
                "Jurakyūru Mihōku",
                "Juraquille \"Hawkeye\" Mihawk",
                "Juraquille Mihawk",
                "Jyabura",
                "Kaido (One Piece character)",
                "Kaidou",
                "Kaku (One Piece)",
                "Kalifa (One Piece)",
                "Kamakiri (One Piece)",
                "Karoo (One Piece)",
                "Kaya (One Piece)",
                "Killer (One Piece)",
                "Kin'emon",
                "King Nebra",
                "King of the Pirates",
                "King of The Pirates",
                "King Of The Pirates",
                "King Wapol",
                "Klahadore",
                "Koala (One Piece)",
                "Koby (One Piece)",
                "Kohza",
                "Kotori (One Piece)",
                "Koza (One Piece)",
                "Kozuki Oden",
                "Kuina (One Piece)",
                "Kumadori (One Piece)",
                "Kureha (One Piece)",
                "Kuro (One Piece)",
                "Kuromarimo",
                "Kuroobi (One Piece)"
            };
      }
   }
}