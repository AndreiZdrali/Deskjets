using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using Deskjets.Settings;

namespace Deskjets.Classes
{
    static class SaveLoad
    {
        public static void SerializeObject<T>(string fileName, T obj)
        {
            try
            {
                using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, obj);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static T DeserializeObject<T>(string fileName)
        {
            T obj = default;
            try
            {
                using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    //am bagat if-ul asta ca daca fisierul era gol dadea eroare
                    if (stream.Length != 0)
                    {
                        var formatter = new BinaryFormatter();
                        obj = (T)formatter.Deserialize(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return obj;
        }

        #region MAI SPECIFICE
        public static void SerializeGeneralSettings()
        {
            SerializeObject<GeneralSettings>(Global.SettingsFile, Global.GeneralSettings);
        }
        
        public static void SerializeUnserializableSettings()
        {
            SerializeObject<UnserializableSettings>(Global.UnserializableSettingsFile, Global.UnserializableSettings);
        }
        #endregion
    }
}
