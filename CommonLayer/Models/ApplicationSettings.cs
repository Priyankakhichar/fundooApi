////-------------------------------------------------------------------------------------------------------------------------------
////<copyright file = "ApplicationSettings.cs" company ="Bridgelabz">
////Copyright © 2019 company ="Bridgelabz"
////</copyright>
////<creator name ="Priyanka khichar"/>
////
////-------------------------------------------------------------------------------------------------------------------------------
namespace CommonLayer.Models
{
    /// <summary>
    /// application seeting class for jwt
    /// </summary>
    public class ApplicationSettings
    {
        /// <summary>
        /// getting and seeting the jwt secret value
        /// </summary>
        public string JWT_Secret
        {
            get; set;
        }

        /// <summary>
        /// getting and setting the value of client url
        /// </summary>
        public string Client_Url
        {
            get; set;
        }
    }
}
