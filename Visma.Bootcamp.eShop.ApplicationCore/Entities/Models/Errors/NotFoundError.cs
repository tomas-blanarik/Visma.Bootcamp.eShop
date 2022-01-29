namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.Models.Errors
{
    public class NotFoundError : Error
    {
        public NotFoundError(string message) : base(404, message)
        { }
    }
}
