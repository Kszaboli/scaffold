using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Olimpia.Models;
using static Olimpia.Models.Dtos;

namespace Olimpia.Controllers
{
    [Route("player")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Player> Post(CreatePlayerDto createPlayerDto)
        {
            var player = new Player()
            {
                Id = Guid.NewGuid(),
                Name = createPlayerDto.Name,
                Age = createPlayerDto.Age,
                Height = createPlayerDto.Height,
                Weight = createPlayerDto.Weight,
                CreatedTime = DateTime.Now,
            };

            if (player != null)
            {
                using (var context = new OlimpiaContext())
                {
                    context.Players.Add(player);
                    context.SaveChanges();
                    return StatusCode(201, player);
                }
            }
            return BadRequest();
        }

        [HttpGet]
        public ActionResult<Player> Get()
        {
            using ( var context = new OlimpiaContext())
            {
                return Ok(context.Players.ToList());
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Player> GetByid(Guid id)
        {
            using( var context = new OlimpiaContext())
            {
                var player = context.Players.FirstOrDefault(x => x.Id == id);
                if (player != null)
                {
                    return Ok(player);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Player> Put(Guid id, UpdatePlayerDto updatePlayerDto)
        {
            using (var context = new OlimpiaContext())
            {
                var existingPlayer = context.Players.FirstOrDefault(x => x.Id == id);
                if (existingPlayer != null)
                {
                    existingPlayer.Name = updatePlayerDto.Name;
                    existingPlayer.Height = updatePlayerDto.Height;
                    existingPlayer.Weight = updatePlayerDto.Weight;
                    existingPlayer.Age = updatePlayerDto.Age;

                    context.Players.Update(existingPlayer);
                    context.SaveChanges();

                    return Ok(existingPlayer);
                }
                return NotFound();
            }
        }

        [HttpDelete]
        public ActionResult<Player> Delete(Guid id)
        {
            using (var context = new OlimpiaContext())
            {
                var player = context.Players.FirstOrDefault(x => x.Id == id);
                if (player != null)
                {
                    context.Players.Remove(player);
                    context.SaveChanges();
                    return StatusCode(200);
                }
                return NotFound();
            }
        }
    }
}
