using System;

namespace Visma.Bootcamp.eShop.ApplicationCore.Exceptions
{
    public class EntityNotFoundException<T> : Exception
    {
        public EntityNotFoundException(object entityId)
            : base($"Entity of type {typeof(T)} with ID: {entityId} doesn't exist")
        { }
    }
}
