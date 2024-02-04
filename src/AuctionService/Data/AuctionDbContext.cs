﻿using AuctionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Data;

public class AuctionDbContext : DbContext
{
    public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options)
    {
    }

    public DbSet<Auction> Auctions { get; set; } = default!;

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Auction>().HasOne(a => a.Item).WithOne(i => i.Auction).OnDelete(DeleteBehavior.Cascade);
    // }
}

