using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;

// StoreController
// created: Dániel Egyed
// egyed.daniel89@gmail.com

namespace MusicStore.Controllers
{
    /// <summary>
    /// A StoreController segítségével megnézhetjük a megvásárolható albumokat.
    /// 3 funkcióval bír ez a controller:
    ///     -Műfajok kilistázása
    ///     -Albumok kilistázása egy adott műfajon belül
    ///     -Megmutatja a részleteket egy adott albumról
    /// </summary>
    public class StoreController : Controller
    {
        //Az adatbázis kontextus egy példányát tároló storeDB(adatbázis műveletekhez)
        MusicStoreEntities storeDB = new MusicStoreEntities();

        /// <summary>
        /// Az Index() Controller művelet(más néven controller action-t) fogjuk
        /// használni a műfajok kilistázására.
        ///    -A Controller műveletek feladata, hogy válaszoljanak az URL kérésekre,
        ///    s eldöntsék, hogy milyen tartalmat kell visszaküldeni a böngészőbe.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
                var genrelist = storeDB.GenresContext.ToList();
                //return "Hello StoreController.Index()";
                return View(genrelist);
        }

        /// <summary>
        /// A Browse() Controller művelet kinyeri az URL-ből a querystring-et:
        /// (.../Store/Browse?genre=Disco URL esetén: genre=Disco).
        /// Hozzáadunk egy genre paramétert, ekkor az ASP.MVC automatikusan átadja
        /// a számára a genre-hez tartozó értéket(Disco) a querystring-ből,
        /// vagy POST-al érkező űrapból
        /// Get: Store/Browse/Pop vagy Store/Browse?genre=Pop
        /// </summary>
        /// <param name="genre"></param>
        /// <returns></returns>
        public ActionResult Browse(string genre)
        {
                //A HttpUtility.HtmlEncode függvény megakadályozza a JS injection-t
                //pl.:/Store/Browse?genre=<script>window.location=’http://hackersite.com’</script>.
            /*
                string message = HttpUtility.HtmlEncode("Store.Browse, Genre = " + genre);
                return message;
            */
            /*
                var genreModel = new Genre { Name = genre };
                return View(genreModel);
           */
            /*
                A Single LINQ művelettel egyetlen eredménnyel térünk vissza.
                A Single művelet egy lambda kifejezést vár paraméterként,
                hogy egyetlen Genre objektumot szeretnénk visszakapni, melynek
                Name tulajdonsága megegyezik azzal, amit megadtunk(genre)
                Az adatbázis hozzáférések számát csökkenthetjük, ha a Genre
                objektum mellé más entitást is lekérünk(Query Result Shaping)
                Az Include művelet segítségével a Genre objektumok mellett
                az Albums listát is lekérjük.
            */
                var genreModel = storeDB.GenresContext.Include("Albums").Single(g => g.Name == genre);
                return View(genreModel);
        }
        /// <summary>
        /// A Details() Controller művelet kinyeri az URL-ből a querystring-et:
        /// (.../Store/Details?id=6 URL esetén: id=6).
        /// Hozzáadunk egy id paramétert, ekkor az ASP.MVC automatikusan átadja
        /// a számára a id-hez tartozó értéket(6) a querystring-ből,
        /// vagy POST-al érkező űrapból
        /// Get: Store/Details/6 (csak id paraméter esetén) vagy Store/Details?id=6
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
                /*
                    string message = HttpUtility.HtmlEncode("Store.Details, id=" + id);
                    return message;
                */
                /*
                var album = new Album { Title = "Album " + id };
                return View(album);
                 */
                
                //Megadott id alapján visszaadjuk az albumot
                var albumModel = storeDB.AlbumsContext.Find(id);
                return View(albumModel);
        }
    }
}
