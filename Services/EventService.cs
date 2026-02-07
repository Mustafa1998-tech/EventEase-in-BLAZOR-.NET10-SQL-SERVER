using EventEase.Data;
using EventEase.Models;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Services;

public class EventService
{
    private readonly EventDbContext _context;

    public EventService(EventDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<Event>> GetPagedAsync(int page, int pageSize, string? search)
    {
        if (page < 1)
        {
            page = 1;
        }

        if (pageSize < 1)
        {
            pageSize = 10;
        }

        IQueryable<Event> query = _context.Events.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(e =>
                e.Title.Contains(term) ||
                (e.Description != null && e.Description.Contains(term)) ||
                (e.Location != null && e.Location.Contains(term)));
        }

        query = query.OrderBy(e => e.EventDate);

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Event>
        {
            Items = items,
            TotalCount = totalCount
        };
    }

    public Task<Event?> GetByIdAsync(int id)
    {
        return _context.Events.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Event?> GetForEditAsync(int id)
    {
        return await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Event> CreateAsync(Event model)
    {
        model.CreatedAt = DateTime.UtcNow;
        model.UpdatedAt = null;

        _context.Events.Add(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<bool> UpdateAsync(Event model)
    {
        var existing = await _context.Events.FirstOrDefaultAsync(e => e.Id == model.Id);
        if (existing is null)
        {
            return false;
        }

        existing.Title = model.Title;
        existing.Description = model.Description;
        existing.Location = model.Location;
        existing.EventDate = model.EventDate;
        existing.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
        if (existing is null)
        {
            return false;
        }

        _context.Events.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}
