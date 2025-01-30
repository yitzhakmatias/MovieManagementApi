using MovieManagementApi.Core.Entities;

namespace MovieManagementApi.Core.Interfaces
{
    public interface IActorRepository
    {
        Task<List<Actor>> GetAllActorsAsync();
        Task<Actor> GetActorByIdAsync(int id);
        Task AddActorAsync(Actor actor);
        Task UpdateActorAsync(Actor actor);
        Task DeleteActorAsync(int id);
    }
}