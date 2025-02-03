using MovieManagementApi.Core.Entities;
using MovieManagementApi.Core.Interfaces;

namespace MovieManagementApi.Core.Services
{
    public class ActorService
    {
        private readonly IActorRepository _actorRepository;

        public ActorService(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        // Get all actors
        public async Task<List<Actor>> GetAllActorsAsync()
        {
            return await _actorRepository.GetAllActorsAsync();
        }

        // Get an actor by ID
        public async Task<Actor?> GetActorByIdAsync(int id)
        {
            return await _actorRepository.GetActorByIdAsync(id);
        }

        // Search actors by name
        public async Task<List<Actor>> SearchActorsByNameAsync(string name)
        {
            return await _actorRepository.SearchActorsByNameAsync(name);
        }

        // Add a new actor
        public async Task AddActorAsync(Actor actor)
        {
            await _actorRepository.AddActorAsync(actor);
        }

        // Update an existing actor
        public async Task UpdateActorAsync(Actor actor)
        {
            await _actorRepository.UpdateActorAsync(actor);
        }

        // Delete an actor
        public async Task DeleteActorAsync(int id)
        {
            await _actorRepository.DeleteActorAsync(id);
        }
    }
}