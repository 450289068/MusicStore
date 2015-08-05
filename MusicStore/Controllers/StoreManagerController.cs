using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicStore.Models;

// StoreManagerController
// created: Dániel Egyed
// egyed.daniel89@gmail.com

namespace MusicStore.Controllers
{
    /// <summary>
    /// Az ASP.NET ismeri a scaffolding paradigmát, vagyis a fordító
    /// maga képes előállítani a CRUD műveletekhez
    /// (Create-Retrieve-Update-Delete) szükséges kódot.
    /// "Controller with read/write actions"
    /// </summary>
    public class StoreManagerController : Controller
    {
        //Az adatbázis kontextus egy példányát tároló db(adatbázis műveletekhez)
        private MusicStoreEntities db = new MusicStoreEntities();

        // GET: StoreManager
        /// <summary>
        /// Az Index() view megkapja az Albumok listáját, akárcsak a hozzájuk tartozó
        /// műfaj(Genre) és előadó(Artist) adatokat, vagyis ahelyett, hogy az
        /// egyes listákat külön adatbázis kapcsolaton keresztül érné el,
        /// egyetlen alkalomm kell csak kapcsolódnia.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var albumsContext = db.AlbumsContext.Include(a => a.Artist).Include(a => a.Genre);
            return View(albumsContext.ToList());
        }

        // GET: StoreManager/Details/5
        /// <summary>
        /// A Details(int) megkeresi az albumot az ID mező alapján,
        /// és átadja azt a view-nak
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.AlbumsContext.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // GET: StoreManager/Create
        /// <summary>
        /// A Create() művelet adatbevitelt vár a felhasználótól.
        /// Lekérjük az üres űrlapot egy HTTP-GET kéréssel.
        /// A ViewBag objektum lehetővé teszi, hogy adatokat juttassunk el a
        /// view()-nak anélkül, hogy erősen típusos model objektumokat használnánk.
        /// Átadjuk az előadók és a műfajok listáját, s erre a legjobb megoldás,
        /// ha beletesszük a ViewBag-be mindkét listát.
        /// A ViewBag attól dinamikus objektum, hogy tetszőlegesen használhatunk
        /// olyan Property-ket is, amelyeket nem is definiáltunk. ( pl. ViewBag.Akarmi)
        /// Az űrlapon a listákból kiválasztott elemek ArtistId és GenreId
        /// néven utaznak tovább. Ezek az értékek SelectList objektumban térnek vissza
        /// az űrlaphoz.
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            //A SelectList 3 paramétere(SelectList(db.Artists, "ArtistId", "Name") esetén):
            //1.: Az előadók listája, mely az űrlapon egy dropdownlist vezérlőben jelenik meg
            //2.: A lista aktuális értéke(SelectedValue)(amit átadunk, Előadó id-ja)
            //3.: Amit megjelenítünk(Elődó neve)
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name");
            ViewBag.GenreId = new SelectList(db.GenresContext, "GenreId", "Description");
            return View();
        }

        // POST: StoreManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.AlbumsContext.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(db.GenresContext, "GenreId", "Description", album.GenreId);
            return View(album);
        }

        // GET: StoreManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.AlbumsContext.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(db.GenresContext, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // POST: StoreManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(db.GenresContext, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // GET: StoreManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.AlbumsContext.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: StoreManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.AlbumsContext.Find(id);
            db.AlbumsContext.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}