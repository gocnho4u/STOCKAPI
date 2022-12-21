using Avapi;
using Avapi.AvapiTIME_SERIES_INTRADAY;
using STOCKAPI.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace STOCKAPI.Controllers
{
    public class HomeController : Controller
    {
        public static STOCK xemmack(string mack)
        {

            // Ket noi voi AVA
            IAvapiConnection connection = AvapiConnection.Instance;

            // lấy API(ALPHAVANTAGE hoặc AVAPI)
            connection.Connect("f95848e5243fafee7e535d6db4f8a7d227346cc3");
            // Lấy obj cần sử dụng, gọi thư viện
            Int_TIME_SERIES_INTRADAY time_series_intraday = connection.GetQueryObject_TIME_SERIES_INTRADAY();

            // request token cần
            IAvapiResponse_TIME_SERIES_INTRADAY time_series_intradayResponse =
            time_series_intraday.Query(mack, Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_interval.n_60min);

            var data = time_series_intradayResponse.Data;

            STOCK stock = new STOCK();
            stock.Mack = mack;
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //List<STOCK> listStock = (List<STOCK>)serializer.Deserialize<List<STOCK>>()

            foreach (var item in data.TimeSeries)
            {
                PriceHis priceHis = new PriceHis();
                priceHis.Volume = int.Parse(item.volume);
                priceHis.Openprice = double.Parse(item.open);
                priceHis.Closeprice = double.Parse(item.close);
                priceHis.Time = Convert.ToDateTime(item.DateTime); //DateTime.ParseExact(item.DateTime, "MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture);
                priceHis.High = double.Parse(item.high);
                priceHis.Low = double.Parse(item.low);
                stock.PriceHis.Add(priceHis);


            }
            return stock;
        }

        public static STOCK dotbiengio(string mack, int gio)
        {
            // Ket noi voi AVA
            IAvapiConnection connection = AvapiConnection.Instance;

            // lấy API(ALPHAVANTAGE hoặc AVAPI)
            connection.Connect("f95848e5243fafee7e535d6db4f8a7d227346cc3");
            // Lấy obj cần sử dụng, gọi thư viện
            Int_TIME_SERIES_INTRADAY time_series_intraday = connection.GetQueryObject_TIME_SERIES_INTRADAY();

            // request token cần
            IAvapiResponse_TIME_SERIES_INTRADAY time_series_intradayResponse =
            time_series_intraday.Query(mack, Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_interval.n_60min);

            var data = time_series_intradayResponse.Data;

            STOCK stks = new STOCK();
            stks.Mack = mack;
            double klt = 0; int dem = 0;
            foreach (var item in data.TimeSeries)
            {
                if (Convert.ToDateTime(item.DateTime).Hour == gio && Convert.ToDateTime(item.DateTime).Minute == 30)
                {
                    klt = klt + int.Parse(item.volume); dem++;
                }
                
            }
            double kltb = klt / dem;
            foreach (var item in data.TimeSeries)
            {

                if (Convert.ToDateTime(item.DateTime).Hour == gio && double.Parse(item.volume) >= kltb)
                {
                    PriceHis prhis = new PriceHis();
                    prhis.Volume = int.Parse(item.volume);
                    prhis.Openprice = double.Parse(item.open);
                    prhis.Closeprice = double.Parse(item.close);
                    prhis.Time = Convert.ToDateTime(item.DateTime); //DateTime.ParseExact(item.DateTime, "MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture);
                    prhis.High = double.Parse(item.high);
                    prhis.Low = double.Parse(item.low);
                    stks.PriceHis.Add(prhis);

                }

            }
            return stks;
        }
        public static STOCK canhbaovunggia(string mack, double vunggiatren, double vunggiaduoi)
        {
            // Ket noi voi AVA
            IAvapiConnection connection = AvapiConnection.Instance;

            // lấy API(ALPHAVANTAGE hoặc AVAPI)
            connection.Connect("f95848e5243fafee7e535d6db4f8a7d227346cc3");
            // Lấy obj cần sử dụng, gọi thư viện
            Int_TIME_SERIES_INTRADAY time_series_intraday = connection.GetQueryObject_TIME_SERIES_INTRADAY();

            // request token cần
            IAvapiResponse_TIME_SERIES_INTRADAY time_series_intradayResponse =
            time_series_intraday.Query(mack, Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_interval.n_60min);

            var data = time_series_intradayResponse.Data;
            STOCK stkk = new STOCK();
            stkk.Mack = mack;

            foreach (var item in data.TimeSeries)
            {

                if (double.Parse(item.close) > vunggiaduoi && double.Parse(item.close) < vunggiatren && Convert.ToDateTime(item.DateTime) > DateTime.Today.AddDays(-10))
                {
                    PriceHis pr = new PriceHis();
                    pr.Volume = int.Parse(item.volume);
                    pr.Openprice = double.Parse(item.open);
                    pr.Closeprice = double.Parse(item.close);
                    pr.Time = Convert.ToDateTime(item.DateTime); 
                    pr.High = double.Parse(item.high);
                    pr.Low = double.Parse(item.low);
                    stkk.PriceHis.Add(pr);
                }
            }
            return stkk;
        }
        public static STOCK getdotbienkhoang(string mack,DateTime ngaybd,DateTime ngaykt)
        {
            // Ket noi voi AVA
            IAvapiConnection connection = AvapiConnection.Instance;

            // lấy API(ALPHAVANTAGE hoặc AVAPI)
            connection.Connect("f95848e5243fafee7e535d6db4f8a7d227346cc3");
            // Lấy obj cần sử dụng, gọi thư viện
            Int_TIME_SERIES_INTRADAY time_series_intraday = connection.GetQueryObject_TIME_SERIES_INTRADAY();

            // request token cần
            IAvapiResponse_TIME_SERIES_INTRADAY time_series_intradayResponse =
            time_series_intraday.Query(mack, Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_interval.n_60min);

            var data = time_series_intradayResponse.Data;
            STOCK stkss = new STOCK();
            stkss.Mack = mack;
            int dem = 0; double kltb = 0;
            foreach (var item in data.TimeSeries)
            {
                if (Convert.ToDateTime(item.DateTime) >= ngaybd && Convert.ToDateTime(item.DateTime) <= ngaykt)
                {
                    dem++;
                    kltb += double.Parse(item.volume);
                }
            }

            double klss = kltb / dem;
            foreach (var item in data.TimeSeries)
            {

                if (Convert.ToDateTime(item.DateTime) >= ngaybd && Convert.ToDateTime(item.DateTime) <= ngaykt && double.Parse(item.volume) >= klss)
                {


                    PriceHis pr = new PriceHis();
                    pr.Volume = int.Parse(item.volume);
                    pr.Openprice = double.Parse(item.open);
                    pr.Closeprice = double.Parse(item.close);
                    pr.Time = Convert.ToDateTime(item.DateTime); //DateTime.ParseExact(item.DateTime, "MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture);
                    pr.High = double.Parse(item.high);
                    pr.Low = double.Parse(item.low);
                    stkss.PriceHis.Add(pr);
                }
            }
            return stkss;
        }
        public static double kldb(string mack, int gio)
        {
            // Ket noi voi AVA
            IAvapiConnection connection = AvapiConnection.Instance;

            // lấy API(ALPHAVANTAGE hoặc AVAPI)
            connection.Connect("f95848e5243fafee7e535d6db4f8a7d227346cc3");
            // Lấy obj cần sử dụng, gọi thư viện
            Int_TIME_SERIES_INTRADAY time_series_intraday = connection.GetQueryObject_TIME_SERIES_INTRADAY();

            // request token cần
            IAvapiResponse_TIME_SERIES_INTRADAY time_series_intradayResponse =
            time_series_intraday.Query(mack, Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_interval.n_60min);

            var data = time_series_intradayResponse.Data;

            STOCK stks = new STOCK();
            stks.Mack = mack;
            double klt = 0; int dem = 0;
            foreach (var item in data.TimeSeries)
            {
                if (Convert.ToDateTime(item.DateTime).Hour == gio)
                {
                    klt = klt + int.Parse(item.volume); dem++;
                }

            }
            double kltb = klt / dem;
            return kltb;
        }

        public ActionResult Index(string mack, int time = 0, double vunggiaduoi = 0, double vunggiatren = 0)
        {
            List<PriceHis> b = new List<PriceHis>();

            if (mack != null && time == 0 && vunggiaduoi == 0 && vunggiatren == 0)
            {
                STOCK a = xemmack(mack);
                foreach (var item in a.PriceHis)
                {
                    b.Add(item);
                }
                ViewBag.tenck = a.Mack;
            }
            else if (mack != null && time != 0 && vunggiaduoi == 0 && vunggiatren == 0)
            {
                STOCK a = dotbiengio(mack, time);
                foreach (var item in a.PriceHis)
                {
                    b.Add(item);
                }
                ViewBag.kltb = kldb(mack, time).ToString();
                ViewBag.tenck = a.Mack;
            }
            else if (mack != null && time == 0 && vunggiaduoi != 0 && vunggiatren != 0)
            {
                STOCK a = canhbaovunggia(mack, vunggiaduoi, vunggiatren);
                foreach (var item in a.PriceHis)
                {
                    b.Add(item);
                }
                ViewBag.tenck = a.Mack;
            }
            
            else b = null;
            return View(b);

        }
    }
}
