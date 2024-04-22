﻿using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Queries;
using Collectioneer.API.Operational.Domain.Services.Intern;
using Collectioneer.API.Shared.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Collectioneer.API.Operational.Presentation.Controllers
{
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionService _auctionService;
        private readonly ILogger<AuctionController> _logger;
        private readonly IUserService _userService;
        public AuctionController(IAuctionService auctionService, ILogger<AuctionController> logger, IUserService userService)
        {
            _auctionService = auctionService;
            _logger = logger;
            _userService = userService;
        }

        // POST api/v1/auctions
        [Authorize]
        [HttpPost("auctions")]
        public async Task<IActionResult> CreateAuction([FromBody] AuctionCreationCommand command)
        {
            try
            {
                var response = await _auctionService.CreateAuction(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating auction.");
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/v1/auctions/bids
        [Authorize]
        [HttpPost("auctions/bids")]
        public async Task<IActionResult> CreateBid([FromBody] BidCreationCommand command)
        {
            try
            {
                var response = await _auctionService.PlaceBid(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating bid.");
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/v1/auctions/bids
        [Authorize]
        [HttpGet("auctions/bids")]
        public async Task<IActionResult> GetBids([FromQuery] BidRetrieveQuery query)
        {
            try
            {
                var response = await _auctionService.GetBids(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting bids.");
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPost("auctions/close")]
        public async Task<IActionResult> CloseAuction([FromBody] AuctionCloseCommand command)
        {
            try
            {
                var response = await _auctionService.CloseAuction(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error closing auction.");
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPost("auctions/confirm")]
        public async Task<IActionResult> AuctioneerConfirmation([FromBody] AuctionValidationCommand command, [FromHeader(Name = "Authorization")] string? token)
        {
            token = token?.Replace("Bearer ", string.Empty);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            int userId = await _userService.GetUserIdByToken(token);

            try
            {
                if (command.AsAuctioneer)
                {
                    await _auctionService.AuctioneerConfirmation(command);
                    return Ok();
                }
                else if (command.AsBidder)
                {
                    await _auctionService.BidderConfirmation(command);
                    return Ok();
                }
                else
                {
                    return BadRequest("Operation role not specified.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error confirming auctioneer.");
                return StatusCode(500, ex.Message);
            }
        }

    }
}
