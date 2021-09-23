using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace RollSpelGrupp6.Classes
{
    public class PlayerDatabase
    {
        private static Dictionary<string, Player> PlayersData = new Dictionary<string, Player>();

        private static string DatabaseName = "PlayersDatabase.json";

        public PlayerDatabase()
        {
            PlayersData = new Dictionary<string, Player>();
        }

        public static void ReadFromPlayerDatabase()
        {
            string playersDataAsJSONString = File.ReadAllText(DatabaseName);
            if (playersDataAsJSONString.Length > 0)
            {
                PlayersData = JsonConvert.DeserializeObject<Dictionary<string, Player>>(playersDataAsJSONString);
            }
        }

        public static void WriteToPlayerDatabase()
        {
            string playersDataAsJSONString = JsonConvert.SerializeObject(PlayersData);
            File.WriteAllText(DatabaseName, playersDataAsJSONString);
        }

        public static void AddUserToPlayerDatabase(string username, Player player)
        {
            if (PlayersData.TryAdd(username, player))
            {
                return;
            }
            PlayersData[username] = player;
        }

        public static Player GetUserFromPlayerDatabase(string username)
        {
            Player playerToReturn;
            if (PlayersData.TryGetValue(username, out playerToReturn))
            {
                return playerToReturn;
            }
            playerToReturn = new Player();
            playerToReturn.Name = username;
            playerToReturn.DressUp();
            return playerToReturn;
        }
    }
}