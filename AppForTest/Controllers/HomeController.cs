using AppForTest.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppForTest.Controllers
{
    public class HomeController : Controller
    {
        //path to txt files
        string path = AppDomain.CurrentDomain.BaseDirectory + @"texts/";
        SentContext db = new SentContext();
        public ActionResult Index(int page = 1)
        {
            int pageSize = 10;
            //Get sentences on current page
            IEnumerable<Sentence> SentPerPage = db
                .Sentences
                .OrderByDescending(m => m.id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                pageNumber = page,
                pageSize = pageSize,
                totalItems = db.Sentences.Count()
            };
            string[] TxtNameArr = FindTexts();
            if (TxtNameArr!=null)
            {
                ViewBag.Texts = TxtNameArr;
            }
            MainView model = new MainView
            {
                sentences=SentPerPage,
                PageInfo=pageInfo
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(MainView model)
        {
            string txt;
            try
            {
                 txt = System.IO.File.ReadAllText(path + model.txtName);
            }
            catch
            {
                return RedirectToAction("Index");
            }
            //every dot is new sentence in another array item
            string[] sentArr = txt.Split('.');
            //make pattern from entered search word
            string pattern = String.Format("({0})", model.word);
            Regex rg = new Regex(pattern);
            foreach(var sent in sentArr)
            {
                //Collect every matches
                MatchCollection matches = rg.Matches(sent);
                if (matches.Count != 0)
                {
                    Sentence sentence = new Sentence
                    {
                        matchs = matches.Count,
                        searchhWord=model.word,
                        sentence=Reverse(sent)+'.'
                    };
                    db.Sentences.Add(sentence);
                    await db.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index");
        }
        
        private string Reverse(string str)
        {
            char[] charArr = str.ToCharArray();
            return new string(charArr.Reverse().ToArray());
        }
        //parse all txt files in folder
        private string[] FindTexts()
        {
            if (!Directory.Exists(path)) 
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch(Exception e)
                {
                    Console.WriteLine("Can`t create directory: {0}",e.Message);
                }
            }
            if (Directory.Exists(path))
            {
                var temp = Directory
                    .GetFiles(path, "*.txt", SearchOption.AllDirectories)
                    .Select(Path.GetFileName)
                    .ToArray();
                return temp;
            }
            else return null;

        }
    }

}