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
        public string Save(string title)
        {
            string content = "Insert Content...";
            string query = $"INSERT INTO notes (title, content, created_at, updated_at) VALUES ('{title}', '{content}', NOW(), NOW())";
            cxn.Execute(query);
            List<Dictionary<string, object>> note = cxn.Query($"SELECT id, title, content, created_at, updated_at FROM notes WHERE title='{title}'");
            string note_string = @"<form class='note' action='notes/" + note[0]["id"] + "'><label>" + title + "</label><input type='submit' value='delete'><p>Insert Content...</p></form>";
            
            return note_string;
        }

        [HttpPost]
        [Route("notes/{id}/delete")]
        public string Delete(string id)
        {
            string query = $"DELETE FROM notes WHERE id={id}";
            cxn.Execute(query);
            return "Success!";
        }

        [HttpPost]
        [Route("notes/{id}/update")]
        public string Update(string id, string content)
        {
            string query = $"UPDATE notes SET content=\"{content}\" WHERE id={id}";
            cxn.Execute(query);
            return "Success!";
        }
    }
}