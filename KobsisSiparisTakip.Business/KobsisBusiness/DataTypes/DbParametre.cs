using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KobsisSiparisTakip.Business.DataTypes
{
    public class DbParametre
    {
        public string ParametreAdi { get; set; }
        public object ParametreDegeri { get; set; }
        public SqlDbType VeriTipi { get; set; }
        public int ParametreBoyutu { get; set; }
    }
}
