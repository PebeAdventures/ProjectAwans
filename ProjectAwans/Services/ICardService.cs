using ProjectAwans.Models.Enums;
using ProjectAwans.Models;
using ProjektAwans.Models;
using ProjectAwans.Dtos;

namespace ProjectAwans.Services
{
   public interface ICardService
   {
      Card CreateCard(CreateCardDto cardDto);
      Task<Card> GenerateRandomCardAsync();
   }
}
