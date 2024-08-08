using ChuckItApi.Models;
using ChuckItApi.Models.DTOs;
using ChuckItApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChuckItApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        private readonly IListingService _listingService;

        public MessagesController(IMessageService messageService, IUserService userService)
        {
            _messageService = messageService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var messages = await _messageService.GetMessagesForUser(userId);

            var resources = messages.Select(message => new
            {
                Id = message.Id,
                ListingId = message.ListingId,
                DateTime = message.datetime,
                Content = message.Message,
                FromUser = new { Id = message.FromUser.Id, Name = message.FromUser.UserName },
                ToUser = new { Id = message.ToUser.Id, Name = message.FromUser.UserName},
            });

            return Ok(resources);
        }

        public async Task<IActionResult> SendMessage([FromBody] MessageDto messageDto)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var listing = await _listingService.GetListingByIdAsync(messageDto.ListingId);
            if (listing == null) return BadRequest(new { error = "Invalid userId" });

            var targetUser = await _userService.GetUserByIdAsync(listing.UserId);
            if (targetUser == null) return BadRequest(new { error = "Invalid userId" });

            var message = new Messages
            {
                FromUserId = UserId,
                ToUserId = listing.UserId,
                ListingId = messageDto.ListingId,
                Message = messageDto.Message
            };

            await _messageService.AddMessage(message);

            /** For push notifications
             if (Expo.IsExpoPushToken(targetUser.ExpoPushToken))
                 await sendPushNotification(targetUser.ExpoPushToken, messageDto.Message);
            **/

            return StatusCode(201);
        }

    }
}
