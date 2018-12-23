using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlagsAPI.Models
{
    public interface IWorkerServiceFlags
    {
        Task<List<string>> GetFlagsUrl();
        Task<string> ReturnFlagUrl(Flag flagRequest);
    } 
}
