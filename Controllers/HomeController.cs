using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Ajax_Notes
{
    public class HomeController : Controller
    {
        private DbConnector cxn;

        public HomeController()
        {
            cxn = new DbConnector();
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            List<Dictionary<string, object>> AllNotes = cxn.Query("SELECT * FROM notes");
            ViewBag.Notes = AllNotes;
            return View();
        }

        [HttpPost]
        [Route("notes/save")]
        public IActionResult Save(string title)
        {
            string content = "Insert Content...";
            string query = $"INSERT INTO notes (title, content, created_at, updated_at) VALUES ('{title}', '{content}', NOW(), NOW())";
            cxn.Execute(query);
            string note_string = "<form><label>" + title + "</label></form>";

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("notes/{id}/delete")]
        public IActionResult Delete(string id)
        {
            string query = $"DELETE FROM notes WHERE id={id}";
            cxn.Execute(query);
            return RedirectToAction("Index");
        }
    }
}