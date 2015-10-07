using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**************************************************
* Adrian Paiva, Patrick Gomes Sanches
* 100864588 
* created: Nov 26 2014
* lastEdit: December 3 2014
**************************************************/

namespace xnaAssignment2
{
    [Serializable]
    public class Score
    {
        public String name;
        public float time;

        public Score()
        {

        }
        public Score(float time)
        {
            this.time = time;
            name = Environment.UserName;
        }
        /*
        public String Name
        {
            get
            {
                return name;
            }
            
        }
        public float Time
        {
            get
            {
                return time;
            }

        }
        */
    }
}
