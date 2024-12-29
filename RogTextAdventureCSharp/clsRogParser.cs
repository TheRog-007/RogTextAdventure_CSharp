using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 ported from the Visual basic version!

 original comments from the Visual Basic version included 
 disabled code has been removed

*/

namespace RogTextAdventureCSharp
{
    internal class clsRogParser
    {

        /* 
        'Created 23/07/2024 By Roger Williams
        '
        'What It Does
        '------------
        '
        'checks passed text to try and determine if statement valid
        '
        '- passes through all the lists
        '- checks for duplicates
        '- checka valid querry
        '- 
        '
        'e.g.:
        '
        'open door with the key
        '
        'run away
        '
        'would produce response:
        ' which direction?
        '
        'run away north
        '
        'would produce response:
        '  you ran north
        '
        'NOTE: this phase one version is only passed direction commands, BUT the infrastructure is all in place for 
               future expansion to phase two
        '
        */
        
    public bool isOk = true;       //'used externally to determine if statement valid
    //'available to caller to see key elements of what user typed
    public string Noun       = "";
    public string Verb        = "";
    public string Adjective   = "";
    public string Preposition = "";
    public string Direction   = "";

    //'internal lists
    private readonly List<string> lstVerbs = new List<string> {
                                                               "be", "have", "do", "go", "get", "make", "know", "take", "see", "look", "give", "need", "put", "get", "let", "begin", "create", "start", "run", "move", "creep",
                                                               "hold", "use", "include", "set", "stop", "allow", "appear", "destroy", "kill", "disable", "enable", "open", "close", "run", "talk", "listen", "walk"};
    private readonly List<string> lstNouns = new List<string> {"exit", "my", "you", "them", "they", "him", "she", "me", "their", "knife", "apple", "bread", "sword", "dragon", "knight", "key", "plate", "candle", "matches", "door"};
    private readonly List<string> lstAdjectives = new List<string>{"new", "old", "box", "first", "last", "current", "low", "high", "partial", "full", "common", "late", "early", "on", "used", "alert", "away", "forward", "backward",
                                                                   "left", "right" };
    private readonly List<string> lstPrepositions = new List<string> { "in", "of", "with", "to", "behind", "when", "why", "while", "kind", "by", "under", "before", "up", "down", "between" };
    private readonly List<string> lstDirections = new List<string> { "north", "south", "east", "west" };
    //'used when users types HELP LIST <verbs><nouns><adjectives><Prepositions><directions>
    private readonly List<string> lstHelpWords = new List<string> { "Verbs", "Nouns", "Adjectives", "Prepositions", "Directions" };
    //'NOTE: the enumeration value is in the SAME order as the lists as created
        private enum enumWordTypes
        {
            Verbs=0,
            Nouns=1,
            Adjectives=2,
            Prepositions=3,
            Directions=4
        }

