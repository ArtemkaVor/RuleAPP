using RuleAPP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleAPP.Controller
{
    class DbConnection
    {
        private static Model.TradeEntities s_firstDBEntities;

        public static Model.TradeEntities GetContext()
        {
            if (s_firstDBEntities == null)
            {
                s_firstDBEntities = new Model.TradeEntities();
            }
            return s_firstDBEntities;
            
        }
    }
}
