using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acconting.ViewModle.Acconting;
using Acconting.DateLayer.Context;
namespace Acconting.Businus
{
    public class Accont
    {
        public static ReposrtNewModle reportformMain()
        {
            ReposrtNewModle rp = new ReposrtNewModle();
            using (UnitOfWork db = new UnitOfWork())
            {
                DateTime startdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01);
                DateTime enddate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 29);

                var rescievs = db.AccontingRepository.Get(a => a.TypeID == 1 && a.DateTime <= startdate && a.DateTime >= enddate).Select(a => a.Amount).ToList();
                var pay = db.AccontingRepository.Get(a => a.TypeID == 2 && a.DateTime <= startdate && a.DateTime >= enddate).Select(a => a.Amount).ToList();

                rp.Receive = rescievs.Sum();
                rp.Pay = pay.Sum();
                rp.AccontingBalance = (rescievs.Sum() - pay.Sum());
            }
            return rp;
        }
    }
}
