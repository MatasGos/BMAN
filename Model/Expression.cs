using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Expression
    {
        string action;
        string command;
        string playerID;
        List<Player> players;
        public Expression(string message, string contextID, List<Player> players)
        {
            string[] splitMessage = message.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (splitMessage.Length > 0)
            {
                action = splitMessage[1];
                command = message.Substring(5);
                playerID = contextID;
                this.players = players;
            }
        }
        public string initialize()
        {
            string[] splitCommand = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (Player player in players)
            {
                if (player.id == playerID) {
                    switch (action)
                    {
                        case "set":
                            string skinCode = "";
                            if (command.Contains("-hat="))
                            {
                                foreach(string val in splitCommand)
                                {
                                    if (val.Contains("-hat="))
                                    {
                                        string skin = val.Substring(5);
                                        if(skin == "fedora")
                                        {
                                            skinCode += "f";
                                        }
                                    }
                                }
                            }
                            if (command.Contains("-shoes="))
                            {
                                foreach (string val in splitCommand)
                                {
                                    if (val.Contains("-shoes="))
                                    {
                                        string skin = val.Substring(7);
                                        if (skin == "leather")
                                        {
                                            skinCode += "s";
                                        }
                                    }
                                }
                            }
                            player.UpdatePlayerStructure(skinCode);
                            return "";
                        case "save":
                            return "Skin saved at index " + player.SavePlayerStructure();
                        case "load":
                            int parsedInt;
                            if (int.TryParse(splitCommand[1], out parsedInt))
                            {
                                if (parsedInt < player.caretaker.GetLength())
                                {
                                    player.UndoPlayerStructure(parsedInt);
                                    return "";
                                }
                            }
                            return ("Wrong command. Read manual");
                        default:
                            return ("Wrong command. Read manual");
                    }                  
                }
            }
            return ("Player no longer exists");
        }
    }
}
