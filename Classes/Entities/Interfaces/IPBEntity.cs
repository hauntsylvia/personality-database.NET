using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace personality_database.NET.Classes.Entities.Interfaces
{
    public interface IPBEntity
    {
        ulong id { get; }
        Uri? uri { get; }
    }
}
