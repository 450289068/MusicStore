using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Genre Model
// created: Dániel Egyed
// egyed.daniel89@gmail.com

namespace MusicStore.Models
{
    /// <summary>
    /// Az műfajokat reprezentáló Genre Model osztály
    /// Entity FrameWork Code First
    /// Az adatábizist az App_Data mappában tároljuk
    /// </summary>
    public class Genre
    {
        public int GenreId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public List<Album> Albums { get; set; }
    }
}