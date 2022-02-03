namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.Models.Errors
{
    public class BadRequestError : Error
    {
        public BadRequestError(string message) : base(400, message)
        { }
    }
}