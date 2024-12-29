using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

/*
 ported from the Visual basic version!

 original comments from the Visual Basic version included 
 disabled code has been removed


'Created 24/07/2024 By Roger Williams
'
'used to store rooms for each level in memory
'
'uses a class structure and main program uses list of this class to represent the entire level
'room data stored in a text file which the main program reads and stores each room data into
'its own class object - not very 80s!
'
'text file supports room description lines of upto 120 characters
'
*/
namespace RogTextAdventureCSharp
{
    internal class clsGameRooms
    {
        /*
        Created 24/07/2024 By Roger Williams

        used to store rooms for each level in memory
        */

       public int ID = 0;
        // ''next 4 propoerties determine which room this one leads to 0=no room!
        public int NextRoomNorth = 0;
        public int NextRoomSouth = 0;
        public int NextRoomEast  = 0;
        public int NextRoomWest  = 0;
        //'used for text to describe room to player
        public string Desc1 ="";   
        public string Desc2= "";
        public string Desc3= "";
        public string Desc4= "";
        public string Desc5= "";
        public string Desc6= "";
        public string Desc7= "";
        public string Desc8= "";
        public string Desc9= "";
        public string Desc10= "";
        public string Desc11= "";
        public string Desc12= "";
        public string Desc13= "";
        public string Desc14= "";
        public string Desc15= "";
        public string Desc16= "";
        public string Desc17= "";
        public string Desc18= "";
        public string Desc19= "";
        public string Desc20= "";

        public void Clear()
        {
            /*
            'Created 24/07/2024 By Roger Williams
            '
            'resets class variables
            '
            '
            */
            ID = 0;
            NextRoomNorth = 0;
            NextRoomSouth = 0;
            NextRoomEast = 0;
            NextRoomWest = 0;
            // 'used for text to descrbie room to player
            Desc1 = ""; 
            Desc2 = "";
            Desc3 = "";
            Desc4 = "";
            Desc5 = "";
            Desc6 = "";
            Desc7 = "";
            Desc8 = "";
            Desc9 = "";
            Desc10 = "";
            Desc11 = "";
            Desc12 = "";
            Desc13 = "";
            Desc14 = "";
            Desc15 = "";
            Desc16 = "";
            Desc17 = "";
            Desc18 = "";
            Desc19 = "";
            Desc20 = "";
    }
   }
}
