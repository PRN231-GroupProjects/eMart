using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMart_Repository.Constants
{
    public static class FileDirectories
    {
        private static string _workingDirectory = Environment.CurrentDirectory;
        private static string _projectDirectory = Directory.GetParent(_workingDirectory).FullName;
        private static string _seedJSONPath = $"{_projectDirectory}/eMart_Repository/Seed/JSON";
        public static string CategoryJsonPath = $"{_seedJSONPath}/CategoriesSeed.json";
        public static string ProductJsonPath = $"{_seedJSONPath}/ProductsSeed.json";
        public static string MemberJsonPath = $"{_seedJSONPath}/MembersSeed.json";
    }
}
