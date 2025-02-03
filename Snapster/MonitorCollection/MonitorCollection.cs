using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.HellStormGames.Imaging.ScreenCapture
{
    public class MonitorCollection : IEnumerable<Monitor>, IDisposable
    {
        private List<Monitor> monitors;

        public MonitorCollection()
        {
            monitors = new List<Monitor>();
        }
        /// <summary>
        /// Adds Monitor to Collection List
        /// </summary>
        /// <param name="monitor"></param>
        public void Add(Monitor monitor)
        {
            monitors.Add(monitor);
        }
        /// <summary>
        /// Clears all monitors from list.
        /// </summary>
        public void Clear() { monitors.Clear(); }

        public void Dispose()
        {
            if (monitors != null && monitors.Count > 0)
            {
                monitors.Clear();
                monitors = null;
            }
        }

        /// <summary>
        /// Gets Monitor from Collection List.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Monitor Get(int index)
        {
            return monitors[index];
        }

        public IEnumerator<Monitor> GetEnumerator()
        {
            return monitors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return monitors.GetEnumerator();
        }
    }
}
