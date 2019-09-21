using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class ApplicationSettings
    {
        public string JWT_Secret
        {
            get; set;
        }
        public string Client_Url
        {
            get; set;
        }
    }
}
