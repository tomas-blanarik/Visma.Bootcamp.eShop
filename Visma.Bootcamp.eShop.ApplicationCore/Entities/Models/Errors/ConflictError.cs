namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.Models.Errors
{
    public class ConflictError : Error
    {
        public ConflictError(string message) : base(409, message)
        { }
    }
}
