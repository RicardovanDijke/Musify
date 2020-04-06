using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Song_Service
{
    public class SongRepository : Repository<Song>, ISongRepository
    {
        public SongRepository(DatabaseContext context) : base(context)
        {
            
        }
    }

    public interface ISongRepository :IRepository<Song>
    {
    }
}
