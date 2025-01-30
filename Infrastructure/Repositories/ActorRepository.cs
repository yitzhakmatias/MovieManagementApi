using Microsoft.EntityFrameworkCore;
using MovieManagementApi.Core.Entities;
using MovieManagementApi.Core.Interfaces;
using MovieManagementApi.Data;

namespace MovieManagementApi.Infrastructure.Repositories;


    public class ActorRepository : IActorRepository
    {
        private readonly AppDbContext _context;

        public ActorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Actor>> GetAllActorsAsync()
        {
            return await _context.Actors.ToListAsync();
        }

        public async Task<Actor> GetActorByIdAsync(int id)
        {
            return await _context.Actors
                .Include(a => a.Movies) // Include related Movies if needed
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddActorAsync(Actor actor)
        {
            await _context.Actors.AddAsync(actor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateActorAsync(Actor actor)
        {
            _context.Actors.Update(actor);
            await _context.SaveChangesAsync();
        }

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
