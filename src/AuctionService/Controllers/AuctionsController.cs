﻿using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controllers;

[ApiController]
[Route("api/auctions")]
public class AuctionsController : ControllerBase
{
        private readonly AuctionDbContext _context;
        private readonly IMapper _mapper;

        public AuctionsController(AuctionDbContext context, IMapper mapper)
        {
                _context = context;
                _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuctionDto>>> GetAllAuctions()
        {
                var auctions = await _context.Auctions.Include(a => a.Item).OrderBy(x => x.Item.Make).ToListAsync();
                return Ok(_mapper.Map<List<AuctionDto>>(auctions));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionDto>> GetAuctionById(Guid id)
        {
                var auction = await _context.Auctions.Include(a => a.Item).FirstOrDefaultAsync(x => x.Id == id);

                if (auction == null) return NotFound();

                return _mapper.Map<AuctionDto>(auction);
        }

        [HttpPost]
        public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto createAuctionDto)
        {
                var auction = _mapper.Map<Auction>(createAuctionDto);
                // To do: Add current user as the auction seller
                auction.Seller = "user123";
                _context.Auctions.Add(auction);
                bool result = await _context.SaveChangesAsync() > 0;

                if (!result) return BadRequest("Could not save changes to the database");

                return CreatedAtAction(nameof(GetAuctionById), new { id = auction.Id }, _mapper.Map<AuctionDto>(auction));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuction(Guid id, UpdateAuctionDto updateAuctionDto)
        {
                var auction = await _context.Auctions.Include(x => x.Item).FirstOrDefaultAsync(x => x.Id == id);

                if (auction == null) return NotFound();

                // TODO: Check if the current user is the seller of the auction

                auction.Item.Make = updateAuctionDto.Make ?? auction.Item.Make;
                auction.Item.Model = updateAuctionDto.Model ?? auction.Item.Model;
                auction.Item.Color = updateAuctionDto.Color ?? auction.Item.Color;
                auction.Item.Mileage = updateAuctionDto.Mileage ?? auction.Item.Mileage;
                auction.Item.Year = updateAuctionDto.Year ?? auction.Item.Year;
                 
                bool result = await _context.SaveChangesAsync() > 0;

                if (!result) return BadRequest("Could not save changes to the database");

                return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuction(Guid id)
        {
                var auction = await _context.Auctions.FindAsync(id);

                if (auction == null) return NotFound();

                // TODO: Check if the current user is the seller of the auction
                _context.Remove(auction);
                bool result = await _context.SaveChangesAsync() > 0;
                if (!result) return BadRequest("Could not save changes to the database");

                return Ok();
        }

}