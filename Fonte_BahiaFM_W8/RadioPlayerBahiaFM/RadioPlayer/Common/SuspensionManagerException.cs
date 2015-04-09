using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Common
{
    /// <summary>
    /// InnerClass  SuspensionManagerException
    /// </summary>
    public class SuspensionManagerException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SuspensionManagerException"/> class.
        /// </summary>
        public SuspensionManagerException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SuspensionManagerException"/> class.
        /// </summary>
        /// <param name="e">Exception e</param>
        public SuspensionManagerException(Exception e)
            : base("SuspensionManager failed", e)
        {
        }
    }
}
