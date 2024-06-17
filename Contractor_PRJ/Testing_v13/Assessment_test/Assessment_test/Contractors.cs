using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment_test
{
    
    public class Contractors
    {

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        /// <summary>
        /// Class for defining a contractor in the organisation
        /// </summary>
        /// <param name="id">Unique identifier for each contractor</param>
        /// <param name="firstName">Given name</param>
        /// <param name="lastName">Family name</param>
        public Contractors(int id, string firstName, string lastName)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
        }
        public override string ToString()
        {
            return ID + " | " + FirstName + " " + LastName; 
        }

    }

   
}
