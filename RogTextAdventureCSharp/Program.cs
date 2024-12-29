using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;
using System.Reflection.Emit;

/*
 ported from the Visual basic version!

 original comments from the Visual Basic version included 
 disabled code has been removed

NOTE: why does the program class need to be declared as a local variable otherwise the main sub cant
      use any subs/funcs? What exactly is the benefit of this??

NOTE: the C# version requires the screen be 1 column WIDER than VB!


'Created 23/07/2024 By Roger Williams
'
'text adventure game!
'
'features a basic parser!
'
'This is Phase one of a potential three phase developmet:
'
'Phase One   - basic game design with movement and primitive parser framework
'Phase Two   - add objects into the rooms and ability to interact better parser
'Phase Three - add entities and action rooms where a list of actions is available e.g. run, stop etc.
'
'NOTE: currently levels have 20 lines of descriptive text, allowing for the 42 row screen leaves lines for future use
'
'
*/
namespace RogTextAdventureCSharp
{
    internal class Program
    {
        clsRogParser clsParser = new clsRogParser();                      //'the parser class
        clsGameRooms clsCurRoom = new clsGameRooms();                     //'current rrom player is in  
        List<clsGameRooms> lstRooms = new List<clsGameRooms>();           //'collection of all rooms in the level
        int intCurRoom = 0;                                               //'ID of current room

        private bool CheckIfEnd()
        /*
        'Created 30/07/2024 By Roger Williams
        '
        'checks if there are NO directions the player can move to
        '
        'this means end of game!
        '
        '
		*/
        {
            return clsCurRoom.NextRoomEast == 0 && clsCurRoom.NextRoomNorth == 0 && clsCurRoom.NextRoomSouth == 0 && clsCurRoom.NextRoomWest == 0;
        }

        private void ShowRoom(int intWhat)
        {
            /*
                    'Created 24/07/2024 By Roger Williams
                    '
                    'draws room description fields to console
                    '
            */
            Console.CursorTop = 0;
            Console.Clear();
            //set console for main game look
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Yellow;

            //if 1 show first room and store dara in vars
            if (intWhat == 1)
            {
                intCurRoom = intWhat;
                clsCurRoom = lstRooms[intWhat];
            }
            //'write room description
            Console.WriteLine(clsCurRoom.Desc1);
            Console.WriteLine(clsCurRoom.Desc2);
            Console.WriteLine(clsCurRoom.Desc3);
            Console.WriteLine(clsCurRoom.Desc4);
            Console.WriteLine(clsCurRoom.Desc5);
            Console.WriteLine(clsCurRoom.Desc6);
            Console.WriteLine(clsCurRoom.Desc7);
            Console.WriteLine(clsCurRoom.Desc8);
            Console.WriteLine(clsCurRoom.Desc9);
            Console.WriteLine(clsCurRoom.Desc10);
            Console.WriteLine(clsCurRoom.Desc11);
            Console.WriteLine(clsCurRoom.Desc12);
            Console.WriteLine(clsCurRoom.Desc13);
            Console.WriteLine(clsCurRoom.Desc14);
            Console.WriteLine(clsCurRoom.Desc15);
            Console.WriteLine(clsCurRoom.Desc16);
            Console.WriteLine(clsCurRoom.Desc17);
            Console.WriteLine(clsCurRoom.Desc18);
            Console.WriteLine(clsCurRoom.Desc19);
            Console.WriteLine(clsCurRoom.Desc20);
            Console.WriteLine();
            Console.WriteLine("Enter Command:");
        }

        private void ShowTitle()
        {
        /*
            'Created 23/07/2024 By Roger Williams
            '
            'shows title screen
            '
            'works thus:
            '
            'the intro screen comprises of TWO text files:
            '
            '    introscr1.txt
            '    introscr2.txt
            '
            'these are loaded into two variables then written one at a time to the console and the
            'background/foreground colour is changed
            '
            '
        */
            byte bytNum = 0;
            StreamReader strmIntro1;   //'used for reading the text files in
            StreamReader strmIntro2;
            string strIntro1 = "";     //stores the intro file data
            string strIntro2 = "";

            //'read intro screen files into strings
            strmIntro1 = new StreamReader("INTROSCR1.txt");
            strIntro1 = strmIntro1.ReadToEnd();
            strmIntro2 = new StreamReader("INTROSCR2.txt");
            strIntro2 = strmIntro2.ReadToEnd();
            Console.ForegroundColor = ConsoleColor.White;

            //'iterate changing console colour 6 times
            for (bytNum = 0; bytNum != 6; bytNum++)
            {
                Console.Clear();

                //'if 0 show one colour else show other
                if (bytNum % 2 == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(strIntro1);
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.WriteLine(strIntro1);
                }
                //'wait to give user chance to see the colour change
                Thread.Sleep(1000);
            }

            Console.Clear();

            //'iterate changing console colour 6 times
            for (bytNum = 0; bytNum != 6; bytNum++)
            {
                Console.Clear();

                if (bytNum % 2 == 0)
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(strIntro2);
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(strIntro2);
                }
                //'wait to give user chance to see the colour change
                Thread.Sleep(1000);
            }

