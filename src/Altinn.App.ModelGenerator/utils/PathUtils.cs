using System.IO;

namespace Altinn.App.ModelGenerator
{
    public static class PathUtils
    {
        public static bool IsLayoutPath(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            if(!fileInfo.Extension.Equals(".json", System.StringComparison.InvariantCultureIgnoreCase)) return false;
            if(!fileInfo.Directory.Name.Equals("layouts", System.StringComparison.InvariantCultureIgnoreCase)) return false;
            if(!fileInfo.Directory.Parent.Name.Equals("ui", System.StringComparison.InvariantCultureIgnoreCase)) return false;
            return true;
        } 
        
        public static bool IsResourcePath(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            if(!fileInfo.Extension.Equals(".json", System.StringComparison.InvariantCultureIgnoreCase)) return false;
            if(!fileInfo.Name.StartsWith("resource.", System.StringComparison.InvariantCultureIgnoreCase)) return false;
            if(!fileInfo.Directory.Name.Equals("texts", System.StringComparison.InvariantCultureIgnoreCase)) return false;
            if(!fileInfo.Directory.Parent.Name.Equals("config", System.StringComparison.InvariantCultureIgnoreCase)) return false;
            return true;
        }
        
        public static bool IsApplicationmetadata(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            if(!fileInfo.Name.Equals("applicationmetadata.json", System.StringComparison.InvariantCultureIgnoreCase)) return false;
            if(!fileInfo.Directory.Name.Equals("config", System.StringComparison.InvariantCultureIgnoreCase)) return false;
            return true;
        }
        
        public static bool IsSettings(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            if(!fileInfo.Name.Equals("settings.json", System.StringComparison.InvariantCultureIgnoreCase)) return false;
            if(!fileInfo.Directory.Name.Equals("ui", System.StringComparison.InvariantCultureIgnoreCase)) return false;
            return true;
        }

        public static string FileNameFromPath(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            return fileInfo.Name;
        }
    }
}