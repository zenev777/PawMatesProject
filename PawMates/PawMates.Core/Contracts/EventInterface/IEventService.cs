using PawMates.Core.Models.EventViewModels;
using PawMates.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.Core.Contracts.EventInterface
{
    public interface IEventService
    {

        Task<IEnumerable<EventInfoViewModel>> GetAllEventsAsync();


    }
}
