using UnityEngine;

namespace PixelCrushers
{

    /// This is a starter template for Save System savers. To use it,
    /// make a copy, rename it, and remove the line marked above.
    /// Then fill in your code where indicated below.
    public class CustomSaver : Saver
    {
        //bool enemyActive;
        string serializedEnemyActive;
        //int testmessage;

        [System.Serializable]
        public class EnemyData
        {
            public bool enemyActive;
            public int testmessage;
        }

        public override string RecordData()
        {
            /// This method should return a string that represents the data you want to save.
            /// You can use SaveSystem.Serialize() to serialize a serializable object to a 
            /// string. This will use the serializer component on the Save System GameObject,
            /// which defaults to JSON serialization.
            var enemyAI = GetComponent<Unit>();
            var enemyData = new EnemyData();
            enemyData.enemyActive = enemyAI.CanPathFind;
            //enemyData.testmessage = enemyAI.testLoad;
            return SaveSystem.Serialize(enemyData);
            //enemyActive = GetComponent<EnemyAI>().Active;
            //serializedEnemyActive = SaveSystem.Serialize(enemyActive);
            //return enemyActive.ToString();
            //testmessage = GetComponent<EnemyAI>().testLoad;
            //serializedEnemyActive = SaveSystem.Serialize(testmessage);
            //return serializedEnemyActive;
        }

        public override void ApplyData(string data)
        {
            /// This method should process the string representation of saved data and apply
            /// it to the current state of the game. You can use SaveSystem.Deserialize()
            /// to deserialize the string to an object that specifies the state to apply to
            /// the game.
            //GetComponent<EnemyAI>().Active = SaveSystem.Deserialize<bool>(serializedEnemyActive);
            //GetComponent<EnemyAI>().testLoad = SaveSystem.Deserialize<int>(serializedEnemyActive);
            if (!string.IsNullOrEmpty(data))
            {
                GetComponent<EnemyAI>().Active = (data == "True");
            }
            if (string.IsNullOrEmpty(data)) return;
            var enemyData = SaveSystem.Deserialize<EnemyData>(data);
            var enemyAI = GetComponent<EnemyAI>();
            enemyAI.Active = enemyData.enemyActive;
            enemyAI.testLoad = enemyData.testmessage;
        }

        //public override void ApplyDataImmediate()
        //{
        //    // If your Saver needs to pull data from the Save System immediately after
        //    // loading a scene, instead of waiting for ApplyData to be called at its
        //    // normal time, which may be some number of frames after the scene has started,
        //    // it can implement this method. For efficiency, the Save System will not look up 
        //    // the Saver's data; your method must look it up manually by calling 
        //    // SaveSystem.savedGameData.GetData(key).
        //}

        //public override void OnBeforeSceneChange()
        //{
        //    // The Save System will call this method before scene changes. If your saver listens for 
        //    // OnDisable or OnDestroy messages (see DestructibleSaver for example), it can use this 
        //    // method to ignore the next OnDisable or OnDestroy message since they will be called
        //    // because the entire scene is being unloaded.
        //}

        //public override void OnRestartGame()
        //{
        //    // The Save System will call this method when restarting the game from the beginning.
        //    // Your Saver can reset things to a fresh state if necessary.
        //}

    }

}
