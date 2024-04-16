using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Queries;
using Collectioneer.API.Operational.Domain.Services.Intern;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Collectioneer.API.Operational.Presentation.Controllers
{
	public class AuctionController : ControllerBase
	{
		private readonly IAuctionService _auctionService;
		private readonly ILogger<AuctionController> _logger;
		public AuctionController(IAuctionService auctionService, ILogger<AuctionController> logger)
		{
			_auctionService = auctionService;
			_logger = logger;
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
		[HttpPost("auctions/confirm/auctioneer")]
		public async Task<IActionResult> AuctioneerConfirmation([FromBody] AuctionValidationCommand command)
		{
			try
			{
				await _auctionService.AuctioneerConfirmation(command);
				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error confirming auctioneer.");
				return StatusCode(500, ex.Message);
			}
		}

		[Authorize]
		[HttpPost("auctions/confirm/bidder")]
		public async Task<IActionResult> BidderConfirmation([FromBody] AuctionValidationCommand command)
		{
			try
			{
				await _auctionService.BidderConfirmation(command);
				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error confirming bidder.");
				return StatusCode(500, ex.Message);
			}
		}

	}
}
