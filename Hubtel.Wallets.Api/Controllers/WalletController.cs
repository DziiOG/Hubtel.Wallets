using Hubtel.Wallets.Api.Contracts.ResponseDtos;
using Hubtel.Wallets.Api.Contracts.DataDtos;
using Hubtel.Wallets.Api.Interfaces;
using Hubtel.Wallets.Api.Filters;
using System.Collections.Generic;
using Hubtel.Wallets.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MongoDB.Bson;
using System;
using Hubtel.Wallets.Api.Extensions;

namespace Hubtel.Wallets.Api.Controllers
{
    /// <summary>
    /// contains all endpoints for the wallet model
    /// </summary>
    //wallets
    [ApiController]
    [Route(ApiRoutes.WalletsApiRoutes.WalletsBase)]
    public class WalletController : ControllerBase
    {
        private readonly IWalletRepository repository;

        public WalletController(IWalletRepository repository)
        {
            this.repository = repository;
        }

        // GET /wallets
        [HttpGet]
        // [RedisCached(duration: 60)]
        [Validate<WalletQueryDto>("Query")]
        public async Task<ActionResult<SuccessWithDataResponse<IEnumerable<WalletDto>?>?>> GetAsync(
            [FromQuery] WalletQueryDto Query
        )
        {
            IEnumerable<WalletDto>? wallets = (
                await repository.GetAsync(criteria: Query.ToFilterCriteria())
            ).Select(wallet => wallet.AsDto());
            SuccessWithDataResponse<IEnumerable<WalletDto>?>? response =
                new SuccessWithDataResponse<IEnumerable<WalletDto>?>()
                {
                    data = wallets,
                    statusCode = 200,
                    message = "Wallets fetched successfully"
                };
            return Ok(response);
        }

        // GET /wallets/{id}
        [HttpGet(ApiRoutes.WalletsApiRoutes.ById)]
        [Validate<string>("id")]
        public async Task<ActionResult<SuccessWithDataResponse<WalletDto>>> GetByIdAsync(
            [FromRoute] string id
        )
        {
            Wallet? wallet = await repository.GetByIdAsync(ObjectId.Parse(id))!;
            if (wallet is null)
            {
                return NotFound(
                    new ResponseWithError() { statusCode = 404, message = "Not found" }
                );
            }
            return Ok(
                new SuccessWithDataResponse<WalletDto>()
                {
                    data = wallet.AsDto(),
                    statusCode = 200,
                    message = "Wallet fetched successfully"
                }
            );
        }

        // POST /wallets
        [HttpPost]
        [Validate<CreateWalletDto>("walletDto")]
        public async Task<ActionResult<SuccessWithDataResponse<WalletDto>?>> CreateAsync(
            [FromBody] CreateWalletDto walletDto
        )
        {
            Wallet item = new Wallet()
            {
                Id = ObjectId.Parse(walletDto.Id),
                LevelOfEducation = walletDto.LevelOfEducation,
                YearsOfExperience = walletDto.YearsOfExperience,
                FarmingExperience = walletDto.FarmingExperience,
                CropsSupervised = walletDto.CropsSupervised,
                CourseOfStudy = walletDto.CourseOfStudy,
                CreatedDate = DateTimeOffset.UtcNow,
                UpdatedDate = DateTimeOffset.UtcNow
            };
            await repository.CreateAsync(item);

            // returns a 201 response with the new created item by calling a reference method that would return the new created data
            return CreatedAtAction(
                nameof(GetByIdAsync),
                new { id = item.Id },
                new SuccessWithDataResponse<WalletDto>()
                {
                    statusCode = 201,
                    data = item.AsDto(),
                    message = "Wallet created successfully"
                }
            );
        }

        // PATCH /wallets/{id}
        [HttpPatch(ApiRoutes.WalletsApiRoutes.ById)]
        [Validate<string>("id")]
        [Validate<PatchWalletDto>("walletDto")]
        public async Task<ActionResult<SuccessWithDataResponse<WalletDto?>?>?> PatchAsync(
            [FromRoute] string id,
            [FromBody] PatchWalletDto walletDto
        )
        {
            ObjectId Id = ObjectId.Parse(id);
            Wallet? wallet = await repository.GetByIdAsync(Id)!;
            if (wallet == null)
            {
                return NotFound(
                    new ResponseWithError() { statusCode = 404, message = "Not found" }
                );
            }
            List<FilterCriteriaItem>? patchQuery = walletDto.ToFilterCriteria();
            await repository.PatchAsync(Id, patchQuery);
            Wallet? updatedDoc = await repository.GetByIdAsync(Id)!;
            return new SuccessWithDataResponse<WalletDto?>()
            {
                data = updatedDoc!.AsDto(),
                statusCode = 200,
                message = "Wallet patched successfully"
            };
        }

        // DELETE /wallets/{id}
        [HttpDelete(ApiRoutes.WalletsApiRoutes.ById)]
        [Validate<string>("id")]
        public async Task<ActionResult> DeleteAsync([FromRoute] string id)
        {
            ObjectId Id = ObjectId.Parse(id);
            Wallet? existingItem = await repository.GetByIdAsync(Id)!;

            if (existingItem is null)
            {
                return NotFound(
                    new ResponseWithError() { statusCode = 404, message = "Not found" }
                );
            }
            await repository.DeleteAsync(Id);
            return NoContent();
        }
    }
}
