using MovieManagementApi.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieManagementApi.Core.Interfaces
{
    public interface IActorRepository
    {
        Task<List<Actor>> GetAllActorsAsync();  // Get all actors
        Task<Actor?> GetActorByIdAsync(int id);  // Get actor by ID
        Task<List<Actor>> SearchActorsByNameAsync(string name);  // Search actors by name
        Task AddActorAsync(Actor actor);  // Add a new actor
        Task UpdateActorAsync(Actor actor);  // Update an existing actor
        Task DeleteActorAsync(int id);  // Delete an actor by ID
    }
}