using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ProcessKiller.Infrastructure
{
    public static class SystemProcesses
    {
        public static Dictionary<string, string> EnvironmentProcesses = new Dictionary<string, string>() {
            {"svchost", "windows"},
            {"conhost", "windows" },
            {"System", "windows"},
            {"wininit", "windows"},
            {"lsass", "windows"},
            {"dasHost", "windows"},
            {"explorer", "windows"},
            {"Memory Compression", "windows"},
            {"MsMpEng", "windows"},
            {"rundll32", "windows"}
        };

        public static Dictionary<int, string> Exceptions = new Dictionary<int, string>();

        public static Dictionary<int, string> KillProcesses = new Dictionary<int, string>();

    }
}
