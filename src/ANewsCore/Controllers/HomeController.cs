using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Enities;
using Core.Services;
using Core.Enums;


using Core.Services;
using Core.Enums;
using Core.Services;
using Core.Enums;


namespace ANewsCore.Controllers
{
    public class HomeController : Controller
    {
        //public static ManualResetEvent allDone = new ManualResetEvent(false);
        string htmlStr;
        [NonAction]
        public ActionResult Index(int status = 0, int page = 1, int pageSize = 100, List<int> statuses = null, int groupID = 0, List<int> managers = null, bool sortbalance = false)
        {

            //ClientsVM model = GetClients(status, page, pageSize, statuses, groupID, managers, sortbalance);


            //model.Managers = LoginService.GetManagers();
            //model.Managers.Insert(0, new LoginAndRoles() { ID = -1, Name = "Не привязан" });
            //model.Managers.Insert(0, new LoginAndRoles() { ID = 0, Name = "Все" });

            var model = new { Periods = new List<string>(), Resources = new List<string>(), Requests = new List<string>(), News = new List<List<string>>() };

            //model.Periods = new List<string>();
            model.Periods.Add("Не учитывать");
            model.Periods.Add("За 24 часа");
            model.Periods.Add("За неделю");
            model.Periods.Add("За месяц");

            model.Resources.Add("www.rbc.ru/economics/");

            model.Resources.Add("www.rbc.ru/politics/");
            model.Resources.Add("www.business-gazeta.ru/news-list");
            model.Resources.Add("www.business-gazeta.ru/razdel/1");

            model.Requests.Add("Бизнес");
            model.Requests.Add("Технологии");
            model.Requests.Add("Арский район");

            model.News.Add(GetNews("За 24 часа", model.Resources, model.Requests));

            return View(model);
        }

        [NonAction]
        private List<string> GetNews(string Period, List<string> Resources, List<string> Requests)
        {
            Dictionary<string, string> TimeTranslate = new Dictionary<string, string>();

            TimeTranslate.Add("Не учитывать", "");
            TimeTranslate.Add("За 24 часа", "&tbs=qdr:d");
            TimeTranslate.Add("За неделю", "&tbs=qdr:w");
            TimeTranslate.Add("За месяц", "&tbs=qdr:m");

            List<string> result = new List<string>();
            foreach (string url in Resources)
            {
                foreach (string request in Requests)
                {
                    string searchRequest = "https://www.google.ru/search?q=" + request.Trim().Replace(" ", "+")
                        + "+" + /*site:*/url.Trim().Replace("https://", "").Replace("http://", "")
                        + "&newwindow=1&espv=2&biw=1366&bih=638&tbas=0&source=lnt" + TimeTranslate[Period]
                        + "&sa=X&ved=0ahUKEwj-473g8dnQAhVDjywKHc8ZB1IQpwUIFQ";

                    foreach (string item in SearchByGoogle.GetInfoBySearchStr(searchRequest))
                    {
                        result.Add(item);
                    }
                }
            }

            return result;
        }

        public IActionResult Index()
        {
            //если куки
            return RedirectToAction("parseUrl", new { time = DateTime.Now.AddDays(-1) });
            //return View();
        }

        public List<TParsedNews> GarbageAll(string login)
        {
            //get db
            //parse with time filter
            //compare

            //parse all
            return null;

        }
        public List<List<string>> parseUrl(DateTime time)//TParsedNews   //,string url
        {
            var model = new { Periods = new List<string>(), Resources = new List<string>(), Requests = new List<string>(), News = new List<List<string>>() };
            //model.Resources.Add("www.business-gazeta.ru/razdel/1");
            model.Resources.Add("www.rbc.ru/economics/");

            model.Requests.Add("Бизнес");
           // model.Requests.Add("Технологии");
          //  model.Requests.Add("Арский район");

            model.News.Add(GetNews("За 24 часа", model.Resources, model.Requests));
            return model.News;
        }