            strmIntro1.Dispose();
            strmIntro2.Dispose();
            //   'show first proper game screen
            //  'display room 1
            ShowRoom(1);
        }

        private void LoadLeve1()
        /* 
                'Created 23/07/2024 By Roger Williams
                '
                'loads level 1 from level1.txt into lstRooms which is a collection of clsGameRooms
                'level text file format matches the class structure
                '
        */
        {
            StreamReader strmRead;
            string       strTemp = "";

            strmRead = new StreamReader("level1.txt");
            //'clear incase already contains data
            lstRooms.Clear();
            //        'first add blank class object as need 0 to represent null and index 1 = room 1
            clsCurRoom = new clsGameRooms();
            lstRooms.Add(clsCurRoom);

            while (!strmRead.EndOfStream)
            {
                //   'recreate the class object else carries over previous values!
                clsCurRoom = new clsGameRooms();
                strTemp = strmRead.ReadLine();
                clsCurRoom.ID = int.Parse(strTemp);
                strTemp = strmRead.ReadLine();
                clsCurRoom.NextRoomNorth = int.Parse(strTemp);
                strTemp = strmRead.ReadLine();
                clsCurRoom.NextRoomSouth = int.Parse(strTemp);
                strTemp = strmRead.ReadLine();
                clsCurRoom.NextRoomEast = int.Parse(strTemp);
                strTemp = strmRead.ReadLine();
                clsCurRoom.NextRoomWest = int.Parse(strTemp);
                // 'read rooms description
                strTemp = strmRead.ReadLine();
                clsCurRoom.Desc1 = strTemp;
                strTemp = strmRead.ReadLine();
                clsCurRoom.Desc2 = strTemp;
                strTemp = strmRead.ReadLine();
                clsCurRoom.Desc3 = strTemp;
                strTemp = strmRead.ReadLine();
                clsCurRoom.Desc4 = strTemp;
                strTemp = strmRead.ReadLine();
                clsCurRoom.Desc5 = strTemp;
                strTemp = strmRead.ReadLine();
                clsCurRoom.Desc6 = strTemp;
                strTemp = strmRead.ReadLine();
                clsCurRoom.Desc7 = strTemp;
                strTemp = strmRead.ReadLine();
                clsCurRoom.Desc8 = strTemp;
                strTemp = strmRead.ReadLine();
                clsCurRoom.Desc9 = strTemp;
                strTemp = strmRead.ReadLine();
                clsCurRoom.Desc10 = strTemp;
                strTemp = strmRead.ReadLine();
                clsCurRoom.Desc11 = strTemp;
                strTemp = strmRead.ReadLine();
                clsCurRoom.Desc12 = strTemp;
                strTemp = strmRead.ReadLine();
                clsCurRoom.Desc13 = strTemp;
                strTemp = strmRead.ReadLine();
                clsCurRoom.Desc14 = strTemp;
                strTemp = strmRead.ReadLine();
                clsCurRoom.Desc15 = strTemp;
                strTemp = strmRead.ReadLine();
                clsCurRoom.Desc16 = strTemp;
                strTemp = strmRead.ReadLine();
                clsCurRoom.Desc17 = strTemp;
                strTemp = strmRead.ReadLine();
                clsCurRoom.Desc18 = strTemp;
                strTemp = strmRead.ReadLine();
                clsCurRoom.Desc19 = strTemp;
                strTemp = strmRead.ReadLine();
                clsCurRoom.Desc20 = strTemp;
                // 'store in level room list
                lstRooms.Add(clsCurRoom);
            }
            //'close level text file
            strmRead.Close();
            strmRead.Dispose();
        }
        private void ExitProgram()
        {
        /*
            'Created 24/07/2024 By Roger Williams
            '
            'technically pointless as the console will just close, but in Visual Stdudio it will wait for a keypress!
            '
         */
            Console.WriteLine("Bye!");
        }
        private void Init()
        /*
                'Created 23/07/2024 By Roger Williams
                '
                'initialises the console, set title, colours etc and show title screen
                '
                '
        */
        {
            LoadLeve1();
            Console.Title = "Rog's Adventure!";
            Console.WindowHeight = 42; // 'NOTE height is measured in ROWS
            Console.WindowWidth = 121; // 'NOTE width is measured in COLUMNS
            Console.Clear();
            //'show intro
            ShowTitle();
            //   'set console for main game look
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        static void Main(string[] args)
        /*
                'Created 23/07/2024 By Roger Williams
                '
                'main routine for the game
                '
        */
        {
            string strInput = "";
            int intNum = 0;
            var clsProgram = new Program();  //unlike VB have to redeclare the program class so Main can use the subs/funcs!
            //configure the console and show intro
            clsProgram.Init();
            //'loop till user wants to leave
            while (strInput != "exit")
            {
                //'get user instruction
                strInput = Console.ReadLine();
                //'validate entry
                clsProgram.clsParser.ParseText(strInput);

                //'if it ok?
                if (clsProgram.clsParser.isOk)
                {
                    //'check for movement verb
                    if ((strInput.IndexOf("go ") != -1) || (strInput.IndexOf("move ") != -1))
                    {
                        //  'set to current room number - why? because if (the direction is VALID
                        //  'the room number will change
                        intNum = clsProgram.intCurRoom;

                        //  'south is forward, north backward, east/west left/right
                        switch (clsProgram.clsParser.Direction)
                        {
                            case "north":
                                {
                                    if (clsProgram.clsCurRoom.NextRoomNorth != 0)
                                    {
                                        // 'move north
                                        clsProgram.intCurRoom = clsProgram.clsCurRoom.NextRoomNorth;
                                        break;
                                    }
                                    break;
                                }
                            case "south":
                                {
                                    if (clsProgram.clsCurRoom.NextRoomSouth != 0)
                                    {
                                        // 'move north
                                        clsProgram.intCurRoom = clsProgram.clsCurRoom.NextRoomSouth;
                                        break;
                                    }
                                    break;
                                }
                            case "east":
                                {
                                    if (clsProgram.clsCurRoom.NextRoomEast != 0)
                                    {
                                        // 'move north
                                        clsProgram.intCurRoom = clsProgram.clsCurRoom.NextRoomEast;
                                        break;
                                    }
                                    break;
                                }
                            case "west":
                                {
                                    if (clsProgram.clsCurRoom.NextRoomWest != 0)
                                    {
                                        //  'move north
                                        clsProgram.intCurRoom = clsProgram.clsCurRoom.NextRoomWest;
                                        break;
                                    }
                                    break;
                                }
                            default:
                                {
                                    Console.WriteLine("Sorry! - Direction entered isnt available! Please try again");
                                    //'wait before redrawing screen
                                    Thread.Sleep(3000);
                                    break;
                                }
                        }

                        //'if command not acceptable dont change rooms  
                        if (intNum == clsProgram.intCurRoom)
                        {
                            Console.WriteLine("Sorry! - Direction entered isnt available! Please try again");
                            strInput = "";
                            //'wait before redrawing screen
                            Thread.Sleep(3000);
                        }

                        //'shows new or even existing room
                        clsProgram.clsCurRoom = clsProgram.lstRooms.Find(clsCurRoomsFind => clsCurRoomsFind.ID == clsProgram.intCurRoom);
                        //'store the room ID - for future development
                        clsProgram.intCurRoom = clsProgram.clsCurRoom.ID;
                        //'show room to player pass 0 as not first room
                        clsProgram.ShowRoom(0);

                        //'has user lost/won the game?
                        if (clsProgram.CheckIfEnd())
                        {
                            //'set text input to "exit" this causes the game to end 
                            strInput = "exit";
                        }
                    }
                    else
                    {
                        //'ignore help and exit commands only show error for commands not understood
                        //'NOTE: check game logic - can this be refactored away?
                        if ((!strInput.Contains("help")) && (strInput != "exit"))
                        {
                            Console.WriteLine("Unregonised command, please tgry again!");
                        }

                        //'ignore exit command
                        if (strInput != "exit")
                        { 
                            strInput = "";
                            Thread.Sleep(4000);
                            clsProgram.ShowRoom(0);
                        }
                    }
                } 
              else
                {
                    //'if command not understood and not "exit"
                    if (strInput != "exit")
                    {
                       Console.WriteLine("Unregonised command, please try again!");
                        strInput = "";
                        Thread.Sleep(4000);
                        clsProgram.ShowRoom(0);
                    }
                }
            }
            //'Bye!
            clsProgram.ExitProgram();
        }
    }
}