namespace Visma.Bootcamp.eShop.ApplicationCore.Entities.Models.Errors
{
    public class UnprocessableEntityError : Error
    {
        public UnprocessableEntityError(string message) : base(422, message)
        { }
    }
}
