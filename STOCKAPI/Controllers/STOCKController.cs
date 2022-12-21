using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Avapi;
using Avapi.AvapiTIME_SERIES_INTRADAY;
using STOCKAPI.Models;

namespace STOCKAPI.Controllers
{
    public class STOCKController : ApiController
    {

     
        public static STOCK GetSTOCK(string mack)
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
        //public  STOCK a = GetSTOCK();
        public List<STOCK> test = new List<STOCK>();
        public static STOCK getbaocao(string mack, int gio)
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
            foreach (var item in data.TimeSeries)
            {

                if (Convert.ToDateTime(item.DateTime).Hour == gio && double.Parse(item.volume)>=kltb)   
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
        public static STOCK getkhoang(string mack, DateTime giobatdau,DateTime giokethuc)
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

            int dem = 0; double kltb = 0 ;
            foreach (var item in data.TimeSeries)
            {
                if (Convert.ToDateTime(item.DateTime) >= giobatdau && Convert.ToDateTime(item.DateTime) <= giokethuc)
                {
                    dem++;
                    kltb +=double.Parse(item.volume);
                }
            }

            double klss = kltb / dem;
            foreach (var item in data.TimeSeries)
            {

                if (Convert.ToDateTime(item.DateTime)>=giobatdau && Convert.ToDateTime(item.DateTime)<=giokethuc && double.Parse(item.volume)>=klss)
                {
                    

                    PriceHis pr = new PriceHis();
                    pr.Volume = int.Parse(item.volume);
                    pr.Openprice = double.Parse(item.open);
                    pr.Closeprice = double.Parse(item.close);
                    pr.Time = Convert.ToDateTime(item.DateTime); //DateTime.ParseExact(item.DateTime, "MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture);
                    pr.High = double.Parse(item.high);
                    pr.Low = double.Parse(item.low);
                    stkk.PriceHis.Add(pr);
                }
            }
            return stkk;
        }


        /// <summary>
        /// Xem thông tin mã chứng khoán
        /// </summary>
        // GET: api/STOCK
        [ResponseType(typeof(IEnumerable<STOCK>))]
        public IEnumerable<STOCK> Get(string mack)
        {
            test.Add(GetSTOCK(mack));
            return test;
        }

        /// <summary>
        /// Xem thông tin đột biến khối lượng giao dịch mã chứng khoán theo mốc thời gian nhất định
        /// </summary>
        // GET: api/STOCK/time
        public List<STOCK> Get(string mack,int times)
        {
            test.Add(getbaocao(mack, times));
            return test ;
        }

        /// <summary>
        /// Xem thông tin đột biến khối lượng giao dịch mã chứng khoán theo một khoảng thời gian
        /// </summary>
        // GET: api/STOCK/time
        // GET: api/STOCK/time
        public List<STOCK> Gets(string mack,DateTime ngaybd,DateTime ngaykt)
        {
            test.Add(getkhoang(mack, ngaybd, ngaykt));
            return test;
        }
       
    }
}
