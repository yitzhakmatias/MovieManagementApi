using Microsoft.EntityFrameworkCore;
using MovieManagementApi.Core.Entities;
using MovieManagementApi.Core.Interfaces;
using MovieManagementApi.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManagementApi.Infrastructure.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly AppDbContext _context;

        public ActorRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get all actors
        public async Task<List<Actor>> GetAllActorsAsync()
        {
            return await _context.Actors.Include(a => a.Movies).ToListAsync();
        }

        // Get actor by ID
        public async Task<Actor?> GetActorByIdAsync(int id)
        {
            return await _context.Actors.Include(a => a.Movies)
                                        .FirstOrDefaultAsync(a => a.Id == id);
        }

        // Search actors by name
        public async Task<List<Actor>> SearchActorsByNameAsync(string name)
        {
            return await _context.Actors
                .Where(a => a.Name.ToLower().Contains(name.ToLower()))
                .Include(a => a.Movies)
                .ToListAsync();
        }

        // Add a new actor
        public async Task AddActorAsync(Actor actor)
        {
            await _context.Actors.AddAsync(actor);
            await _context.SaveChangesAsync();
        }

        // Update an existing actor
        public async Task UpdateActorAsync(Actor actor)
        {
            _context.Actors.Update(actor);
            await _context.SaveChangesAsync();
        }

        // Delete an actor by ID
        public async Task DeleteActorAsync(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor != null)
            {
                _context.Actors.Remove(actor);
                await _context.SaveChangesAsync();
            }
        }
    }
}