        /******************************custom c# only function******************************
         Created 25/07/2024 By Roger Williams 
          
         C# does not have a InStrRev function so created it!

    
        */
        private int InStrRev(string strWhere, string strWhat)
        {
            /*
                Created 25/07/2024 By Roger Williams 
          
                What It Does
                ============

                looks through a string from the RIGHT and finds the first instance of the string to look for
                and returns that position in the string

                VARS
                ----
                strWhere     : string to search
                strWhat      : string to look for

                RETURNS - location in string
            */

            int intNum = strWhere.Length-1;  //set to size of search string take 1 from it string length starts at zero (?)
            string strCompare = "";
            bool blnFound = false;
            //loop through the string
            while ((!blnFound) || (intNum>0))
            {
                //append characters to compare string one at a time from the RIGHT of strWhere 
                strCompare = strWhere.Substring(intNum, 1) + strCompare;
                //is it found?'
                if (strCompare.IndexOf(strWhat) !=-1)
                {
                    blnFound = true;
                    break;
                }

                intNum -= 1;
            }

            return intNum;
        }
        public void Help_ListValidWords(int intWhat)
        {
            /*
                    'Created 23/07/2024 By Roger Williams
                    '
                    'when user types: HELP LIST VERBS
                    '
                    'runs this which shows them on the console
                    '
                    'VARS
                    '
                    'bytWhat    : what to show (uses enum) 0=verb 1=noun
                    '
                    '
                    '
              */
            string strOutput = "";
            int intNum = 1;

            //'make sure help is only text on screen
            //'
            // 'NOTE: for later phases how about a SECOND console with the help soley on it?
            // '      tis the age of 20 inch monitors after all..
            Console.Clear();

            switch (intWhat)
            {
               case 0: //'verbs
                    {
                        Console.WriteLine("Valid Verbs");
                        //'print list contents to console
                        foreach (string strTemp in lstVerbs)
                        {
                            //'puposely append to string 
                            strOutput = strOutput + strTemp + " ";
                            intNum += 1;

                            //'print string when 10 commands in it to stop unwanted word wrap
                            if (intNum == 10)
                            {
                                Console.WriteLine(strOutput);
                                //'reset vars
                                strOutput = "";
                                intNum = 1;
                            }
                        }

                        break;
                    }
//'NOTE: above procews is repeated for each of the list types
                case 1:  //'nouns
                    {
                       Console.WriteLine("Valid Nouns");
    
                       foreach (string strTemp in lstNouns)
                       {
                            strOutput = strOutput + strTemp + " ";
                            intNum += 1;


                            if (intNum == 10)
                            {
                                Console.WriteLine(strOutput);
                                strOutput = "";
                                intNum = 1;
                            }
                        }

                        break;
                    }
                case 2: //'adjectives
                    { 
                         Console.WriteLine("Valid Adjectives");
    
                        foreach (string strTemp in lstVerbs)
                        {
                            strOutput = strOutput + strTemp + " ";
                            intNum += 1;


                            if (intNum == 10)
                            {
                                Console.WriteLine(strOutput);
                                strOutput = "";
                                intNum = 1;
                            }
                        }

                        break;
                    }
                case 3: //'prepositions
                    { 
                    Console.WriteLine("Valid Prepositions");
    
                        foreach (string strTemp in lstPrepositions)
                        {
                            strOutput = strOutput + strTemp + " ";
                            intNum += 1;


                            if (intNum == 10)
                            {
                               Console.WriteLine(strOutput);
                               strOutput = "";
                               intNum = 1;
                            }
                        }

                       break;
                    }
                case 4: //'directions
                { 
                    Console.WriteLine("Valid Directions");

                    foreach (string strTemp in lstDirections)
                    {
                        strOutput = strOutput + strTemp + " ";
                        intNum += 1;


                        if (intNum == 10)
                        {
                            Console.WriteLine(strOutput);
                            strOutput = "";
                            intNum = 1;
                        }
                    }
                    break;
                }
            }
            //'check if string not null if so write to console
            //'NOTE: is there a better way to do this?  
            if (strOutput.Length != 0)
            {
                Console.WriteLine(strOutput);
            }

            //write blank line
            Console.WriteLine();
    }
    private void Help_List()
        {
            /*
                    'Created 24/07/2024 By Roger Williams
                    '
                    'Lists the available help options when user just types: HELP
                    '
                    '
                    '
                    'NOTE: for later phases could all these options be shown in SECOND console?
                    '
            */
            Console.Clear();
            Console.WriteLine("Help Options");
            Console.WriteLine("============");
            Console.WriteLine("");
            Console.WriteLine("List available adjectives             - help list adjectives");
            Console.WriteLine("List available verbs                  - help list verbs");
            Console.WriteLine("List available nouns                  - help list nouns");
            Console.WriteLine("List available prepositions           - help list prepositions");
            Console.WriteLine("List available directions of movement - help list directions");
            Console.WriteLine("");
            Console.WriteLine("Enter: exit - at any time to end game");
            Console.WriteLine("");
    }
        private bool ContainsValidWords(string strWhat, byte bytWhat)
        {
            /*     
                    'Created 23/07/2024 By Roger Williams
                    '
                    'checks if strPhrase contains verb,noun,adjective,preposition,direction
                    '
                    'VARS
                    '
                    'strWhat    : what to search
                    'bytWhat    : what to check for (enum) verb,noun etc
                    '
                    'returns true if finds valid word
                    ' 
                    'also populates public class vars:
                    '
                    'noun
                    'verb
                    'adjective
                    'preposition
                    'direction
                    '
              */
            bool blnOK = false;
            //what to check
            switch (bytWhat)
            {
                case 0: // 'verbs
                    {
                        //'iterate through the list looking for the required value
                        foreach (string strTemp in lstVerbs)
                        {
                            //'does any part of the passed string exist in the list? 
                            if (strWhat.IndexOf(strTemp) != -1)
                            {
                                //'set public variable
                                Verb = strTemp;
                                //'say ok then exit loop - saving processor cycles!
                                blnOK = true;
                                break;
                            }
                        }

                        break;
                    }
       //'NOTE: above process copied for rest of the options
                case 1: // 'nouns
                    {
                        foreach (string strTemp in lstNouns)
                        {
                            if (strWhat.IndexOf(strTemp) != -1)
                            {
                                Noun = strTemp;
                                blnOK = true;
                                break;
                            }
                        }
                        break;

                    }
                case 2: //'adjectives
                    {
                        foreach (string strTemp in lstAdjectives)
                        {
                            if (strWhat.IndexOf(strTemp) != -1)
                            {
                                Adjective = strTemp;
                                blnOK = true;
                                break;
                            }
                        }
                        break;

                    }
                case 3: //'prepositions
                    {
                        foreach (string strTemp in lstPrepositions)
                        {
                            if (strWhat.IndexOf(strTemp) != -1)
                            {
                                Preposition = strTemp;
                                blnOK = true;
                                break;
                            }
                        }
                        break;

                    }
                case 4: // 'directions
                    {
                        foreach (string strTemp in lstDirections)
                        {
                            if (strWhat.IndexOf(strTemp) != -1)
                            {
                                Direction = strTemp;
                                blnOK = true;
                                break;
                            }
                        }
                        break;

                    }
                    //'no need for a case..else as fixed values are sent
            }
            return blnOK;
    }

