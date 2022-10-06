using Microsoft.EntityFrameworkCore;
using TeamReport.Data.Entities;
using TeamReport.Data.Persistence;
using TeamReport.Data.Repositories.Interfaces;

namespace TeamReport.Data.Repositories;

public class LeadershipRepository : ILeadershipRepository
{
    private readonly ApplicationDbContext _context;

    public LeadershipRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Leadership> Create(Leadership entity)
    {
        _context.Leaderships.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Leadership?> Read(int entityId)
    {
        return await _context.Leaderships.FirstOrDefaultAsync(x => x.Id == entityId);
    }

    public async Task<bool> Update(Leadership entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int entityId)
    {
        var leadership = await Read(entityId);
        if (leadership is null)
        {
            return false;
        }

        _context.Leaderships.Remove(leadership);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Leadership>> ReadAll()
    {
        return await _context.Leaderships.ToListAsync();
    }

    public async Task<List<Member>> ReadLeaders(int memberId)
    {
        return await _context.Leaderships.Where(x => x.Member.Id == memberId).Select(x => x.Leader).ToListAsync();
    }

    public async Task<List<Member>> ReadReporters(int leaderId)
    {
        return await _context.Leaderships.Where(x => x.Leader.Id == leaderId).Select(x => x.Member).ToListAsync();
    }

    public async Task<bool> DeleteLeaderships(int memberId)
    {
        var itemsToRemove = await _context.Leaderships.Where(x => x.Leader.Id == memberId || x.Member.Id == memberId)
            .ToListAsync();

        _context.Leaderships.RemoveRange(itemsToRemove);
        await _context.SaveChangesAsync();
        return true;
    }
}