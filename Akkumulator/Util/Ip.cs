using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Akkumulator.Util
{
    public delegate void IpChangeHandler(string newIp);

    internal class Ip
    {
        public const string ERROR_DETECTING_IP = "";

        // listeners
        private static readonly HashSet<IpChangeHandler> listeners = new HashSet<IpChangeHandler>();

        public static void AddListener(IpChangeHandler newListener)
        {
            _ = listeners.Add(newListener);
        }

        public static void RemoveListener(IpChangeHandler oldListener)
        {
            _ = listeners.Remove(oldListener);
            if (listeners.Count == 0)
            {
                Stop();
            }
        }

        public static void RemoveAllListeners()
        {
            listeners.Clear();
            Stop();
        }

        private static void NotifyAllListeners(string ip)
        {
            if (listeners.Count == 0)
            {
                return;
            }
            foreach (IpChangeHandler handler in listeners)
            {
                handler(ip);
            }
        }

        public static string Current { get; private set; } = "";

        // ip api client
        private static readonly HttpClient client = new HttpClient();
        
        private static async Task<string> LoadIpAsync()
        {
            try
            {
                return await client.GetStringAsync("https://api.ipify.org");
            }
            catch (HttpRequestException)
            {
                return ERROR_DETECTING_IP;
            }
        }

        // periodical updates
        private static int _refreshTimeout = 300000;
        public static int RefreshTimeout
        {
            get => _refreshTimeout;
            set
            {
                if (value > 0)
                {
                    _refreshTimeout = value;
                }
            }
        }

        private static bool canRefreshIp = false;

        private static async Task RefreshIpAsync()
        {
            if (!canRefreshIp)
            {
                return;
            }

            string newIp = await LoadIpAsync();
            if (newIp == Current)
            {
                return;
            }

            Current = newIp;
            NotifyAllListeners(Current);

            await Task.Delay(RefreshTimeout);
            _ = RefreshIpAsync();
        }

        public static void Start()
        {
            if (canRefreshIp)
            {
                return;
            }
            canRefreshIp = true;
            _ = RefreshIpAsync();
        }

        public static void Stop()
        {
            canRefreshIp = false;
        }
    }
}
