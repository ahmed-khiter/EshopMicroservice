using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.CQRS
{
    public interface IQuery<TResponse> : ICommand<TResponse>
        where TResponse : notnull
    {
    }
}