    public void ParseText(string strWhat)
        { 
/*
        'Created 23/07/2024 By Roger Williams
        '
        'checks if text contains valid words e.g. nouns sets IsOk accordingly
        '
        'Rules (phase one)
        '-----
        '
        '- every phase should contain a verb
        '- every verb should either have an adjective e.g. open door
        'or
        'a preposition e.g. while
        'or
        'a noun e.g. key
        '
        'also handles user help requests, valid request string are:
        '
        'HELP
        'HELP LIST 
        ''         VERBS
        '          NOUNS
        '          ADJECTIVES
        '          PREPOSITIONS
        '          DIRECTIONS
        '
        '
*/
        byte bytValid  = 0;
        string strTemp = "";
        //'set check variables to false
        bool blnAdjective  = false;
        bool blnDirection  = false;
        bool blnNoun = false;
        bool blnPreposition  = false;
        bool blnVerb = false;
        //'if passed string has no value leave and set error to true
         if (strWhat.Length == 0)
          {
             isOk = false;
          }
        else
            {
                //'clear public vars
                Noun = "";
                Adjective = "";
                Verb = "";
                Preposition = "";
                Direction = "";

                //'convert to lowercase
                strWhat = strWhat.ToLower();
                //'check if help request
                if (strWhat.IndexOf("help") != -1)
                {
                   //'check if help request
                   if (strWhat == "help")
                   {
                        Help_List();
                   }
                    //'check if user asking for a list
                    if (strWhat.IndexOf("help list ") != -1)
                    {
                        //extract last word
                        strTemp = strWhat.Substring(InStrRev(strWhat, " ")+1, strWhat.Length - InStrRev(strWhat, " ")-1);

                        switch( strTemp)
                        {
                            case "verbs":
                                {
                                    Help_ListValidWords((int)enumWordTypes.Verbs);
                                    break;
                                }
                            case "adjectives":
                                {
                                    Help_ListValidWords((int)enumWordTypes.Adjectives);
                                    break;
                                }
                            case "nouns":
                                {
                                    Help_ListValidWords((int)enumWordTypes.Nouns);
                                    break;
                                }
                            case "prepositions":
                                {
                                    Help_ListValidWords((int)enumWordTypes.Prepositions);
                                    break;
                                }
                            case "directions":
                                {
                                    Help_ListValidWords((int)enumWordTypes.Directions);
                                    break;
                                } 
                        }
                    }
                }
            else
             {
                /*
                                'every phrase should contain a verb
                                'every verb should either have an
                                '
                                'adjective e.g. open door
                                'or
                                'a preposition e.g. while
                                'or
                                'a noun e.g. key
                                '
                 */
                if (ContainsValidWords(strWhat, (int)enumWordTypes.Adjectives))
                {
                      blnAdjective = true;
                }
                if (ContainsValidWords(strWhat, (int)enumWordTypes.Directions))
                {
                      blnDirection = true;
                }
                if (ContainsValidWords(strWhat, (int)enumWordTypes.Nouns))
                {
                    blnNoun = true;
                }
                if (ContainsValidWords(strWhat, (int)enumWordTypes.Prepositions))
                {
                    blnPreposition = true;
                }
                if (ContainsValidWords(strWhat, (int)enumWordTypes.Verbs))
                {
                    blnVerb = true;
                }

                //'mow look at the rules
                // 'NOTE: these are primitive grammar rules and need to be expanded and developed
                if (blnVerb)
                {
                    bytValid = 1;
                }
                if ((blnAdjective) && (blnVerb))
                {
                    bytValid += 1;
                }
                if ((blnPreposition) && (blnVerb))
                {
                    bytValid += 1;
                }
                if ((blnNoun) && (blnVerb))
                {
                    bytValid += 1;
                }
                //'if valid phrase or user typed "exit"
                if ((bytValid > 0) || (Noun=="exit"))
                   {
                    isOk = true;
                   }
                else
                    {
                        //'if not containing any valid words set to incorrect phrase
                        Console.WriteLine(strWhat + " - Not Recognised Phrase");
                        isOk = false;
                    }
                }
            }
        }
    }
}
