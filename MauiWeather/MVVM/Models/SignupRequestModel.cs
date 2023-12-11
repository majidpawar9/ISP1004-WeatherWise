using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/* Model class for Signup, this takes in just the email and password
 * Firebase signup will be implemented soon
**/

namespace WeatherWise.MVVM.Models
{
    public class SignupRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