        public IActionResult Login(string login)
        {
            ViewData["Message"] = "Your application description page.";//тодо
            List< TParsedNews > ParsedN=new List<TParsedNews>();
            using (var DB = new MyDbContext())
            {
                var result = DB.TLoginFavNews.Where(_ => _.TLogin.Login == login).Select(_ => _.TFavoriteNews).ToList();
                //var r2 = DB.TLoginFavNews.Where(_ => _.TLogin.Login == login).SelectMany(_ => _.TFavoriteNews.TParsedNews).ToList();//.Select(_ => _.TFavoriteNews).ToList().Select(_=>_.;
                //var r3 = DB.TLoginFavNews.Where(_ => _.TLogin.Login == login).SelectMany(_ => _.TFavoriteNews.).Single();//.Select(_ => _.TFavoriteNews).ToList().Select(_=>_.;
                
               
                return View("~/Views/Home/Index.cshtml", new List<TFavoriteNews> { result.SingleOrDefault() });//TODO
            }
        }

        public List<TFavoriteNews> GetFavoriteNews(string login)
        {
            using (var DB = new MyDbContext())
            {//?
                var a=DB.TLoginFavNews.Where(_ => _.TLogin.Login == login).Select(_ => _.TFavoriteNews).ToList();
                return a;
            }
            //parse?
        }
        public List<TFavoriteWords> GetFavoriteWords(string login)
        {
            using (var DB = new MyDbContext())
            {//?
                return DB.TFavoriteWords.Where(_ => _.TLogin.Login == login).ToList();
            }
        }
        public List<TParsedNews> GetTParsedNews(string login)//?or parse
        {

            using (var DB = new MyDbContext())
            {//?
                var fnews = this.GetFavoriteNews(login);
                return DB.TParsedNews.Where(_ => fnews.Contains(_.TFavoriteNews)).ToList();
            }
        }

        public bool AddLogin(string login)
        {
            using (var DB = new MyDbContext())
            {
                if (DB.TLogin.Where(_ => _.Login == login).Count() != 0) return false;//todo
                DB.TLogin.Add(new TLogin { Login=login});
                DB.SaveChanges();
                return true;
            }
        }
        public bool AddFavoriteNews(string login, string link, int type)
        {
            using (var DB = new MyDbContext())
            {//?
                DB.TLoginFavNews.Where(_ => _.TLogin.Login == login).Select(_ => _.TFavoriteNews).ToList()
                    .Add(new TFavoriteNews { Link = link, Type = type });
                DB.SaveChanges();
                return true;
            }
            //parse?
        }
        public bool AddFavoriteWords(string login, string word)
        {
            using (var DB = new MyDbContext())
            {//?
                var tlogin =DB.TLogin.First(_ => _.Login == login);//.Select(_ => _.TFavoriteNews).ToList();

                DB.TFavoriteWords.Where(_ => _.TLogin.Login == login).ToList()
                    .Add(new TFavoriteWords { Data = word, TLogin=tlogin });
                DB.SaveChanges();
                return true;
            }
        }
        //public bool AddTParsedNews(string login)//?or parse
        //{

        //    using (var DB = new MyDbContext())
        //    {//?
        //        var fnews = this.GetFavoriteNews(login);
        //        return DB.TParsedNews.Where(_ => fnews.Contains(_.TFavoriteNews)).ToList();
        //    }
        //}



        //public IActionResult Register()
        //{
        //    return View();
        //}

        public IActionResult Register(string login, TFavoriteNews favN)
        {
            using (var DB = new MyDbContext())
            {
                //передать enums во views как описаание
                var TLogin =new TLogin() { Login = login };
                var TFavoriteNews = new TFavoriteNews() { Link = favN.Link, Type = favN.Type };
                DB.TLoginFavNews.Add(new TLoginFavNews() { TLogin = TLogin ,TFavoriteNews=TFavoriteNews});
                //SaveFavoriteNews(TLogin, favN);
                DB.SaveChanges();
                return RedirectToAction("login", "Home",new { login = login });
            }
        }

        public void SaveFavoriteNews(TLogin TLogin, TFavoriteNews favN)
        {
            //foreach (var favN in favNews)
            using (var DB = new MyDbContext())
            {
                var TFavoriteNews = DB.TFavoriteNews.Add(new TFavoriteNews() { Link = favN.Link, Type = favN.Type });

                DB.TLoginFavNews.Add(new TLoginFavNews() { TLogin = TLogin, TFavoriteNews = TFavoriteNews.Entity });
            }       
        }

        public IActionResult Error()
        {
            return View();
            //return RedirectToAction("Register", "Home");
        }
        
    }
}
