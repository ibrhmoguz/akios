using System.Data;

namespace Kobsis.DataType
{
    public class DbParametre
    {
        public string ParametreAdi { get; set; }
        public object ParametreDegeri { get; set; }
        public SqlDbType VeriTipi { get; set; }
        public int ParametreBoyutu { get; set; }
        public ParameterDirection? ParametreYonu { get; set; }

        public DbParametre()
        {
            this.ParametreYonu = null;
        }
    }
}
