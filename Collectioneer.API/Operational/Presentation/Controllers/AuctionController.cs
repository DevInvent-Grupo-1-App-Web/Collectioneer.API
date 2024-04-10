using Collectioneer.API.Operational.Domain.Commands;
using Collectioneer.API.Operational.Domain.Queries;
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
    }
}
