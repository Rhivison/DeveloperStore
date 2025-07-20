using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IEventPublisher
    {
        Task PublishAsync<T>(string channel, T @event) where T : class;
    }
}