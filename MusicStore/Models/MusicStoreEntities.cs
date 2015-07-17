using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

// MusicStoreEntities Model
// created: Dániel Egyed
// egyed.daniel89@gmail.com

namespace MusicStore.Models
{
    /// <summary>
    /// A MusicStoreEntities osztály fogja reprezentálni az
    /// Entity Framework kontextusát, amelynek segítségével
    /// lekérdezhetjük és manipulálhatjuk az adatbázisban lévő adatokat.
    /// </summary>
    public class MusicStoreEntities : DbContext
    {
        public DbSet<Album> AlbumsContext { get; set; }
        public DbSet<Genre> GenresContext { get; set; }
    }
}