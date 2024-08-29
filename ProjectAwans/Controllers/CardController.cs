using Microsoft.AspNetCore.Mvc;
using ProjectAwans.Dtos;
using ProjectAwans.Models;
using ProjectAwans.Models.Enums;
using ProjectAwans.Services;
using ProjektAwans.Models;


namespace ProjektAwans.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class CardController : ControllerBase
   {
      private readonly ICardService _cardService;

      public CardController(ICardService cardService)
      {
         _cardService = cardService;
      }

      [HttpPost("create")]
      public IActionResult CreateCard([FromBody] CreateCardDto createCardDto)
      {
         var newCard = _cardService.CreateCard(createCardDto);
/*             createCardDto.Name,
             createCardDto.Cost,
             createCardDto.Power,
             createCardDto.AttackType,
             createCardDto.CardType,
             createCardDto.Description,
             createCardDto.Ability,
             createCardDto.CardColor,
             createCardDto.Counter,
             createCardDto.Traits,
             createCardDto.CardNumber);
*/
         return Ok(newCard);
      }

      [HttpPost("generate-random")]
      public async Task<ActionResult<Card>> GenerateRandomCard()
      {
         var card = await _cardService.GenerateRandomCardAsync();
         return CreatedAtAction(nameof(GenerateRandomCard), new { id = card.Id }, card);
      }
   }
}
