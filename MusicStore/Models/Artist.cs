using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// Artist Model
// created: Dániel Egyed
// egyed.daniel89@gmail.com

namespace MusicStore.Models
{
    /// <summary>
    /// Az előadókat reprezentáló Artist Model osztály
    /// Entity FrameWork Code First
    /// Az adatábizist az App_Data mappában tároljuk
    /// </summary>
    public class Artist
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }
    }
}