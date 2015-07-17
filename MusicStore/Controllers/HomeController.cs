using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// HomeController
// created: Dániel Egyed
// egyed.daniel89@gmail.com

namespace MusicStore.Controllers
{
    public class HomeController : Controller
    { 
        /*
          A Controller-ek felelősek a bejövő HTTP kérések feldolgozásáért,
          a felhasználótól érkező inputok kezeléséért, az adatok mentéséért,
          adatok "előkerítéséért", és annak meghatározásáért, hogy milyen választ
          küldjön vissza a kliensnek(HTML dokumentumot megjeleníteni, letölteni
          egy fájlt, átirányítani egy másik oldalra, stb.)
        */

        public ActionResult Index()
        {
            /*
            Közös erőforrások elérése a /Views/Shared mappa _Layout.cshtml
            fájl segítségével történhet. A @RenderBody() feletti sorokba írva
            minden View kap egy alapértelmezett tartalmat(pl.: fejlécet)
            Ha dinamikus oldalt szeretnénk készíteni, akkor a controller
            műveletekből kell információkat átadnunk a sablonoknak.
            Az ActionResult típusú controller műveletek átadhatnak egy model
            objektumot a view sablonnak. Ez lehetővé teszi a controller műveletnek,
            hogy minden szükséges információt egy csomagban elküldje a view-nak,
            ami majd legenerálja a megfelelő HTML tartalmat.
            */

            return View();
        }
    }
}