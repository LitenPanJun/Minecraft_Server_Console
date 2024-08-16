using System.Diagnostics;

namespace Minecraft_Server_Console
{
    public class Server(string FileDir)
    {
        private string FileDir = FileDir;//服务端运行目录
        private string ServerFile;//服务端文件
        private Process Process;//进程
        private string Cmd;//附加命令行

        /// <summary>
        /// 启动服务器
        /// </summary>
        /// <param name="ServerFile">服务端/Java.exe文件</param>
        /// <param name="Cmd">命令行，用于Java服务器</param>
        /// <returns>无特殊含义</returns>
        public bool Run(string ServerFile, string? Cmd)
        {
            Process = new Process();
            Process.StartInfo.FileName = ServerFile;
            if (Cmd != null) Process.StartInfo.Arguments = Cmd;
            Process.StartInfo.UseShellExecute = false;
            Process.StartInfo.RedirectStandardOutput = true;
            Process.StartInfo.RedirectStandardError = true;
            Process.StartInfo.RedirectStandardInput = true;
            Process.StartInfo.CreateNoWindow = true;
            Process.StartInfo.WorkingDirectory = FileDir;
            try
            {
                Process.Start();
            }
            catch
            {
                Process = null;
                return false;
            }
            this.Cmd = Cmd;
            this.ServerFile = ServerFile;
            return true;
        }
        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="Cmd">要发送的命令</param>
        /// <returns>无特殊含义</returns>
        public bool SendCommand(string Cmd)
        {
            Process.StandardInput.WriteLine(Cmd);
            return true;
        }
        /// <summary>
        /// 正常关服（非BC类）
        /// </summary>
        /// <returns>无特殊含义</returns>
        public bool Stop()
        {
            SendCommand("stop");
            return true;
        }
        /// <summary>
        /// 强制关服
        /// </summary>
        /// <returns>无特殊含义</returns>
        public bool Kill()
        {
            Process.Kill();
            return true;
        }
        /// <summary>
        /// 重启
        /// </summary>
        /// <returns>无特殊含义</returns>
        public bool Restart()
        {
            //等待后期实现（挤牙膏ing）
            return true;
        }
    }
}
