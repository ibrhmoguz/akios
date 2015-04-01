using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace WebFrame.DataType.Common.ExceptionHandling
{
   public class ExceptionHander:IExceptionHandler
    {


       public ExceptionHander()
       {
           
       }

       public void HandleException<T>(T ex) where T :Exception
       {
           



       }

      

    }
}
