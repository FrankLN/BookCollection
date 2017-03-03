using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookCollection.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace BookCollection.Controllers
{
    [Authorize]
    public class BookCollectionController : Controller
    {
        private DefaultConnectionEntities db = new DefaultConnectionEntities();

        private List<BookCollectionViewModel> _data = new List<BookCollectionViewModel>
        {
            new BookCollectionViewModel
            {
                Id = 1,
                Name = "Disney Books",
                Books = new List<Book> { new Book { Id=001, Name="Snehvide", PublishYear="1970"}, new Book { Id=002, Name="Tornerose", PublishYear="1974"} }
            },

            new BookCollectionViewModel
            {
                Id = 2,
                Name = "Animal Books",
                Books = new List<Book> { new Book { Id = 001, Name = "Havet er skønt", PublishYear = "1970" } }
            }
        };

        public BookCollectionController()
        {
            foreach(var item in _data)
            {
                item.calculateNumberOfBooks();
            }
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: BookCollection
        public ActionResult Index()
        {
            var userId = UserManager.FindById(User.Identity.GetUserId()).Id;

            var model = db.BooklistDetailView.Where(m => m.Id == userId);
           
            return View(model);
        }

        // GET: BookCollection/Details/1
        public ActionResult Details(int? booklistId)
        {
            if (booklistId != null)
            {
                var model = db.BooklistDetailView.Where(m => m.Id == UserId && m.booklistId == booklistId);

                if(model.Count() > 0)
                {
                    return View(model);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: BookCollection/CreateNewBooklist
        public ActionResult CreateNewBooklist()
        {
            return View();
        }

        // POST: BookCollection/CreateNewBooklist
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateNewBooklist(BookCollectionViewModel model)
        {
            var newBooklist = new Booklists { booklistName = model.Name };
            var booklist = db.Booklists.Add(newBooklist);

            var newLink = new linkAspNetUsersBooklists { aspNetUserId = UserId, booklistId = booklist.booklistId };
            db.linkAspNetUsersBooklists.Add(newLink);

            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: BookCollection/CreateNewBook
        public ActionResult CreateNewBook(int? booklistId)
        {
            if(booklistId != null)
            {
                ViewBag.booklistId = booklistId;
                return View();
            }
            return RedirectToAction("Index");
        }

        // POST: BookCollection/CreateNewBook
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateNewBook(Books model, int? booklistId)
        {
            if(model != null && booklistId != null)
            {
                var newBook = db.Books.Add(model);
                var newLink = new linkBooklistsBooks { bookId = newBook.bookId, booklistId = (int)booklistId };
                db.linkBooklistsBooks.Add(newLink);

                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { booklistId = booklistId });
            }

            return RedirectToAction("Index");
        }


        // POST: BookCollection/ToggleBookOwned
        [HttpPost]
        public ActionResult ToggleBookOwned(int id)
        {
            Books book = db.Books.Where(m => m.bookId == id).FirstOrDefault();

            book.bookOwned = !book.bookOwned;
            db.SaveChanges();
            return null;
        }

        #region Helpers

        public string UserId { get { return UserManager.FindById(User.Identity.GetUserId()).Id; } }

        #endregion
    }
}